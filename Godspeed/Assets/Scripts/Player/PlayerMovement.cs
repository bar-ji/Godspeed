using Management;
using TMPro;
using UnityEngine;

namespace Player
{
    public enum MovementState
    {
        Walking, Airborn, Crouching, Sliding
    }
    
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Walking")]
        [SerializeField] private float force;
        [SerializeField] private float responsiveness;
        [SerializeField] private float drag;
        [SerializeField] private float absMaxSpeed;
                         public float currentMaxSpeed { get; private set; }
        [SerializeField] private float maxSpeedAcceleration;
                         private float maxSpeedOnAwake;
                         private float accelerationOnAwake;
                         private float currentMaxSpeedT;

        [Header("Jumping")]
        [SerializeField] private float groundDistance;
        [SerializeField] private float jumpForce;
        [SerializeField] private float gravityScale;
        [SerializeField] private LayerMask groundMask;

        [Header("Sliding")] 
        [SerializeField] private float slideForce;
        [SerializeField] private float slideActuation;
        

        [Header("References")]
        [SerializeField] private Transform orientation;
        [SerializeField] private Transform groundCheck;
        [SerializeField] private CapsuleCollider collider;
                         private Rigidbody rb;
                         private InputManager inputManager;

                         private MovementState state;
                         
        private bool isMoving => Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0;
        private bool isGrounded => Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        private bool isCrouching;
        private bool isSliding;

        public MovementState currentState { get; private set; }
        
        public Vector3 previousPosition { get; private set; }
    
        void Awake()
        {
            currentMaxSpeed = absMaxSpeed / 2;
            accelerationOnAwake = force;
            maxSpeedOnAwake = currentMaxSpeed;
        }
        
        private void Start()
        {
            inputManager = InputManager.instance;
            rb = GetComponent<Rigidbody>();
        }
    
        void Update()
        {
            state = isGrounded ? MovementState.Walking : MovementState.Airborn;
            previousPosition = transform.position;
        }   
        private void FixedUpdate()
        {
            CounterMovement();
            Gravity();
            
            if (GameManager.instance.pauseMenu.isPaused) return;

            if (Input.GetKey(KeyCode.Space) && isGrounded)
                Jump();
            
            if (Input.GetKeyDown(KeyCode.LeftControl))
                StartCrouch();
            
            if (Input.GetKeyUp(KeyCode.LeftControl))
                StopCrouch();
            
            if(!isSliding)
                Movement();
        }
    
        private void Movement()
        {
            Vector3 rightDir = orientation.right * force * inputManager.xInput;
            Vector3 forwardDir = orientation.forward * force * inputManager.yInput;
            Vector3 dir = (forwardDir + rightDir).normalized;
            Vector3 velNoY = new Vector3(rb.velocity.x, 0, rb.velocity.z);

            if (Mathf.Abs(Vector3.Angle(dir.normalized, velNoY.normalized)) > 2.0f)
                force = accelerationOnAwake * responsiveness;
            else
                force = accelerationOnAwake;

            if (velNoY.magnitude > currentMaxSpeed - 0.2f && velNoY.magnitude < absMaxSpeed && !isGrounded)
                currentMaxSpeedT += Time.fixedDeltaTime * maxSpeedAcceleration;
            else if (currentMaxSpeed > maxSpeedOnAwake)
                currentMaxSpeedT -= Time.fixedDeltaTime * maxSpeedAcceleration * 8;

            currentMaxSpeedT = Mathf.Clamp(currentMaxSpeedT, 0, 1);
            currentMaxSpeed = Mathf.Lerp(maxSpeedOnAwake, absMaxSpeed, currentMaxSpeedT);
        
            if (currentMaxSpeed > absMaxSpeed)
                currentMaxSpeed = absMaxSpeed;
            if (currentMaxSpeed < maxSpeedOnAwake)
                currentMaxSpeed = maxSpeedOnAwake;

            rb.AddForce(dir * force);
            
            Vector3 vel = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            vel = Vector3.ClampMagnitude(vel, currentMaxSpeed);
            rb.velocity = new Vector3(vel.x, rb.velocity.y, vel.z);
        }

        void StartCrouch()
        {
            isCrouching = true;
            collider.height /= 2;
            if (rb.velocity.magnitude >= slideActuation)
            {
                rb.AddForce(rb.velocity.normalized * slideForce);
                isSliding = true;
            }
        }
        
        void StopCrouch()
        {
            isCrouching = false;
            isSliding = false;
            collider.height *= 2;
        }
        
        private void CounterMovement()
        {
            if (rb.velocity.magnitude > 0 && !isMoving && isGrounded)
            {
                Vector3 dir = new Vector3(-rb.velocity.x, 0, -rb.velocity.z);
                rb.AddForce(dir * drag);
            }
        }

        private void Jump()
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(Vector3.up * jumpForce);
        }

        private void Gravity()
        {
            rb.AddForce(Physics.gravity * gravityScale);
        }

        private void OnDrawGizmos()
        {
            Color col = isGrounded ? Color.green : Color.red;
            Gizmos.color = col;
            Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
        }
    }
}