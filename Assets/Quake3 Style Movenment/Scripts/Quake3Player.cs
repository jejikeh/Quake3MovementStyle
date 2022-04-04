using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quake3MovementStyle
{

    public class Quake3Player : MonoBehaviour
    {

        [SerializeField] private Transform _character;
        [SerializeField] private Transform _camera;
        [SerializeField] private Quake3Rotation _characterRotation;


        void Start()
        {
            _characterRotation = GetComponent<Quake3Rotation>();
            _characterRotation.SetCursorLock(true);
        }

        void Update()
        {
            _characterRotation.LookRotation(_character, _camera, Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        }
    }
}