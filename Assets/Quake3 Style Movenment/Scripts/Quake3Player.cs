using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Quake3MovementStyle
{

    [RequireComponent(typeof(Quake3HoldAndDropObjects))]
    public class Quake3Player : MovingEntity
    {
        private Vector2 _mouseInput; // Mouse 2D Input Vector
        private Vector3 _keyboardInput;

        [SerializeField] private Quake3HoldAndDropObjects _quake3HoldAndDropObjects;


        private void Start()
        {
            _quake3HoldAndDropObjects = GetComponent<Quake3HoldAndDropObjects>();

            _characterRotation.SetCursorLock(true);

        }


        void Update()
        {
            // --- Rotation
            _mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            ControlCharacterLookRotation(_mouseInput.x, _mouseInput.y);


            // Movement
            _keyboardInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0 , Input.GetAxisRaw("Vertical"));
            ControlCharacterMovement(_keyboardInput);


            // --- Jumping Event ---
            if (Input.GetButtonDown("Jump"))
            {
                isCharacterJump(true);
            }
            else if (Input.GetButtonUp("Jump"))
            {
                isCharacterJump(false);
            }

            // --- Crouch Event ---
            if (Input.GetButton("Crouch"))
            {
                isCharacterCrouch(true);
            }
            else
            {
                isCharacterCrouch(false);
            }

            // --- Pick up Event ---
            if (Input.GetKeyDown(KeyCode.E))
            {
                _quake3HoldAndDropObjects.CheckForPickUpObject(transform); // pick up objects
            }

            
        }
    }
}