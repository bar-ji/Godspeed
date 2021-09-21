using System;
using System.Collections;
using System.Collections.Generic;
using Management;
using TMPro;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Walking")]
        [SerializeField] private int acceleration;
        [SerializeField] private int responsiveness;
        [SerializeField] private int drag;
        [SerializeField] private int maxSpeed;
                         private int accelerationOnAwake;
        
        [Header("Jumping")]
        [SerializeField] private float groundDistance;
        [SerializeField] private float jumpForce;
        [SerializeField] private float gravityScale;
        [SerializeField] private LayerMask groundMask;
        

        [Header("References")]
        [SerializeField] private Transform orientation;
        [SerializeField] private TMP_Text velocityText;
                         private Rigidbody rb;
                         
        private bool isMoving => Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0;
        private bool canJump => Physics.Raycast(transform.position, Vector3.down, groundDistance, groundMask);
    
        void Awake()
        {
            accelerationOnAwake = acceleration;
        }
        
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }
    
        void Update()
        {
            if (rb.velocity.magnitude > maxSpeed)
                velocityText.text = "VEL: " + maxSpeed;
            else
                velocityText.text = "VEL: " + rb.velocity.magnitude.ToString("F1");
            if (Input.GetKeyDown(KeyCode.Space) && canJump)
                Jump();
        }   
        private void FixedUpdate()
        {
            CounterMovement();
            Gravity();
            
            if (GameState.instance.isPaused) return;
            
            Movement();
        }
    
        private void Movement()
        {
            Vector3 forwardDir = orientation.forward * acceleration * Input.GetAxisRaw("Vertical");
            Vector3 rightDir = orientation.right * acceleration * Input.GetAxisRaw("Horizontal");
            Vector3 dir = (forwardDir + rightDir).normalized;
            Vector3 velNoY = new Vector3(rb.velocity.x, 0, rb.velocity.y);

            if (Mathf.Abs(Vector3.Angle(dir.normalized, velNoY.normalized)) > 0.0f)
                acceleration = accelerationOnAwake * responsiveness;
            else
                acceleration = accelerationOnAwake;
            
            rb.AddForce(dir * acceleration);
            Vector3 vel = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            vel = Vector3.ClampMagnitude(vel, maxSpeed);
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
            Color col = canJump ? Color.green : Color.red;
            var position = transform.position;
            Debug.DrawLine(position, position + (Vector3.down * groundDistance), col);
        }
    }
}