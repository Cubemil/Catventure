using UnityEngine;

namespace Gameplay.MiniGames.FlappyCat
{
    public class OutOfBounds : MonoBehaviour
    {
        public LogicScript logic;
        public BirdScript bird;
        
        private void Start()
        {
            logic= GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        }
    
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.layer != 3) return;
            
            Debug.Log("Out of Bounds2");
            logic.GameOver();
            bird.birdIsAlive = false;
        }
    }
}
