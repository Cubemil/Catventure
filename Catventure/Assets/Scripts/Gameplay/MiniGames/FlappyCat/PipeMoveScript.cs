using UnityEngine;

namespace Gameplay.MiniGames.FlappyCat
{
    public class PipeMoveScript : MonoBehaviour
    {
        public float moveSpeed = 3;

        private void Update()
        {
            transform.position += Vector3.left * (moveSpeed * Time.deltaTime);

            if (!(transform.position.x < -15)) return;
            Destroy(gameObject);
        }
    }
}
