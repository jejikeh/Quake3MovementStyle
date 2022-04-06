using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quake3MovementStyle
{
    //[RequireComponent(typeof(CharacterController))]
    public class Quake3Movement : MonoBehaviour
    {
        // System variables
        private Vector3 _characterVelocity = Vector3.zero;
        private bool _isJump = false;
        private bool _isCrouch = false;


        [System.Serializable]
        private class _movementSettings
        {
            public float MaxSpeed;
            public float Acceleration;
            public float Deceleration;

            public _movementSettings(float maxSpeed, float acceleration, float deceleration)
            {
                MaxSpeed = maxSpeed;
                Acceleration = acceleration;
                Deceleration = deceleration;
            }
        }

        [SerializeField] private _movementSettings _groundMovementSettings = new _movementSettings(0, 0, 0);
        [SerializeField] private _movementSettings _crouchMovementSettings = new _movementSettings(0, 0, 0);
        [SerializeField] private _movementSettings _airMovementSettings = new _movementSettings(0, 0, 0);
        [SerializeField] private _movementSettings _strafeMovementSettings = new _movementSettings(0, 0, 0);





        [SerializeField] private float _frictionRunning;
        [SerializeField] private float _frictionCrouch;
        [SerializeField] private float _gravity;
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _airControl;


        // Used to get speed for head bobing
        public Vector3 _speed { get { return _characterVelocity;  } }

        public void Movement(CharacterController characterController,Transform characterTransform ,Vector3 directionInput)
        {
            if (characterController.isGrounded)
            {
                GroundMove(characterController,characterTransform, directionInput.x, directionInput.z);
            } else
            {
                AirMove(characterTransform,directionInput.x,directionInput.z);
            }
            characterController.Move(_characterVelocity * Time.deltaTime);
        }

        public void Jump(bool isJumpPressed)
        {
            if(isJumpPressed && !_isJump) // If Button pressed and character not jumping
            {
                _isJump = true;
            } else
            {
                _isJump = false;
            }
        }

        public void Crouch(bool isCrouchPressed,CharacterController characterController)
        {
            if (isCrouchPressed)
            {
                _isCrouch = true;
                characterController.radius = 0.25f;
                characterController.height = 0.5f;

            }
            else
            {
                _isCrouch = false;
                characterController.radius = 0.5f;
                characterController.height = 1f;
            }
        }

        private void GroundMove(CharacterController characterController, Transform characterTransform,float xInput, float zInput)
        {
            if (!_isJump)
            {
                ApplyFriction(characterController, 1.0f); // Apply friction only on the ground
            }
            else if (_isCrouch)
            {
                ApplyFriction(characterController, 0.01f);
            }else
            {
                ApplyFriction(characterController, 0);
            }
            Vector3 wishDirection = new Vector3(xInput, 0, zInput);
            wishDirection.Normalize();
            wishDirection = characterTransform.TransformDirection(wishDirection); // from local to global
            float wishSpeed = wishDirection.magnitude;
            wishSpeed *=  _isCrouch? _crouchMovementSettings.MaxSpeed : _groundMovementSettings.MaxSpeed;
            Accelerate(wishDirection, wishSpeed, _isCrouch? _crouchMovementSettings.Acceleration : _groundMovementSettings.Acceleration);

            _characterVelocity.y = -_gravity * Time.deltaTime; // Reset the jump force 

            if (_isJump) // Jump only when on the ground
            {
                _characterVelocity.y = _jumpForce;
                _isJump = false;
            }
            
        }

        private void AirMove(Transform _characterTransform,float xInput, float zInput)
        {
            float acceleration;
            Vector3 wishDir = new Vector3(xInput, 0, zInput);
            wishDir = _characterTransform.TransformDirection(wishDir);

            float wishSpeed = wishDir.magnitude;
            wishSpeed *= _airMovementSettings.MaxSpeed;
            
            wishDir.Normalize();
            // !!
            if(Vector3.Dot(_characterVelocity,wishDir) < 0) // If in air wishDir changes
            {
                acceleration = _airMovementSettings.Deceleration;
            } else
            {
                acceleration = _airMovementSettings.Acceleration;
            }

            if(xInput == 0 && zInput != 0)
            {
                if(wishSpeed > _strafeMovementSettings.MaxSpeed)
                {
                    wishSpeed = _strafeMovementSettings.MaxSpeed;
                }
                acceleration = _strafeMovementSettings.MaxSpeed;
            }   

            Accelerate(wishDir, wishSpeed, acceleration);
            if(_airControl > 0)
            {
                AirControl(wishDir, xInput, zInput, wishSpeed);
            }

            _characterVelocity.y -= _gravity * Time.deltaTime;

        }

        private void AirControl(Vector3 targetDir,float xInput,float zInput, float targetSpeed )
        {
            if(Mathf.Abs(zInput) < 1 || Mathf.Abs(targetSpeed) < 1)
            {
                return;
            }

            float zSpeed = _characterVelocity.y;
            _characterVelocity.y = 0;

            float speed = _characterVelocity.magnitude;
            _characterVelocity.Normalize();

            float dot = Vector3.Dot(_characterVelocity, targetDir);
            float k = 32;
            k *= _airControl * dot * dot * Time.deltaTime;

            if(dot > 0)
            {
                _characterVelocity.x *= speed + targetDir.x * k;
                _characterVelocity.y *= speed + targetDir.y * k;
                _characterVelocity.z *= speed + targetDir.z * k;

                _characterVelocity.Normalize();
            }

            _characterVelocity.x *= speed;
            _characterVelocity.y = zSpeed;

            _characterVelocity.z *= speed;
    
        }

        private void Accelerate(Vector3 targetDirection, float targetSpeed, float acceleration) // Float acceleration can be removed.
        {
            float currentSpeed = Vector3.Dot(_characterVelocity, targetDirection);

            if ( (targetSpeed - currentSpeed) <= 0)
            {
                return;
            }

            float accelerationSpeed = acceleration * targetSpeed * Time.deltaTime;
            if (accelerationSpeed > (targetSpeed - currentSpeed))
            {
                accelerationSpeed = (targetSpeed - currentSpeed);
            }

            _characterVelocity.x += accelerationSpeed * targetDirection.x;
            //_characterVelocity.y += accelerationSpeed * targetDirection.y;
            _characterVelocity.z += accelerationSpeed * targetDirection.z;
        }

        private void ApplyFriction(CharacterController characterController,float t)
        {

            // Taken from https://github.com/id-Software/Quake/blob/master/WinQuake/sv_user.c#L122 

            //_characterVelocity.y = 0;
            float speed = _characterVelocity.magnitude;
            float newSpeed = 0;
            // Checking if grounded

            if (characterController.isGrounded)
            {
                float control;
                if (!_isCrouch)
                {
                    control = speed < _groundMovementSettings.Deceleration ? _groundMovementSettings.Deceleration : speed;
                    newSpeed = speed - (control * _frictionRunning * t * Time.deltaTime);
                } else
                {
                    control = speed < _crouchMovementSettings.Deceleration ? _crouchMovementSettings.Deceleration : speed;
                    newSpeed = speed - (control * _frictionCrouch * t * Time.deltaTime);
                }
            }

            if (newSpeed < 0)
            {
                newSpeed = 0;
            }
            newSpeed /= speed;
            _characterVelocity.x *= newSpeed;
            //_characterVelocity.y *= newSpeed;
            _characterVelocity.z *= newSpeed;

        }
    }

}
