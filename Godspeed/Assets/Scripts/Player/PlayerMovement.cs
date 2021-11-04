using Management;
using TMPro;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Walking")]
        [SerializeField] private float force;
        [SerializeField] private float responsiveness;
        [SerializeField] private float drag;
        [SerializeField] private float maxSpeedAcceleration;
        [SerializeField] private float absMaxSpeed;
                         public float currentMaxSpeed;
                         private float maxSpeedOnAwake;
                         private float accelerationOnAwake;
                         private float currentMaxSpeedT;

        [Header("Jumping")]
        [SerializeField] private float groundDistance;
        [SerializeField] private float jumpForce;
        [SerializeField] private float gravityScale;
        [SerializeField] private LayerMask groundMask;


        [Header("References")]
        [SerializeField] private Transform orientation;
        [SerializeField] private Transform groundCheck;
                         private Rigidbody rb;
                         private InputManager inputManager;

                         private int climbJumpsRemaining = 1;

        private bool isGrounded => Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        private bool canClimbJump => Physics.Raycast(groundCheck.position, orientation.forward, groundDistance, groundMask);
        private bool isMoving => inputManager.xInput != 0 || inputManager.yInput != 0;
        private bool movingDiagonally => inputManager.xInput != 0 && inputManager.yInput != 0;
        private Vector3 velNoY => new Vector3(rb.velocity.x, 0, rb.velocity.z);

        void Awake()
        {
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
            MaxSpeedHandler();
        }   
        private void FixedUpdate()
        {
            CounterMovement();
            Gravity();
            
            if (GameManager.instance.pauseMenu.isPaused) return;

            if (Input.GetKey(KeyCode.Space) && isGrounded)
                Jump();
            
            Movement();
        }
    
        private void Movement()
        {
            Vector3 rightDir = orientation.right * force * inputManager.xInput;
            Vector3 forwardDir = orientation.forward * force * inputManager.yInput;
            Vector3 dir = (forwardDir + rightDir).normalized;

            if (Mathf.Abs(Vector3.Angle(dir.normalized, velNoY.normalized)) > 2.0f)
                force = accelerationOnAwake * responsiveness;
            else
                force = accelerationOnAwake;

            rb.AddForce(dir * force);

            ClampVelocity();
        }

        void ClampVelocity()
        {
            Vector3 vel = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            vel = Vector3.ClampMagnitude(vel, currentMaxSpeed);
            rb.velocity = new Vector3(vel.x, rb.velocity.y, vel.z);
        }

        private void CounterMovement()
        {
            if (rb.velocity.magnitude > 0 && !isMoving && isGrounded)
            {
                Vector3 dir = -velNoY;
                rb.AddForce(dir * drag);
            }
        }

        private void MaxSpeedHandler()
        {
            const float threshold = 0.2f;

            bool canIncreaseMaxVelocity = velNoY.magnitude > currentMaxSpeed - threshold && velNoY.magnitude < absMaxSpeed && !isGrounded && movingDiagonally;
            if (canIncreaseMaxVelocity)
                currentMaxSpeedT += Time.deltaTime * maxSpeedAcceleration;
            else if (currentMaxSpeed > maxSpeedOnAwake && !isMoving)
                currentMaxSpeedT -= Time.deltaTime * maxSpeedAcceleration * 8;
            
            if(rb.velocity.magnitude < currentMaxSpeed)
                currentMaxSpeedT -= Time.deltaTime * maxSpeedAcceleration * 16;

            currentMaxSpeedT = Mathf.Clamp(currentMaxSpeedT, 0, 1);
            currentMaxSpeed = Mathf.Lerp(maxSpeedOnAwake, absMaxSpeed, currentMaxSpeedT);
        
            if (currentMaxSpeed > absMaxSpeed)
                currentMaxSpeed = absMaxSpeed;
            if (currentMaxSpeed < maxSpeedOnAwake)
                currentMaxSpeed = maxSpeedOnAwake;
        }

        private void Jump()
        {
            rb.velocity = velNoY;
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