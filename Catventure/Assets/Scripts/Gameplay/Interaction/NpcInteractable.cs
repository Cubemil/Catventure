using UnityEngine;

namespace Gameplay.Interaction
{
    public class NpcInteractable : MonoBehaviour
    {
        private Animator _animator;
        private static readonly int Talk = Animator.StringToHash("Talk");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void Interact()
        {
            //_animator.SetTrigger(Talk);
        }
    }
}