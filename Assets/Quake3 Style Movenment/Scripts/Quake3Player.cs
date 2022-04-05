using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Quake3MovementStyle
{

    [RequireComponent(typeof(Quake3Rotation))]
    [RequireComponent(typeof(Quake3Movement))]
    [RequireComponent(typeof(CharacterController))]
    public class Quake3Player : MonoBehaviour
    {
        [SerializeField] private Transform _characterTransform;


        [Header("Camera")]
        [SerializeField] private Transform _camera;
        [SerializeField] private Quake3Rotation _characterRotation;
        private Vector2 _mouseInput; // Mouse 2D Input Vector


        [Header("Movement")]
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Quake3Movement _characterMovement;
        private Vector3 _keyboardInput;


        private void Start()
        {
            _characterController = GetComponent<CharacterController>();

            _characterRotation = GetComponent<Quake3Rotation>();
            _characterRotation.SetCursorLock(true);
        }


        void Update()
        {
            _mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")); // Mouse Input Vector
            _characterRotation.LookRotation(_characterTransform, _camera,_mouseInput.x,_mouseInput.y);

            _keyboardInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            _characterMovement.Movement(_characterController, _characterTransform, _keyboardInput);
            

        }
    }
}