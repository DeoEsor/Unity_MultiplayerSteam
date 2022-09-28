using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Transform))]
    public class PlayerInfoFollow : MonoBehaviour
    {
        private Transform _mainCameraTransform;

        private void Start()
        {
            _mainCameraTransform = GameObject.Find("MainCamera").transform;
        }

        private void Update()
        {
            transform.LookAt(_mainCameraTransform);
            transform.rotation = _mainCameraTransform.rotation;
        }
    }
}