using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quake3MovementStyle
{
    public class Quake3HoldAndDropObjects : MonoBehaviour
    {
        [SerializeField] private float _pickUpRange = 2f;
        [SerializeField] private float _throwForce = 2f;

        [SerializeField] private Transform _holdTransform;
        [SerializeField] private Vector3 _holdDefaultPosition;


        [Header("Mouse Wheel Settings")]
        [SerializeField] private float _farDistance;
        [SerializeField] private float _closeDistance;

        [SerializeField] private float _wheelForce;
        private Transform _holdedObject;

        public Vector3 GetHoldDefaultPosition { get { return _holdDefaultPosition; } }

        public void CheckForPickUpObject(Transform cameraTransform)
        {
            
            // if not already holding object
            if (_holdedObject == null)
            {
                RaycastHit hit;
                // Check if in the center of camera is pickable object
                if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, _pickUpRange))
                {
                    PickUpObject(hit.transform.transform);
                }
            } else
            {
                DropObject();
            }

            if (_holdedObject != null)
            {
                MovePickUpObject();
            }
        }

        private void MovePickUpObject()
        {
            if(Vector3.Distance(_holdedObject.transform.position, _holdTransform.position) > 0.1f)
            {
                _holdedObject.GetComponent<Rigidbody>().AddForce((_holdTransform.position - _holdedObject.transform.position) * 100f);
            }
        }

        private void Update()
        {
            if (_holdedObject != null)
            {
                MovePickUpObject();
            }
        }

        internal void PickUpObject(Transform pickObject)
        {
            if (pickObject.GetComponent<Rigidbody>())
            {
                pickObject.GetComponent<Rigidbody>().useGravity = false;
                pickObject.GetComponent<Rigidbody>().drag = 10;
                //pickObject.GetComponent<Rigidbody>().freezeRotation = true;
                pickObject.parent = _holdTransform;
                _holdedObject = pickObject;

                // Reset the _holdedTransform
                _holdTransform.localPosition = _holdDefaultPosition;
            }
        }

        internal void DropObject()
        {
            _holdedObject.GetComponent<Rigidbody>().useGravity = true;
            _holdedObject.GetComponent<Rigidbody>().drag = 1;
            //_holdedObject.GetComponent<Rigidbody>().freezeRotation = false;
            _holdedObject.parent = null;
            _holdedObject = null;

            // Reset the _holdedTransform
            _holdTransform.localPosition = _holdDefaultPosition;
        }

        public void ThrowObject(Transform cameraTransform)
        {
            if(_holdedObject != null)
            {
                _holdedObject.GetComponent<Rigidbody>().AddForce(cameraTransform.forward * _throwForce);
                DropObject();
            }
        }

        public void MoveHoldTransform(bool direction) // Add transform object by wheel only by Z coords
        {
            if (_holdedObject != null)
            {

                Vector3 tempPosition = _holdTransform.localPosition;
                if (direction)
                {
                    if(tempPosition.z - _holdDefaultPosition.z < _farDistance)
                    {
                        tempPosition.z += _wheelForce; 
                    }   
                    // Reset the _holdedTransform
 
                } else
                {
                    if (tempPosition.z - _holdDefaultPosition.z > _closeDistance)
                    {
                        tempPosition.z -= _wheelForce;
                    }

                    // Reset the _holdedTransform

                }
                _holdTransform.localPosition = tempPosition;

            }
        }
    }
}
