using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace jsch
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(PlayerInputController))]
    public class PlayerMoveController : MonoBehaviour
    {
    // =============================================================== //
    // =========     Variables              ========================== //
    // =============================================================== //

        // ----- public vars ----------- //
        [Header("Movement")]
        public float moveSpeed;


        // ----- private vars ----------- //
        private CharacterController controller;
        private PlayerInputController input;


    // =============================================================== //
    // =========     Lifecycle Methods      ========================== //
    // =============================================================== //

        void Start()
        {
            controller = GetComponent<CharacterController>();
            input = GetComponent<PlayerInputController>();
        }


        void Update()
        {
            controller.Move(input.Current.Movement * moveSpeed * Time.deltaTime);
        }
    }
}