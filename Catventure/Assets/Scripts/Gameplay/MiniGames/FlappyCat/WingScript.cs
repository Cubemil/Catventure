using UnityEngine;

namespace Gameplay.MiniGames.FlappyCat
{
    public class WingScript : MonoBehaviour
    {
        public Animator wingAnimator;
        private static readonly int Flap = Animator.StringToHash("Flap");

        private void Start()
        {
            wingAnimator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                wingAnimator.SetTrigger(Flap);
        }
    }
}
