using Management;
using TMPro;
using UnityEngine;

namespace Player
{
    public enum MovementState
    {
        Walking, Airborn
    }
    
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Walking")]
        [SerializeField] private float force;
        [SerializeField] private float responsiveness;
        [SerializeField] private float drag;
        [SerializeField] private float absMaxSpeed;
        [SerializeField] private float currentMaxSpeed;
        [SerializeField] private float maxSpeedAcceleration;
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
        [SerializeField] private TMP_Text velocityText;
                         private Rigidbody rb;

                         private MovementState state;
                         
        private bool isMoving => Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0;
        private bool isGrounded => Physics.Raycast(transform.position, Vector3.down, groundDistance, groundMask);
        
        public Vector3 previousPosition { get; private set; }
    
        void Awake()
        {
            accelerationOnAwake = force;
            maxSpeedOnAwake = currentMaxSpeed;
        }
        
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }
    
        void Update()
        {
            if (rb.velocity.magnitude > currentMaxSpeed)
                velocityText.text = "Speed: " + currentMaxSpeed.ToString("F2");
            else
                velocityText.text = "Speed: " + new Vector2(rb.velocity.x,rb.velocity.z).magnitude.ToString("F2");

            state = isGrounded ? MovementState.Walking : MovementState.Airborn;
            
            previousPosition = transform.position;
        }   
        private void FixedUpdate()
        {
            CounterMovement();
            Gravity();
            
            if (GameState.instance.isPaused) return;
            
            if (Input.GetKey(KeyCode.Space) && isGrounded)
                Jump();
            Movement();
        }
    
        private void Movement()
        {
            Vector3 forwardDir = orientation.forward * force * Input.GetAxisRaw("Vertical");
            Vector3 rightDir = orientation.right * force * Input.GetAxisRaw("Horizontal");
            Vector3 dir = (forwardDir + rightDir).normalized;
            Vector3 velNoY = new Vector3(rb.velocity.x, 0, rb.velocity.z);

            if (Mathf.Abs(Vector3.Angle(dir.normalized, velNoY.normalized)) > 2.0f)
                force = accelerationOnAwake * responsiveness;
            else
                force = accelerationOnAwake;

            if (velNoY.magnitude > currentMaxSpeed - 0.2f && velNoY.magnitude < absMaxSpeed && !isGrounded)
                currentMaxSpeedT += Time.deltaTime * maxSpeedAcceleration;
            else if (currentMaxSpeed > maxSpeedOnAwake)
                currentMaxSpeedT -= Time.deltaTime * maxSpeedAcceleration * 8;

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
        
        private void CounterMovement()
        {
            if (rb.velocity.magnitude > 0 && !isMoving)
            {
                Vector3 dir = new Vector3(-rb.velocity.x, 0, -rb.velocity.z);
                rb.AddForce(dir * drag);
            }
        }

        private void Jump()
        {
            rb.AddForce(Vector3.up * jumpForce);
        }

        private void Gravity()
        {
            rb.AddForce(Physics.gravity * gravityScale);
        }

        private void OnDrawGizmos()
        {
            Color col = isGrounded ? Color.green : Color.red;
            var position = transform.position;
            Debug.DrawLine(position, position + (Vector3.down * groundDistance), col);
        }
    }
}