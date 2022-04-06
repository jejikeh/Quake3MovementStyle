using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Quake3MovementStyle
{

    [RequireComponent(typeof(Quake3Rotation))]
    [RequireComponent(typeof(Quake3Movement))]
    [RequireComponent(typeof(Quake3HeadBob))]
    [RequireComponent(typeof(CharacterController))]
    public class Quake3Player : MonoBehaviour
    {
        [SerializeField] private Transform _characterTransform;


        [Header("Camera")]
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private Quake3Rotation _characterRotation;
        [SerializeField] private bool _isHeadBobing;
        private Vector2 _mouseInput; // Mouse 2D Input Vector


        [Header("Movement")]
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Quake3Movement _characterMovement;
        [SerializeField] private Quake3HeadBob _characterHeadBob;
        private Vector3 _keyboardInput;


        private void Start()
        {
            _characterController = GetComponent<CharacterController>();

            _characterHeadBob = GetComponent<Quake3HeadBob>();
            _characterMovement = GetComponent<Quake3Movement>();
            _characterRotation = GetComponent<Quake3Rotation>();

            _characterRotation.SetCursorLock(true);

        }


        void Update()
        {
            ControlCharacter(); // allow user to control the character

            if (_isHeadBobing) // enabling head bobing
            {
                EnableHeadBob();
            }
        }

        private void EnableHeadBob()
        {
            if (_characterController.isGrounded)
            {
                _characterHeadBob.HeadBob(_cameraTransform, _characterMovement._speed);
            }
        }

        private void ControlCharacter()
        {
            _mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")); // Mouse Input Vector
            _characterRotation.LookRotation(_characterTransform, _cameraTransform, _mouseInput.x, _mouseInput.y);


            _keyboardInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            _characterMovement.Movement(_characterController, _characterTransform, _keyboardInput);

            if (Input.GetButtonDown("Jump"))
            {
                _characterMovement.Jump(true);
            }
            else if (Input.GetButtonUp("Jump"))
            {
                _characterMovement.Jump(false);
            }
            else if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                _characterMovement.Crouch(true, _characterController);
            }
            else if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                _characterMovement.Crouch(false, _characterController);
            }
        }
    }
}