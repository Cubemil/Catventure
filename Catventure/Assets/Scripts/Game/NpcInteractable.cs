using UnityEngine;

namespace Game
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
            //ChatBubble3D.Create(transform.transform, new Vector3(-.3f, 1.7f, 0f), "Hallo, wer bist denn du?");
            _animator.SetTrigger(Talk);
        }
    }
}