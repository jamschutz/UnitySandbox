using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace jsch
{
    // taken largely from: https://github.com/IronWarrior/SuperCharacterController/blob/master/Assets/SuperCharacterController/Scripts/PlayerCamera.cs
    [RequireComponent(typeof(Camera))]
    public class CameraController : MonoBehaviour
    {
    // =============================================================== //
    // =========     Variables              ========================== //
    // =============================================================== //

        // ----- public vars ----------- //
        public Transform player;

        [Header("Follow")]
        [Range(0, 1)]
        public float followSpeed = 0.2f;
        public Vector3 followOffset;


        // ----- private vars ----------- //
        private Camera cam;
        private PlayerInputController input;

        private float yRotation;
        private Vector3 lookDirection;

        // helpers
        private float inverseFollowSpeed;




    // =============================================================== //
    // =========     Lifecycle Methods      ========================== //
    // =============================================================== //

        void Start()
        {
            cam = GetComponent<Camera>();
            input = player.gameObject.GetComponent<PlayerInputController>();
            inverseFollowSpeed = 1.0f - followSpeed;
            yRotation = 0;
            lookDirection = player.forward;
        }


        void LateUpdate()
        {
            // set position to player
            transform.position = player.position;

            // get input this frame to determine how to rotate
            yRotation += input.Current.Camera.y;

            // update look direction along the y axis
            lookDirection = Quaternion.AngleAxis(input.Current.Camera.x, player.up) * lookDirection;

            // get left direction in camera space
            Vector3 leftCamSpace = Vector3.Cross(lookDirection, player.up);

            // rotate camera
            transform.rotation = Quaternion.LookRotation(lookDirection, player.up);
            transform.rotation *= Quaternion.AngleAxis(yRotation, leftCamSpace);

            // move camera
            transform.position = transform.position - (transform.forward * followOffset.z); // z axis 
            transform.position = transform.position + (Vector3.up * followOffset.y); // y axis
        }


        void FollowPlayer()
        {
            // calculate position
            var currentPosition = transform.position;
            var targetPosition = player.position - player.forward * followOffset.z + new Vector3(0, followOffset.y, 0);
            var newPosition = targetPosition * followSpeed + currentPosition * inverseFollowSpeed;

            // calculate rotation
            var currentRotation = transform.rotation;
            var targetRotation = Quaternion.Euler(player.forward);
            var newRotation = Quaternion.Lerp(currentRotation, targetRotation, followSpeed);

            // set new position / rotation
            transform.position = newPosition;
            transform.rotation = newRotation;
        }
    }
}