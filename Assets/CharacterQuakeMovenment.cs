using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterQuakeMovenment : MonoBehaviour
{
    [SerializeField] private Transform _character;
    [SerializeField] private Transform _camera;

    [Header("Camera")]
    [SerializeField] private Mouse_Look _mouseLook;
    void Start()
    {
        _mouseLook.SetCursorLock(true);
    }

    // Update is called once per frame
    void Update()
    {
        _mouseLook.LookRotation(_character,_camera);
    }
}
