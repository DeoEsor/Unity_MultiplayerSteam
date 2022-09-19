using Unity.Netcode;
using UnityEngine;

namespace Networking
{
    public class NetworkTransformSync : NetworkBehaviour
    {
        private NetworkObject _networkObject;

        private Vector3 _oldPosition;
        private Quaternion _oldRotation;

        private readonly NetworkVariable<Vector3> _position = new(default, NetworkVariableReadPermission.Everyone,
            NetworkVariableWritePermission.Owner);

        private readonly NetworkVariable<Quaternion> _rotation = new(default, NetworkVariableReadPermission.Everyone,
            NetworkVariableWritePermission.Owner);

        private Transform _transform;

        // Start is called before the first frame update
        private void Start()
        {
            _transform = GetComponent<Transform>();
            _networkObject = GetComponent<NetworkObject>();
        }

        // Update is called once per frame
        private void Update()
        {
            if (_networkObject.IsLocalPlayer &&
                (_transform.position != _oldPosition || _transform.rotation != _oldRotation))
            {
                _position.Value = _transform.position;
                _rotation.Value = _transform.rotation;
                _oldPosition = _transform.position;
                _oldRotation = _transform.rotation;
            }
            else if (_position.Value != _transform.position &&
                     _rotation.Value != _transform.rotation)
            {
                _transform.position = _position.Value;
                _transform.rotation = _rotation.Value;
            }
        }
    }
}