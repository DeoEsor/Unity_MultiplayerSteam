using System;
using System.Threading.Tasks;
using Netcode.Transports.Facepunch;
using Steamworks;
using Steamworks.Data;
using Unity.Netcode;
using UnityEngine;

namespace Networking
{
    [RequireComponent(typeof(FacepunchTransport))]
    public class GameNetworkManager : MonoBehaviour
    {
        private bool _isClientStarted;
        private bool _isHostStarted;
        public static GameNetworkManager Instance { get; private set; }

        public Lobby? CurrentLobby { get; private set; }

        public FacepunchTransport Transport { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            Transport = GetComponent<FacepunchTransport>();

            SteamMatchmaking.OnLobbyCreated += OnLobbyCreated;
            SteamMatchmaking.OnLobbyEntered += OnLobbyEntered;
            SteamMatchmaking.OnLobbyMemberJoined += OnLobbyMemberJoined;
            SteamMatchmaking.OnLobbyMemberLeave += OnLobbyMemberLeave;
            SteamMatchmaking.OnLobbyInvite += OnLobbyInvite;
            SteamMatchmaking.OnLobbyGameCreated += OnLobbyGameCreated;
            SteamFriends.OnGameLobbyJoinRequested += OnGameLobbyJoinRequested;
        }

        private void OnDestroy()
        {
            SteamMatchmaking.OnLobbyCreated -= OnLobbyCreated;
            SteamMatchmaking.OnLobbyEntered -= OnLobbyEntered;
            SteamMatchmaking.OnLobbyMemberJoined -= OnLobbyMemberJoined;
            SteamMatchmaking.OnLobbyMemberLeave -= OnLobbyMemberLeave;
            SteamMatchmaking.OnLobbyInvite -= OnLobbyInvite;
            SteamMatchmaking.OnLobbyGameCreated -= OnLobbyGameCreated;
            SteamFriends.OnGameLobbyJoinRequested -= OnGameLobbyJoinRequested;

            if (NetworkManager.Singleton == null)
                return;

            NetworkManager.Singleton.OnServerStarted -= OnServerStarted;
            NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnectedCallback;
            NetworkManager.Singleton.OnClientDisconnectCallback -= OnClientDisconnectCallback;
        }

        private void OnApplicationQuit()
        {
            Disconnect();
        }

        public async void StartHost(int maxMembers = 100)
        {
            NetworkManager.Singleton.OnServerStarted += OnServerStarted;
            if (_isHostStarted || _isClientStarted)
                return;
            if (NetworkManager.Singleton.StartHost())
            {
                Debug.Log("Host started", gameObject);
                _isHostStarted = true;
            }
            else
            {
                Debug.Log("Host didn't start", gameObject);
            }

            CurrentLobby = await SteamMatchmaking.CreateLobbyAsync(maxMembers);
        }

        public void StartClient(SteamId id, Lobby lobby)
        {
            if (_isHostStarted || _isClientStarted)
                return;

            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnectedCallback;
            NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnectCallback;

            CurrentLobby = lobby;
            CurrentLobby?.Join();

            Transport.targetSteamId = id;

            if (NetworkManager.Singleton.StartClient())
            {
                _isClientStarted = true;
                Debug.Log("Client is connected", gameObject);
            }
            else
            {
                Debug.Log("Client isn't connected", gameObject);
            }
        }

        public void Disconnect()
        {
            CurrentLobby?.Leave();
            if (NetworkManager.Singleton == null)
                return;
            NetworkManager.Singleton.Shutdown();
            _isClientStarted = false;
            _isHostStarted = false;
        }

        private static async Task<Image?> GetAvatar()
        {
            try
            {
                return await SteamFriends.GetLargeAvatarAsync(SteamClient.SteamId);
            }
            catch (Exception e)
            {
                Debug.Log(e);
                return null;
            }
        }

        #region Network Callbacks

        private void OnClientDisconnectCallback(ulong clientId)
        {
            Debug.Log($"Client disconnected, clientId={clientId}");

            Disconnect();
        }

        private void OnServerStarted()
        {
            Debug.Log("Server started", gameObject);
        }

        private void OnClientConnectedCallback(ulong clientId)
        {
            Debug.Log($"Client connected, clientId={clientId}");
        }

        #endregion

        #region Steam Callbacks

        private void OnGameLobbyJoinRequested(Lobby lobby, SteamId id)
        {
            Debug.Log("Connecting to lobby");
            StartClient(id, lobby);
        }

        private void OnLobbyGameCreated(Lobby lobby, uint ip, ushort port, SteamId id)
        {
            Debug.Log("Lobby has been created");
        }

        private void OnLobbyInvite(Friend friend, Lobby lobby)
        {
            Debug.Log($"You got a invite from {friend.Name}", gameObject);
        }

        private void OnLobbyMemberLeave(Lobby lobby, Friend friend)
        {
            Debug.Log($"{friend.Name} left the lobby");
        }

        private void OnLobbyMemberJoined(Lobby lobby, Friend friend)
        {
            Debug.Log($"{friend.Name} joined the lobby");
        }

        private void OnLobbyEntered(Lobby lobby)
        {
            Debug.Log("You're connected to lobby");

            if (NetworkManager.Singleton.IsHost)
                return;
        }

        private void OnLobbyCreated(Result result, Lobby lobby)
        {
            Debug.Log("Lobby created");

            if (result != Result.OK)
            {
                Debug.LogError($"Lobby couldn't be created, {result}", gameObject);
                return;
            }

            lobby.SetFriendsOnly();
            lobby.SetData("name", "Cool Lobby");
            lobby.SetJoinable(true);
            lobby.SetPublic();

            Debug.Log("Lobby has been created!");
        }

        #endregion
    }
}