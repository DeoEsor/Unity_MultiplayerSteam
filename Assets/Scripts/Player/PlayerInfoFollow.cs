using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Transform))]
    public class PlayerInfoFollow : MonoBehaviour
    {
        private Transform _mainCamera;

        private void Start()
        {
            _mainCamera = GameObject.Find("MainCamera").transform;
        }

        private void Update()
        {
            transform.LookAt(_mainCamera);
            transform.rotation = _mainCamera.transform.rotation;
        }
    }
}