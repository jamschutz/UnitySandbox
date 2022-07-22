using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace jsch
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(PlayerInputController))]
    public class PlayerCharacterController : MonoBehaviour
    {
    // =============================================================== //
    // =========     Variables              ========================== //
    // =============================================================== //

        // ----- public vars ----------- //
        [Header("Movement")]
        public float moveSpeed;
        public AnimationCurve rampUp;
        public AnimationCurve rampDown;
        public float rampUpTime;
        public float rampDownTime;

        [Header("Jump")]
        public float jumpPower;

        [Header("Physics")]
        public LayerMask groundLayerMask;


        // ----- private vars ----------- //
        private CharacterController controller;
        private PlayerInputController input;

        // movement helpers
        private Vector3 lastInputMovement;
        private float currentMoveLerp;

        // physics helpers
        private const float MAX_IS_GROUNDED_DISTANCE = 0.1f;
        private float verticalVelocity;
        private bool isGrounded;


    // =============================================================== //
    // =========     Lifecycle Methods      ========================== //
    // =============================================================== //

        void Awake()
        {
            controller = GetComponent<CharacterController>();
            input = GetComponent<PlayerInputController>();
        }

        void Start()
        {
            currentMoveLerp = 0;
            lastInputMovement = Vector3.zero;
            verticalVelocity = 0;
        }


        void Update()
        {
            // perform grounded check once per frame
            isGrounded = IsGrounded();

            Move();
            ApplyGravity();
        }


        void Move()
        {
            Vector3 currentMovement;

            // if we're currently moving, move!
            if(input.Current.GotMovementInput) {
                currentMoveLerp = Mathf.Clamp(currentMoveLerp + Time.deltaTime / rampUpTime, 0, 1);
                float currentSpeed = rampUp.Evaluate(currentMoveLerp) * moveSpeed;
                currentMovement = input.Current.Movement.normalized * currentSpeed;

                // update last movement
                lastInputMovement = input.Current.Movement.normalized;
            }
            // otherwise, move slightly at the end
            else {
                currentMoveLerp = Mathf.Clamp(currentMoveLerp - Time.deltaTime / rampDownTime, 0, 1);
                float currentSpeed = rampDown.Evaluate(currentMoveLerp) * moveSpeed;
                currentMovement = lastInputMovement * currentSpeed;
            }

            // apply jump
            if(isGrounded && input.Current.Jump) {
                
                verticalVelocity += jumpPower;
            }

            currentMovement.y = verticalVelocity;
            controller.Move(currentMovement * Time.deltaTime);
        }


    // =============================================================== //
    // =========     Physics Methods        ========================== //
    // =============================================================== //

        void ApplyGravity()
        {
            if(isGrounded && verticalVelocity < 0)  {
                verticalVelocity = -2f;
                return;
            }

            // apply gravity
            verticalVelocity -= Globals.Gravity * Time.deltaTime;
        }

        bool IsGrounded()
        {
            // set feet position to center of object, minus half the character controller's height
            Vector3 feetPosition = transform.position - (Vector3.up * (controller.height * 0.5f));
            // check if we're grounded
            return Physics.CheckSphere(feetPosition, MAX_IS_GROUNDED_DISTANCE, groundLayerMask);
        }
    }
}