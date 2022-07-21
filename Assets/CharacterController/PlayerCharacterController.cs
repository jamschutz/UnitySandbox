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


    // =============================================================== //
    // =========     Lifecycle Methods      ========================== //
    // =============================================================== //

        void Start()
        {
            controller = GetComponent<CharacterController>();
            input = GetComponent<PlayerInputController>();

            currentMoveLerp = 0;
            lastInputMovement = Vector3.zero;
            verticalVelocity = 0;
        }


        void Update()
        {
            Move();
        }


        void Move()
        {
            Vector3 currentMovement;

            // if we're currently moving, move!
            if(input.Current.Movement.sqrMagnitude > 0) {
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
            ApplyGravity();
            if(IsGrounded() && input.Current.Jump) {
                
                verticalVelocity += jumpPower;
            }

            Debug.Log($"jump: {input.Current.Jump} isgrounded: {IsGrounded()}");

            currentMovement.y = verticalVelocity;
            controller.Move(currentMovement * Time.deltaTime);
        }


    // =============================================================== //
    // =========     Physics Methods        ========================== //
    // =============================================================== //

        void ApplyGravity()
        {
            if(IsGrounded())  {
                verticalVelocity = 0;
                return;
            }

            // apply gravity
            verticalVelocity -= Globals.Gravity * Time.deltaTime;
        }

        bool IsGrounded()
        {
            return Physics.Raycast(transform.position, Vector3.down, MAX_IS_GROUNDED_DISTANCE, groundLayerMask);
        }
    }
}