using Infrastructure.Environment;
using UnityEngine;

namespace Environment
{
    public class LightSwitch : MonoBehaviour, IInteractable
    {
        [SerializeField]
        private Light mLight;
        private bool _isOn;

        [SerializeField] private string descriptionOnEnabled = "Press [E] to turn <color=green>on</color> the light.";
        [SerializeField] private string descriptionOnDisable = "Press [E] to turn <color=red>off</color> the light.";

        private void Start()
        {
            mLight.enabled = _isOn;
        }

        public string GetDescription()
        {
            return _isOn ? descriptionOnDisable : descriptionOnEnabled;
        }

        public void Interact()
        {
            _isOn = !_isOn;
            mLight.enabled = _isOn;
        }
    }
}