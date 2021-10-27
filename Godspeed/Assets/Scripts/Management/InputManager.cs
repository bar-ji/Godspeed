using UnityEngine;

namespace Management
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager instance;
        void Awake()
        {
            if(instance) Destroy(this);
            instance = this;
        }
        
        public float xInput { get; private set; }
        public float yInput { get; private set; }
        public float xMouse { get; private set; }
        public float yMouse { get; private set; }

        public bool detectInput { private get; set; } = true;

        void Update()
        {
            if (!detectInput) return;
            
            xInput = Input.GetAxisRaw("Horizontal");
            yInput = Input.GetAxisRaw("Vertical");
            xMouse = Input.GetAxisRaw("Mouse X");
            yMouse = Input.GetAxisRaw("Mouse Y");
        }
    }
}