using UnityEngine;

// namespace for consistency and error handling
namespace Game
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Rigidbody rb;
        [SerializeField] public float speed = 8f;
        [SerializeField] private float walkSpeed = 8f;
        [SerializeField] private float runSpeed = 16f;
        [SerializeField] private float rotationSpeed = 500;
        [SerializeField] private float jumpForce = 5.0f;

        private Vector3 _input;
        private bool _isOnGround = true;
        private Animator _animator;
        
        /*  basically the same as checking for "isRunning/isWalking"
            but with readonly properties for consistency */
        private static readonly int IsRunning = Animator.StringToHash("isRunning");
        private static readonly int IsWalking = Animator.StringToHash("isWalking");

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>();

            // lock rotation
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }

        private void Update()
        {
            // get horizontal/vertical axis from input => Vec3
            GatherInput();
            // change controller look direction and move player after
            Look();
            Move();
        }

        void GatherInput()
        {
            var moveHorizontal = Input.GetAxisRaw("Horizontal");
            var moveVertical = Input.GetAxisRaw("Vertical");
            _input = new Vector3(moveHorizontal, 0, moveVertical);
        }

        private void Look()
        {
            if (_input == Vector3.zero) return;

            // calc rotation with quaternion, vec3.toIso => translates isometric axes to camera viewpoint 
            var rot = Quaternion.LookRotation(_input.ToIso(), Vector3.up);
            // lock rotation to y-axis only
            rot = Quaternion.Euler(0, rot.eulerAngles.y, 0);
            // change player rotation matrix
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, rotationSpeed * Time.deltaTime);
        }

        void Move()
        {
            if (!_animator) return;
            _animator.SetBool(IsWalking, _input != Vector3.zero);
            var currentSpeed = walkSpeed;
            
            if (_input != Vector3.zero && Input.GetButton("Run"))
            {
                _animator.SetBool(IsRunning, true);
                currentSpeed = runSpeed;
            }
            else
            {
                _animator.SetBool(IsRunning, false);
            }
            
            if (Input.GetButtonDown("Jump") && _isOnGround)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                _isOnGround = false;
            }

            // move player
            var move = transform.forward * (_input.normalized.magnitude * currentSpeed * Time.deltaTime);
            rb.MovePosition(rb.position + move);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                _isOnGround = true;
            }
        }

        private void OnCollisionExit(Collision other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                _isOnGround = false;
            }
        }
        
    }
}
