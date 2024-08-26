using UnityEngine;

namespace FlappyCat
{
    public class PointsScript : MonoBehaviour
    {
        public GameObject gameOverScreen;
        public LogicScript logic;


        // Start is called before the first frame update
        void Start()
        {
            logic= GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        }
    

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == 3)
            {
                logic.AddScore(1);
            }
        }
    }
}
