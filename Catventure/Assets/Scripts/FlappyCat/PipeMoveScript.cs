using UnityEngine;

namespace FlappyCat
{
    public class PipeMoveScript : MonoBehaviour
    {
        public float MoveSpeed = 3;


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
