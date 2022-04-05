using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quake3MovementStyle
{
    //[RequireComponent(typeof(CharacterController))]
    public class Quake3Movement : MonoBehaviour
    {

        private Vector3 _characterVelocity = Vector3.zero;


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

        //private float _speed { get { return _characterController.velocity.magnitude;  } }
        [SerializeField] private _movementSettings _groundMovementSettings = new _movementSettings(0, 0, 0);

        
        
        [SerializeField] private float _friction;
        [SerializeField] private float _gravity;


        public void Movement(CharacterController characterController,Transform characterTransform ,Vector3 directionInput)
        {
            if (characterController.isGrounded)
            {
                GroundMove(characterController,characterTransform, directionInput.x, directionInput.z);
            } else
            {
                AirMove();
            }
            characterController.Move(_characterVelocity * Time.deltaTime);

        }

        private void GroundMove(CharacterController characterController, Transform characterTransform,float xInput, float zInput)
        {
            ApplyFriction(characterController,1.0f);
            Vector3 wishDirection = new Vector3(xInput, 0, zInput);
            wishDirection = characterTransform.TransformDirection(wishDirection);

            float wishSpeed = wishDirection.magnitude;
            wishSpeed *= _groundMovementSettings.MaxSpeed;
            Accelerate(wishDirection, wishSpeed, _groundMovementSettings.Acceleration);
        }

        private void AirMove()
        {
            _characterVelocity.y -= _gravity * Time.deltaTime;
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
            _characterVelocity.z += accelerationSpeed * targetDirection.z;
        }

        private void ApplyFriction(CharacterController characterController,float t)
        {

            //_characterVelocity.y = 0;
            float speed = _characterVelocity.magnitude;
            float drop = 0;
            // Checking if grounded

            if (characterController.isGrounded)
            {
                float control = speed < _groundMovementSettings.Deceleration ? _groundMovementSettings.Deceleration : speed;
                drop = control * _friction * t * Time.deltaTime;
            }


            float newSpeed = speed - drop;

            if (newSpeed < 0)
            {
                newSpeed = 0;
            }
            if (newSpeed > 0)
            {
                newSpeed /= speed;
            }
            _characterVelocity.x *= newSpeed;
            _characterVelocity.z *= newSpeed;

        }
    }

}
