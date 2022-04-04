using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quake3Movement : MonoBehaviour
{

    [SerializeField] private Transform _character;
    [SerializeField] private Transform _camera;
    [SerializeField] private Quake3CharacterRotation _characterRotation;
    
    void Start()
    {
        _characterRotation = GetComponent<Quake3CharacterRotation>();
        _characterRotation.SetCursorLock(true);
    }

    
    void Update()
    {

        _characterRotation.LookRotation(_character, _camera, Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    }
}
