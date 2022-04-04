using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quake3CharacterRotaion : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _smoothRotationSpeed;
    [SerializeField] private float _limitAngle;

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

    public void LookRotation(Transform character, Transform camera, float xRotation, float yRotation)
    {
        _rotation.x += xRotation * _rotateSpeed;
        _rotation.y += yRotation * _rotateSpeed;
        _rotation.y = Mathf.Clamp(_rotation.y, -_limitAngle, _limitAngle);
        var xQuaternion = Quaternion.AngleAxis(_rotation.x, Vector3.up);
        var yQuaternion = Quaternion.AngleAxis(_rotation.y, Vector3.left);

        camera.localRotation = Quaternion.Slerp(camera.localRotation, yQuaternion, _smoothRotationSpeed * Time.deltaTime);
        character.localRotation = Quaternion.Slerp(character.localRotation, xQuaternion, _smoothRotationSpeed * Time.deltaTime);
    }
}
