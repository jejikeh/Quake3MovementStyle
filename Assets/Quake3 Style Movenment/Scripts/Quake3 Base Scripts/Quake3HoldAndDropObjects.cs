using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quake3MovementStyle
{
    public class Quake3HoldAndDropObjects : MonoBehaviour
    {
        [SerializeField] private float _pickUpRange = 2f;
        [SerializeField] private Transform _holdTransform;
        private Transform _holdedObject;

        public void CheckForPickUpObject()
        {
            
            
            if (_holdedObject == null)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, _pickUpRange))
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
                _holdedObject.position = _holdTransform.position;
            }
        }

        private void PickUpObject(Transform pickObject)
        {
            if (pickObject.GetComponent<Rigidbody>())
            {
                pickObject.GetComponent<Rigidbody>().useGravity = false;
                pickObject.GetComponent<Rigidbody>().drag = 10;
                pickObject.GetComponent<Rigidbody>().freezeRotation = true;
                pickObject.GetComponent<Rigidbody>().isKinematic = true;

                pickObject.parent = _holdTransform;
                _holdedObject = pickObject;
            }
        }

        private void DropObject()
        {
            _holdedObject.GetComponent<Rigidbody>().useGravity = true;
            _holdedObject.GetComponent<Rigidbody>().drag = 1;
            _holdedObject.GetComponent<Rigidbody>().freezeRotation = false;
            _holdedObject.GetComponent<Rigidbody>().isKinematic = false;

            _holdedObject.parent = null;
            _holdedObject = null;
        }
    }
}
