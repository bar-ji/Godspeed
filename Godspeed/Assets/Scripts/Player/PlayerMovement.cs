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
                         
        [Header("Airborn")] 
        [SerializeField] private float airForceMultiplier;

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

            if (Input.GetKey(KeyCode.Space) && IsGrounded())
                Jump();
            
            Movement();
        }
    
        private void Movement()
        {
            Vector3 rightDir = orientation.right * force * inputManager.xInput;
            Vector3 forwardDir = orientation.forward * force * inputManager.yInput;
            Vector3 dir = (forwardDir + rightDir).normalized;

            if (Mathf.Abs(Vector3.Angle(dir.normalized, VelNoY.normalized)) > 2.0f)
                force = accelerationOnAwake * responsiveness;
            else
                force = accelerationOnAwake;

            if (!IsGrounded())
                dir *= airForceMultiplier;

            rb.AddForce(dir * force);

            ClampVelocity();
        }

        void ClampVelocity()
        {
            var vel = Vector3.ClampMagnitude(VelNoY, currentMaxSpeed);
            rb.velocity = new Vector3(vel.x, rb.velocity.y, vel.z);
        }

        private void CounterMovement()
        {
            if (rb.velocity.magnitude > 0 && !IsMoving && IsGrounded())
            {
                Vector3 dir = -VelNoY;
                rb.AddForce(dir * drag);
            }
        }

        private void MaxSpeedHandler()
        {
            const float threshold = 0.2f;

            bool canIncreaseMaxVelocity = VelNoY.magnitude > currentMaxSpeed - threshold && VelNoY.magnitude < absMaxSpeed && !IsGrounded();
            
            if (canIncreaseMaxVelocity)
                currentMaxSpeedT += Time.deltaTime * Mathf.Pow(maxSpeedAcceleration, 2);
            else if (currentMaxSpeed > maxSpeedOnAwake)
                currentMaxSpeedT -= Time.deltaTime * Mathf.Pow(maxSpeedAcceleration, 2) * 8;
            
            if(rb.velocity.magnitude < currentMaxSpeed)
                currentMaxSpeedT -= Time.deltaTime * maxSpeedAcceleration * 16;

            currentMaxSpeedT = Mathf.Clamp(currentMaxSpeedT, 0, 1);
            
            currentMaxSpeed = Mathf.Lerp(maxSpeedOnAwake, absMaxSpeed, currentMaxSpeedT);
            currentMaxSpeed = Mathf.Clamp(currentMaxSpeed, maxSpeedOnAwake, absMaxSpeed);
        }

        private void Jump()
        {
            rb.velocity = VelNoY;
            rb.AddForce(Vector3.up * jumpForce);
        }

        private void Gravity()
        {
            rb.AddForce(Physics.gravity * gravityScale);
        }

        private void OnDrawGizmos()
        {
            Color col = IsGrounded() ? Color.green : Color.red;
            Gizmos.color = col;
            Gizmos.DrawRay(groundCheck.position, Vector3.down * groundDistance);
        }
        
        
        private bool IsMoving => inputManager.xInput != 0 || inputManager.yInput != 0;
        private bool IsMovingDiagonally => inputManager.xInput != 0 && inputManager.yInput != 0;
        private Vector3 VelNoY => new Vector3(rb.velocity.x, 0, rb.velocity.z);

        private bool IsGrounded()
        {
            //Centre
            if (Physics.Raycast(groundCheck.position, Vector3.down, groundDistance, groundMask))
                return true;
            if (Physics.Raycast(groundCheck.position + Vector3.forward * groundDistance, Vector3.down, groundDistance, groundMask))
                return true;
            if (Physics.Raycast(groundCheck.position - Vector3.forward * groundDistance, Vector3.down, groundDistance, groundMask))
                return true;
            if (Physics.Raycast(groundCheck.position + Vector3.right * groundDistance, Vector3.down, groundDistance, groundMask))
                return true;
            if (Physics.Raycast(groundCheck.position - Vector3.right * groundDistance, Vector3.down, groundDistance, groundMask))
                return true;
            return false;
        }
    }
}