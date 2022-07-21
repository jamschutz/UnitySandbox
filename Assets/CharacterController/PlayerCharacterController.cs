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

        private float currentMoveLerp;


    // =============================================================== //
    // =========     Lifecycle Methods      ========================== //
    // =============================================================== //

        void Start()
        {
            controller = GetComponent<CharacterController>();
            input = GetComponent<PlayerInputController>();

            currentMoveLerp = 0;
        }


        void Update()
        {
            Move();
        }


        void Move()
        {
            float currentSpeed;

            // get our current speed
            if(input.Current.Movement.sqrMagnitude > 0) {
                currentMoveLerp = Mathf.Clamp(currentMoveLerp + Time.deltaTime / rampUpTime, 0, 1);
                currentSpeed = rampUp.Evaluate(currentMoveLerp) * moveSpeed;
            }
            else {
                currentMoveLerp = Mathf.Clamp(currentMoveLerp - Time.deltaTime / rampDownTime, 0, 1);
                currentSpeed = (1.0f - rampDown.Evaluate(currentMoveLerp)) * moveSpeed;
            }

            controller.Move(input.Current.Movement.normalized * currentSpeed * Time.deltaTime);
        }
    }
}