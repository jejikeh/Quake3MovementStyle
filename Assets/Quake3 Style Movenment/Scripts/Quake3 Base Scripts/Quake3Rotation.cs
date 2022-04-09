using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quake3MovementStyle
{

    public class Quake3Rotation : MonoBehaviour
    {
        [SerializeField] private float _rotateSpeed;
        [SerializeField] private float _smoothRotationSpeed;
        [SerializeField] private float _limitAngle;
        [SerializeField] private float _rotateMovementSpeed;

        private Vector2 _rotation;

        public void SetCursorLock(bool value)
        {
            if (value)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

        public void LookRotation(Transform character, Transform camera, float xRotation, float yRotation,Vector3 characterVelocity)
        {
            _rotation.x += (xRotation * _rotateSpeed);
            _rotation.y += (yRotation * _rotateSpeed);
            _rotation.y = Mathf.Clamp(_rotation.y, -_limitAngle, _limitAngle);
            var xQuaternion = Quaternion.AngleAxis(_rotation.x + characterVelocity.x * _rotateMovementSpeed, Vector3.up);
            var yQuaternion = Quaternion.AngleAxis(_rotation.y + characterVelocity.z * _rotateMovementSpeed, Vector3.left);

            camera.localRotation = Quaternion.Slerp(camera.localRotation, yQuaternion, _smoothRotationSpeed * Time.deltaTime);
            character.localRotation = Quaternion.Slerp(character.localRotation, xQuaternion, _smoothRotationSpeed * Time.deltaTime);
        }
    }
}
