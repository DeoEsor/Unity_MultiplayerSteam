using Player;
using Unity.Netcode;
using UnityEngine;

namespace Networking
{
    public class NetworkAnimatorSync : NetworkBehaviour
    {
        private Animator _animator;

        private readonly NetworkVariable<bool> _freefallAnimationBooleanNet = new(false,
            NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

        private int _freeFallAnimationHash;

        private int _groundAnimationHash;

        //= new (false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner)
        private readonly NetworkVariable<bool> _groundedAnimationBooleanNet = new(false,
            NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

        private readonly NetworkVariable<bool> _jumpAnimationBooleanNet =
            new(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);


        private int _jumpAnimationHash;

        private readonly NetworkVariable<float> _motionSpeedAnimationFloatNet =
            new(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

        private int _motionSpeedAnimationHash;
        private NetworkObject _networkObject;
        private bool _oldFreefallAnimationBoolean;
        private bool _oldGroundedAnimationBoolean;
        private bool _oldJumpAnimationBoolean;
        private float _oldMotionSpeedAnimationFloat;
        private float _oldSpeedAnimationFloat;


        private readonly NetworkVariable<float> _speedAnimationFloatNet =
            new(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

        private int _speedAnimationHash;
        private ThirdPersonController _thirdPersonController;

        // Start is called before the first frame update
        private void Start()
        {
            _thirdPersonController = GetComponent<ThirdPersonController>();
            _animator = GetComponent<Animator>();
            _networkObject = GetComponent<NetworkObject>();
            _groundAnimationHash = Animator.StringToHash("Grounded");
            _speedAnimationHash = Animator.StringToHash("Speed");
            _motionSpeedAnimationHash = Animator.StringToHash("MotionSpeed");
            _jumpAnimationHash = Animator.StringToHash("Jump");
            _freeFallAnimationHash = Animator.StringToHash("FreeFall");
        }

        // Update is called once per frame
        private void Update()
        {
            if (!_networkObject.IsLocalPlayer)
                UpdateAnimation();
            //Debug.Log(_jumpAnimationBooleanNet.Value);
            //_animator.SetBool(_jumpAnimationHash, true);
            else
                GetFromController();
        }

        private void UpdateAnimation()
        {
            _animator.SetBool(_jumpAnimationHash, _jumpAnimationBooleanNet.Value);

            _animator.SetBool(_freeFallAnimationHash, _freefallAnimationBooleanNet.Value);

            _animator.SetBool(_groundAnimationHash, _groundedAnimationBooleanNet.Value);

            _animator.SetFloat(_motionSpeedAnimationHash, _motionSpeedAnimationFloatNet.Value);

            _animator.SetFloat(_speedAnimationHash, _speedAnimationFloatNet.Value);
        }

        private void GetFromController()
        {
            if (_oldGroundedAnimationBoolean != _thirdPersonController.GroundedAnimationForNet)
            {
                _groundedAnimationBooleanNet.Value = _thirdPersonController.GroundedAnimationForNet;
                _oldGroundedAnimationBoolean = _thirdPersonController.GroundedAnimationForNet;
            }

            if (_oldSpeedAnimationFloat != _thirdPersonController.SpeedAnimationFloatForNet)
            {
                _speedAnimationFloatNet.Value = _thirdPersonController.SpeedAnimationFloatForNet;
                _oldSpeedAnimationFloat = _thirdPersonController.SpeedAnimationFloatForNet;
            }

            if (_oldMotionSpeedAnimationFloat != _thirdPersonController.MotionSpeedAnimationFloatForNet)
            {
                _motionSpeedAnimationFloatNet.Value = _thirdPersonController.MotionSpeedAnimationFloatForNet;
                _oldMotionSpeedAnimationFloat = _thirdPersonController.MotionSpeedAnimationFloatForNet;
            }

            if (_oldJumpAnimationBoolean != _thirdPersonController.JumpAnimationForNet)
            {
                _jumpAnimationBooleanNet.Value = _thirdPersonController.JumpAnimationForNet;
                _oldJumpAnimationBoolean = _thirdPersonController.JumpAnimationForNet;
            }

            if (_oldFreefallAnimationBoolean != _thirdPersonController.FreefallAnimationForNet)
            {
                _freefallAnimationBooleanNet.Value = _thirdPersonController.FreefallAnimationForNet;
                _oldFreefallAnimationBoolean = _thirdPersonController.FreefallAnimationForNet;
            }
        }
    }
}