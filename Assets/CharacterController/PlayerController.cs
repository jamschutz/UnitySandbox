using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using jsch.Managers;

namespace jsch
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        // ------ public vars ------ //
        public float moveSpeed;


        // ------ private vars ------ //
        private CharacterController controller;


        // ---------- LIFETIME ----------------- //
        void Start()
        {
            controller = GetComponent<CharacterController>();
        }


        void Update()
        {
            Move(InputManager.Movement.forward, transform.forward);
            Move(InputManager.Movement.backward, -transform.forward);
            Move(InputManager.Movement.left, transform.right);
            Move(InputManager.Movement.right, -transform.right);
        }


        // ---------- HELPERS ----------------- //
        void Move(KeyCode moveKey, Vector3 direction)
        {
            if(Input.GetKey(moveKey)) {
                controller.Move(direction * moveSpeed * Time.deltaTime);
            }
        }
    }
}