using UnityEngine;

namespace Gameplay.MiniGames.FlappyCat
{
    public class OutOfBounds : MonoBehaviour
    {
        public LogicScript logic;

        public BirdScript bird;
        // Start is called before the first frame update
        void Start()
        {
            logic= GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        }
        
    
        private void OnTriggerExit2D(Collider2D other)
        {
            Debug.Log("out");
            if (other.gameObject.layer == 3)
            {
                Debug.Log("Out of Bounds2");
                logic.GameOver();
                bird.birdIsAlive = false;
            }
        }
    }
}
