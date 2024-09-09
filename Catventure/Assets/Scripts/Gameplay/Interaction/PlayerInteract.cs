using UnityEngine;

namespace Gameplay.Interaction
{
    public class PlayerInteract : MonoBehaviour
    {
        [SerializeField] private float interactRange = 6f;
    
        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.E)) return;
            
            // gets all collisions inside sphere with radius of interactRange
            var colliders = Physics.OverlapSphere(transform.position, interactRange);
            foreach (var col in colliders)
            {
                if (col.TryGetComponent(out NpcInteractable npcInteractable))
                {
                    npcInteractable.Interact();
                }

                if (col.TryGetComponent(out ItemInteractable itemInteractable))
                {
                    itemInteractable.Interact();
                }
            }
        }

    }
}
