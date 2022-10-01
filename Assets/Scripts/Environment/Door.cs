using Infrastructure.Environment;
using UnityEngine;

namespace Environment
{
    public class Door : MonoBehaviour, IInteractable
    {
        [SerializeField] private Animator mAnimator;
        [SerializeField] private bool isOpen;
        private static readonly int IsOpen = Animator.StringToHash("isOpen");
        
        [SerializeField] private string descriptionOnEnabled = "Press [E] to <color=green>open</color> the door.";
        [SerializeField] private string descriptionOnDisable = "Press [E] to <color=red>close</color> the door.";

        private void Start()
        {
            if (isOpen)
                mAnimator.SetBool(IsOpen, true);
        }

        public string GetDescription() 
            => isOpen ? descriptionOnDisable : descriptionOnEnabled;

        public void Interact()
        {
            isOpen = !isOpen;
            mAnimator.SetBool(IsOpen, isOpen);
        }
    }
}