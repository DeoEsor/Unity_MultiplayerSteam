using Infrastructure.Environment;
using Infrastructure.Player;
using TMPro;
using UnityEngine;

namespace Core.Core
{
    public abstract class InteractionBase :  IInteraction<InteractionBase>
    {
        [SerializeField]  private Camera _mainCam;
        [SerializeField] private readonly float interactionDistance = 10f;

        [SerializeField] private GameObject interactionUI;
        [SerializeField] private TextMeshProUGUI interactionText;

        private void Update()
        {
            InteractionRay();
        }

        public abstract InteractionBase GetInteractData();

        public void InteractionRay()
        {
            var ray = _mainCam.ViewportPointToRay(Vector3.one / 2f);

            if (!Physics.Raycast(ray, out var hit, interactionDistance))
            {
                interactionUI.SetActive(false);
                return;
            }
            
            var interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                interactionText.text = interactable.GetDescription();

                if (Input.GetKeyDown(KeyCode.E)) 
                    interactable.Interact();
            }
            
            interactionUI.SetActive(true);
        }
    }
}