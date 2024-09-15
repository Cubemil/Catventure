using UnityEngine;

namespace Gameplay.MiniGames.FlappyCat
{
    public class PipeSpawnScript : MonoBehaviour
    {
        public GameObject pipe;
        private const float PipeOffset = 3;
        
        public float timeRate = 3;
        private float _timer;

        private void Start()
        {
            _timer = timeRate;
        }

        private void Update()
        {
            if (_timer < timeRate)
                _timer += Time.deltaTime;
            else
            {
                SpawnPipe();
                _timer = 0;
            }
        }

        private void SpawnPipe()
        {
            var lowestPoint= transform.position.y - PipeOffset;
            var highestPoint = transform.position.y + PipeOffset;
            
            Instantiate(pipe, new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), transform.position.z), transform.rotation);
        }
    }
}
