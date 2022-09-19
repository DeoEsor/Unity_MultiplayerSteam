using System.Collections.Generic;
using System.Threading.Tasks;
using Steamworks;
using Steamworks.Data;
using Unity.Netcode;
using UnityEngine;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        private readonly Dictionary<SteamId, Friend> _players = new();
        public static PlayerManager Instance { get; private set; }

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
            SteamMatchmaking.OnLobbyMemberJoined += OnLobbyMemberJoined;
            SteamMatchmaking.OnLobbyMemberLeave += OnLobbyMemberLeave;
            SteamMatchmaking.OnLobbyEntered += OnLobbyEntered;
            SteamMatchmaking.OnLobbyCreated += OnLobbyCreated;
        }

        // Update is called once per frame
        private void Update()
        {
        }

        private void OnDestroy()
        {
            SteamMatchmaking.OnLobbyMemberJoined -= OnLobbyMemberJoined;
            SteamMatchmaking.OnLobbyMemberLeave -= OnLobbyMemberLeave;
            SteamMatchmaking.OnLobbyEntered -= OnLobbyEntered;
            SteamMatchmaking.OnLobbyCreated -= OnLobbyCreated;
        }

        public Friend GetPlayer(SteamId steamID)
        {
            return _players[steamID];
        }

        public async Task<List<Friend>> GetAllPlayersAsync()
        {
            var result = new List<Friend>();
            await Task.Run(() =>
            {
                foreach (var player in _players) result.Add(player.Value);
            });
            return result;
        }

        private void OnLobbyCreated(Result result, Lobby lobby)
        {
            if (result == Result.OK) PlayersInitializing(lobby);
        }

        private void OnLobbyEntered(Lobby lobby)
        {
            if (NetworkManager.Singleton.IsHost)
                return;

            PlayersInitializing(lobby);
        }

        private void OnLobbyMemberLeave(Lobby lobby, Friend player)
        {
            _players.Remove(player.Id);
        }

        private void OnLobbyMemberJoined(Lobby lobby, Friend player)
        {
            _players.Add(player.Id, player);
        }

        private void PlayersInitializing(Lobby lobby)
        {
            _players.Clear();
            foreach (var player in lobby.Members) _players.Add(player.Id, player);
        }
    }
}