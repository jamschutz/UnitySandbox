using UnityEngine;

// idea for this class taken from: https://github.com/IronWarrior/SuperCharacterController/blob/master/Assets/SuperCharacterController/Scripts/PlayerInputController.cs
namespace jsch
{
    public class PlayerInputController : MonoBehaviour
    {
        // ------ public vars ------ //
        public Vector2 mouseSensitivity;
        public Vector2 rightStickSensitivity = new Vector2(3, -1.5f);

        [HideInInspector]
        public PlayerInput Current;

        // ---------- LIFETIME ----------------- //
        void Start()
        {
            Current = new PlayerInput();
        }


        void Update()
        {
            // get movement
            Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            // get mouse
            Vector2 mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            // get joypad
            // Vector2 rightStickInput = new Vector2(Input.GetAxisRaw("Right Stick X"), Input.GetAxisRaw("Right Stick Y")) * rightStickSensitivity;
            Vector2 rightStickInput = new Vector2(0, 0);

            // pass right stick values in place of mouse if we can
            Vector2 camera = new Vector2(
                rightStickInput.x != 0 ? rightStickInput.x : mouseInput.x,
                rightStickInput.y != 0 ? rightStickInput.y : mouseInput.y
            );

            bool jumpInput = Input.GetButtonDown("Jump");

            Current = new PlayerInput() {
                Movement = moveInput,
                Camera = camera,
                Jump = jumpInput
            };
        }
    }


    public struct PlayerInput
    {
        public Vector3 Movement;
        public Vector2 Camera;
        public bool Jump;

        public bool GotMovementInput => Movement.sqrMagnitude > 0;
    }
}