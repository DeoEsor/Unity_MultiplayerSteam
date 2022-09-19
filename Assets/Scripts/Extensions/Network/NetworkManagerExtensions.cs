using Unity.Netcode;

namespace Extensions.Network
{
    public static class NetworkManagerExtensions
    {
        public static bool IsConnectedAndListening(this NetworkManager networkManager)
        {
            return networkManager.IsConnectedClient && !networkManager.IsListening;
        }
    }
}