using UnityEngine;

namespace Gameplay.Movement
{
    //TODO order of move operations in update
    //TODO jump animation
    public class IsometricPlayerController : MonoBehaviour
    {
        [SerializeField] private Rigidbody rb;
        [SerializeField] private float walkSpeed;
        [SerializeField] public float runSpeed;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private float jumpForce;
        [SerializeField] private float groundCheckDistance = 1f;

        private Vector3 _input;
        private bool _isOnGround;
        private Animator _animator;
        
        // storing animator states in readonly constants
        private static readonly int IsRunning = Animator.StringToHash("isRunning");
        private static readonly int IsWalking = Animator.StringToHash("isWalking");
        private static readonly int IsJumping = Animator.StringToHash("isJumping");
        private static readonly int IsHitting = Animator.StringToHash("isHitting");

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>();

            // lock rotation
            rb.constraints = (RigidbodyConstraints)80; //== RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
        }

        private void Update()
        {
            // get horizontal/vertical axis from input => Vector3
            GatherInput();
            
            // check if player is on ground with raycasting
            CheckGroundStatus();
            
            // change controller look direction and move player after
            Look();
            Move();
            HandleHit();
        }

        private void GatherInput()
        {
            var moveHorizontal = Input.GetAxisRaw("Horizontal");
            var moveVertical = Input.GetAxisRaw("Vertical");
            _input = new Vector3(moveHorizontal, 0, moveVertical);
        }

        private void Look()
        {
            if (_input == Vector3.zero) return;

            // calc rotation with quaternion, Vector3.toIso => translates isometric axes to camera view point 
            var rot = Quaternion.LookRotation(_input.ToIso(), Vector3.up);
            // lock rotation to y-axis only
            rot = Quaternion.Euler(0, rot.eulerAngles.y, 0);
            // change player rotation matrix
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, rotationSpeed * Time.deltaTime);
        }

        private void Move()
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

            if (Input.GetButtonDown("Jump") && _isOnGround && !_animator.GetCurrentAnimatorStateInfo(0).IsName("CatSleeping"))
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                _isOnGround = false;
                _animator.SetBool(IsJumping, true);
            }

            // move player
            var move = transform.forward * (_input.normalized.magnitude * currentSpeed * Time.deltaTime);
            rb.MovePosition(rb.position + move);
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private void CheckGroundStatus()
        {
            // bitmask excluding layer NotWalkable
            var notWalkableLayer = LayerMask.GetMask("NotWalkable");
            // invert mask to check all layers except NotWalkable
            var layerMask = ~notWalkableLayer;
            
            // raycast checking if there's a surface underneath (except for NotWalkable Layers)
            if (Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, layerMask))
            {
                //Debug.Log("Standing on: " + hit.collider.gameObject.name);
                _isOnGround = true;
                _animator.SetBool(IsJumping, false);
            }
            else
            {
                _isOnGround = false;
                _animator.SetBool(IsJumping, true);
            }
        }
        
        private void HandleHit()
        {
            if (Input.GetMouseButtonDown(0))
                _animator.SetBool(IsHitting, true);
            else if (!Input.GetMouseButtonDown(0))
                _animator.SetBool(IsHitting, false);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.CompareTag("Ground")) return;
            
            _isOnGround = true;
            _animator.SetBool(IsJumping, false);
        }
    }
}
