using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quake3CharacterRotation : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _smoothRotateSpeed;

    private Vector2 _rotation;

    public void SetCursorLock(bool value)
    {
        if (value)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        } else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void LookRotation(Transform character, Transform camera,float xRotation,float yRotation)
    {
        _rotation.x += xRotation * _rotateSpeed; 
        _rotation.y += yRotation * _rotateSpeed;
        var xQuaternion = Quaternion.AngleAxis(_rotation.x, Vector3.up);
        var yQuaternion = Quaternion.AngleAxis(_rotation.y, Vector3.left);

        camera.localRotation = Quaternion.Slerp(camera.localRotation, yQuaternion, _smoothRotateSpeed * Time.deltaTime);
        character.localRotation = Quaternion.Slerp(character.localRotation, xQuaternion, _smoothRotateSpeed * Time.deltaTime);
    }
}
