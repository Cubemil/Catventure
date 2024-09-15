using UnityEngine;

namespace Gameplay.MiniGames.FlappyCat
{
    public class PointsScript : MonoBehaviour
    {
        public GameObject gameOverScreen;
        public LogicScript logic;

        private void Start()
        {
            logic= GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == 3)
                logic.AddScore(1);
        }
    }
}
