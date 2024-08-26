using UnityEngine;

namespace Flappy_Cat
{
    public class PipeMoveScript : MonoBehaviour
    {
        public float MoveSpeed = 3;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            transform.position = transform.position + (Vector3.left * MoveSpeed) * Time.deltaTime;

            if (transform.position.x < -15) 
            {
                Destroy(gameObject);
                Debug.Log("PipeDeleted");
            }
    
        }
    }
}
