using Networking;
using Networking.Steam;
using Steamworks;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject _menu;
        private Button _disconnect;
        private Button _exit;

        private bool _isMenuOn;
        private bool _isMenuOnOld;
        private PlayerInput _localPlayerInput;
        private RawImage _playerAvatar;
        private Text _playerName;
        private Button _starthost;
        private Keyboard keyboard;

        private void Start()
        {
            keyboard = Keyboard.current;
            _isMenuOn = true;
            _isMenuOnOld = true;
            MainMenuInitializing();
            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnectedCallback;
            DontDestroyOnLoad(gameObject);
        }

        // Update is called once per frame
        private void Update()
        {
            CheckRunningGame();
            MenuUpdate();
        }

        private void OpenCloseMenuUpdate()
        {
            if (_isMenuOn != _isMenuOnOld)
            {
                if (_isMenuOn)
                {
                    _isMenuOnOld = true;
                    Cursor.lockState = CursorLockMode.None;
                    SetLocalPlayerInput(false);
                }
                else
                {
                    _isMenuOnOld = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    SetLocalPlayerInput(true);
                }
            }

            Cursor.visible = _isMenuOn;
            _menu.SetActive(_isMenuOn);
        }

        private async void InitializingPlayerAvatar()
        {
            if (!SteamClient.IsValid) return;
            var img = await SteamFriends.GetLargeAvatarAsync(SteamClient.SteamId);
            _playerAvatar.texture = img.Value.Convert();
        }

        private PlayerInput FindLocalPlayerInput()
        {
            var players = GameObject.FindGameObjectsWithTag("Player");
            foreach (var player in players)
                if (player.GetComponent<NetworkObject>().IsLocalPlayer)
                    return player.GetComponent<PlayerInput>();
            return null;
        }

        private void SetLocalPlayerInput(bool boolean)
        {
            if (_localPlayerInput == null) _localPlayerInput = FindLocalPlayerInput();
            if (_localPlayerInput != null)
                _localPlayerInput.enabled = boolean;
        }

        private void MenuUpdate()
        {
            if (keyboard.escapeKey.wasPressedThisFrame &&
                (NetworkManager.Singleton.IsHost || NetworkManager.Singleton.IsClient))
                _isMenuOn = !_isMenuOn;
            OpenCloseMenuUpdate();
        }

        private void CheckRunningGame()
        {
            if (!(NetworkManager.Singleton.IsHost || NetworkManager.Singleton.IsClient)) _isMenuOn = true;
        }

        private void MainMenuInitializing()
        {
            if (!_menu.transform.Find("Main menu").Find("ButtonStartHost").TryGetComponent(out _starthost))
                Debug.LogError("ButtonStartHost not found", this);
            else
                _starthost.onClick.AddListener(() =>
                {
                    if (!(NetworkManager.Singleton.IsHost || NetworkManager.Singleton.IsClient))
                    {
                        GameNetworkManager.Instance.StartHost();
                        _isMenuOn = false;
                    }
                });

            if (!_menu.transform.Find("Main menu").Find("ButtonDisconnect").TryGetComponent(out _disconnect))
                Debug.LogError("ButtonDisconnect not found", this);
            else
                _disconnect.onClick.AddListener(() =>
                {
                    if (NetworkManager.Singleton.IsClient || NetworkManager.Singleton.IsHost)
                        GameNetworkManager.Instance.Disconnect();
                });

            if (!_menu.transform.Find("Main menu").Find("ButtonExit").TryGetComponent(out _exit))
                Debug.LogError("ButtonExit not found", this);
            else
                _exit.onClick.AddListener(() => { Application.Quit(); });

            if (!_menu.transform.Find("Main menu").Find("PlayerAvatar").TryGetComponent(out _playerAvatar))
                Debug.LogError("PlayerAvatar not found", this);
            else
                InitializingPlayerAvatar();

            if (!_menu.transform.Find("Main menu").Find("PlayerName").TryGetComponent(out _playerName))
                Debug.LogError("PlayerName not found", this);
            else
                _playerName.text = SteamClient.Name;
        }

        private void OnClientConnectedCallback(ulong clientId)
        {
            _isMenuOn = false;
        }
    }
}