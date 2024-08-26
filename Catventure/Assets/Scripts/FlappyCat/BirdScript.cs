using UnityEngine;

namespace FlappyCat
{
    public class BirdScript : MonoBehaviour
    {
        public Rigidbody2D myRigidbody2D;
        public BoxCollider2D myCircleCollider2D;
        public float flapStrength =6;
        public LogicScript logic;
        public bool birdIsAlive = true;
        public float rotationSpeed = -0.1F;
    
    
        // Start is called before the first frame update
        void Start()
        { 
            gameObject.name = "Kafka";
            logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
            transform.rotation.Set(0, 0, 0, 0);
            gameObject.name = "Kafka";
        }

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(new Vector3(0, 0, rotationSpeed));
            if (Input.GetKeyDown(KeyCode.Space) && birdIsAlive)
            {
                myRigidbody2D.velocity = Vector2.up * flapStrength;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            logic.GameOver();
            birdIsAlive = false;
            rotationSpeed = 1;
            myRigidbody2D.gravityScale =4 ;
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            Debug.Log("Out");
            if (other.gameObject.layer == 6)
            {
                logic.GameOver();
                birdIsAlive = false;
            }
        }
    }
}
