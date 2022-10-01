using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerInitializer : MonoBehaviour
    {
        [FormerlySerializedAs("PlayerCameraRoot")] [SerializeField] 
        private GameObject playerCameraRoot;

        private void Awake()
        {
            transform.Find("Player Info").gameObject.SetActive(false);
        }

        private void Start()
        {
            if (!GetComponent<NetworkObject>().IsLocalPlayer)
            {
                GetComponent<ThirdPersonController>().enabled = false;
                transform.Find("Player Info").gameObject.SetActive(true);
            }
            else
            {
                var playerFollowCamera = GameObject.Find("PlayerFollowCamera");
                playerFollowCamera.GetComponent<CinemachineVirtualCamera>().Follow = playerCameraRoot.transform;

                GetComponent<PlayerInput>().enabled = true;
            }
        }
    }
}