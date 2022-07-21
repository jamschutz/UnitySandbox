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


        // ----- private vars ----------- //
        private CharacterController controller;
        private PlayerInputController input;

        private Vector3 lastInputMovement;
        private float currentMoveLerp;


    // =============================================================== //
    // =========     Lifecycle Methods      ========================== //
    // =============================================================== //

        void Start()
        {
            controller = GetComponent<CharacterController>();
            input = GetComponent<PlayerInputController>();

            currentMoveLerp = 0;
            lastInputMovement = Vector3.zero;
        }


        void Update()
        {
            Move();
        }


        void Move()
        {
            float currentSpeed;
            Vector3 currentMovement;

            // get our current speed
            if(input.Current.Movement.sqrMagnitude > 0) {
                currentMoveLerp = Mathf.Clamp(currentMoveLerp + Time.deltaTime / rampUpTime, 0, 1);
                currentSpeed = rampUp.Evaluate(currentMoveLerp) * moveSpeed;
                currentMovement = input.Current.Movement.normalized * currentSpeed;
                lastInputMovement = input.Current.Movement.normalized;
            }
            else {
                currentMoveLerp = Mathf.Clamp(currentMoveLerp - Time.deltaTime / rampDownTime, 0, 1);
                currentSpeed = rampDown.Evaluate(currentMoveLerp) * moveSpeed;
                currentMovement = lastInputMovement * currentSpeed;
                // currentMovement = Vector2.zero;
            }

            Debug.Log($"lerp: {currentMoveLerp}  speed: {currentSpeed}");

            controller.Move(currentMovement * Time.deltaTime);
        }
    }
}