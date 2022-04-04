using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Quake3MovementStyle
{

    [RequireComponent(typeof(Quake3Rotation))]
    public class Quake3Player : MonoBehaviour
    {

        [SerializeField] private Transform _character;
        [SerializeField] private Transform _camera;
        [SerializeField] private Quake3Rotation _characterRotation;


        private Vector2 _mouseInput; // Mouse 2D Input Vector


        void Start()
        {
            _characterRotation = GetComponent<Quake3Rotation>();
            _characterRotation.SetCursorLock(true);
        }

        void Update()
        {
            _mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")); // Mouse Input Vector
            _characterRotation.LookRotation(_character, _camera,_mouseInput.x,_mouseInput.y);
        }
    }
}