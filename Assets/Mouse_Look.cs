using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse_Look : MonoBehaviour
{
    [SerializeField] private float _sensitivity = 2f;
    [SerializeField] private float _smooth_sensitivity = 2f;
    private static Quaternion _characterRotation;
    private static Quaternion _cameraRotation;

    private static Vector2 _rotation;
    /*
     ===========
     Init
     ===========
    */
    public void Init(Transform character,Transform camera)
    {
        _characterRotation = character.localRotation;
        _cameraRotation = camera.localRotation;
    }

    /* 
     ==========
    Set Mouse lock
     ==========
    */

    public void SetCursorLock(bool value)
    {
        if (value)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    /*
     ================= 
     LookRotation
     =================
    */
    public void LookRotation(Transform character,Transform camera)
    {
        _rotation.x += Input.GetAxis("Mouse X") * _sensitivity;
        _rotation.y += Input.GetAxis("Mouse Y") * _sensitivity;
        var xQuaternion = Quaternion.AngleAxis(_rotation.x, Vector3.up);
        var yQuaternion = Quaternion.AngleAxis(_rotation.y, Vector3.left);

        camera.localRotation = Quaternion.Slerp(camera.localRotation, yQuaternion, _smooth_sensitivity * Time.deltaTime);
        character.localRotation = Quaternion.Slerp(character.localRotation, xQuaternion, _smooth_sensitivity * Time.deltaTime); ;

        /*
        camera.localRotation *= Quaternion.Euler(-yRotation, 0f,0f);
        character.localRotation *= Quaternion.Euler(0f, xRotation, 0f);
        */

    }
}
