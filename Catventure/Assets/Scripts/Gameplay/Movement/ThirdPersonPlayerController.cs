using UnityEngine;

namespace Gameplay.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Animator))]
    public class ThirdPersonPlayerController : MonoBehaviour
    {
        [SerializeField] private float walkSpeed = 2.5f;
        [SerializeField] public float speed = 2.5f;
        [SerializeField] private float runSpeed = 5f;
        [SerializeField] private float rotationSpeed = 720f; // Degrees per second
        [SerializeField] private float jumpForce = 7f;
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private LayerMask groundLayer;

        private Rigidbody _rigidBody;
        private Animator _animator;
        private Vector3 _inputDirection;
        private bool _isGrounded = true;

        private static readonly int IsRunning = Animator.StringToHash("isRunning");
        private static readonly int IsWalking = Animator.StringToHash("isWalking");

        private void Start()
        {
            _rigidBody = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>();

            // Lock only the X and Z rotations
            _rigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

            if (!cameraTransform)
            {
                cameraTransform = Camera.main?.transform;
                if (!cameraTransform)
                {
                    Debug.LogError("Camera Transform is not assigned and no Main Camera found in the scene.");
                    return;
                }
            }
            
            enabled = false;
        }

        private void Update()
        {
            GatherInput();
            HandleMovement();
            HandleRotation();
            HandleJump();
            UpdateAnimator();
        }

        private void GatherInput()
        {
            var moveHorizontal = Input.GetAxis("Horizontal");
            var moveVertical = Input.GetAxis("Vertical");
            _inputDirection = new Vector3(moveHorizontal, 0, moveVertical).normalized;
        }

        private void HandleMovement()
        {
            var currentSpeed = Input.GetButton("Run") ? runSpeed : walkSpeed;

            var movement = GetCameraRelativeDirection(_inputDirection) * (currentSpeed * Time.deltaTime);
            _rigidBody.MovePosition(transform.position + movement);
        }

        private void HandleRotation()
        {
            if (!(_inputDirection.magnitude > 0)) return;

            var targetDirection = GetCameraRelativeDirection(_inputDirection);
            var targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        private void HandleJump()
        {
            // Perform a ray cast slightly below the player's position to check if grounded
            //_isGrounded = Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, 0.2f, groundLayer);
            _isGrounded = Physics.CheckSphere(transform.position + Vector3.up * 0.1f, 0.2f, groundLayer);

            if (_isGrounded && Input.GetButtonDown("Jump"))
            {
                _rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }

        private void UpdateAnimator()
        {
            var isWalking = _inputDirection.magnitude > 0;
            _animator.SetBool(IsWalking, isWalking);
            _animator.SetBool(IsRunning, isWalking && Input.GetButton("Run"));
        }

        private Vector3 GetCameraRelativeDirection(Vector3 inputDirection)
        {
            var forward = cameraTransform.forward;
            forward.y = 0;
            forward.Normalize();

            var right = cameraTransform.right;
            right.y = 0;
            right.Normalize();

            return (forward * inputDirection.z + right * inputDirection.x).normalized;
        }
    }
}
