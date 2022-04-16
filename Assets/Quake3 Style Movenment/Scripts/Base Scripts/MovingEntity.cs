using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quake3MovementStyle
{
    [RequireComponent(typeof(Quake3Rotation))]
    [RequireComponent(typeof(Quake3Movement))]
    [RequireComponent(typeof(Quake3HeadBob))]
    [RequireComponent(typeof(CharacterController))]
    public class MovingEntity : MonoBehaviour
    {

        [SerializeField] private Transform _characterTransform;


        [Header("Rotate")]
        [SerializeField] protected Transform _cameraTransform;
        [SerializeField] protected Quake3Rotation _characterRotation;
        [SerializeField] private bool _isHeadBobing;


        [Header("Movement")]
        [SerializeField] private CharacterController _characterController; // Delegirovanie
        [SerializeField] private Quake3Movement _characterMovement;
        [SerializeField] private Quake3HeadBob _characterHeadBob;

        private void Start()
        {
            _characterController = GetComponent<CharacterController>();

            _characterHeadBob = GetComponent<Quake3HeadBob>();
            _characterMovement = GetComponent<Quake3Movement>();
            _characterRotation = GetComponent<Quake3Rotation>();
        }


        protected void ControlCharacterLookRotation(float xRotation, float yRotation)
        {
            _characterRotation.LookRotation(_characterTransform, _cameraTransform, xRotation, yRotation,transform.InverseTransformVector(_characterMovement.Speed));
        }

        protected void ControlCharacterMovement(Vector3 direction)
        {
            _characterMovement.Movement(_characterController, _characterTransform, direction);
            if(_isHeadBobing)
            {
                ControlCharacterHeadBob();
            }
        }


        // --- Crouch System --- 
        protected void isCharacterCrouch(bool crouch)
        {
            _characterMovement.Crouch(crouch,_characterController,_characterTransform);
        }


        // --- Jump System ---
        protected void isCharacterJump(bool jump)
        {
            _characterMovement.Jump(jump);
        }

        private void ControlCharacterHeadBob()
        {
            if (_characterMovement.IsCharacterGrounded(_characterTransform))
            {
                _characterHeadBob.HeadBob(_characterTransform, _cameraTransform, transform.InverseTransformVector(_characterMovement.Speed)); // speed from world to local
            }
        }

        // --- Check if on the ground

        
    }
}
