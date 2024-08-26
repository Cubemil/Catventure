using UnityEngine;

namespace FlappyCat
{
    public class WingScript : MonoBehaviour
    {

        public Animator wingAnimator;
        // Start is called before the first frame update
        void Start()
        {
            wingAnimator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                wingAnimator.SetTrigger("Flap");
            }
        
        }
    }
}
