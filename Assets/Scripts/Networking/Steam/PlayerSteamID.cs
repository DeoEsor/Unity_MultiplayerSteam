using Steamworks;
using Unity.Netcode;

namespace Networking.Steam
{
    public class PlayerSteamID : NetworkBehaviour
    {
        private readonly NetworkVariable<ulong> _steamIDNet = new();

        public SteamId SteamID => _steamIDNet.Value;

        private void Start()
        {
            if (GetComponent<NetworkObject>().IsLocalPlayer)
                SetSteamIDServerRpc(SteamClient.SteamId.Value);
        }

        [ServerRpc]
        private void SetSteamIDServerRpc(ulong steamIDNum) 
            => _steamIDNet.Value = steamIDNum;
    }
}