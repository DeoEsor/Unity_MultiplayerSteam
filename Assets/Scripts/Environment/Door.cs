using Infrastructure.Environment;
using UnityEngine;

namespace Environment
{
    public class Door : MonoBehaviour, IInteractable
    {
        [SerializeField] private Animator mAnimator;
        [SerializeField] private bool isOpen;
        [SerializeField] private string animationName = "isOpen";
        private int _isOpen;
        
        [SerializeField] private string descriptionOnEnabled = "Press [E] to <color=green>open</color> the door.";
        [SerializeField] private string descriptionOnDisable = "Press [E] to <color=red>close</color> the door.";

        private void Start()
        {
            _isOpen = Animator.StringToHash(animationName);
            if (isOpen)
                mAnimator.SetBool(_isOpen, true);
        }

        public string GetDescription() 
            => isOpen ? descriptionOnDisable : descriptionOnEnabled;

        public void Interact()
        {
            isOpen = !isOpen;
            mAnimator.SetBool(_isOpen, isOpen);
        }
    }
}