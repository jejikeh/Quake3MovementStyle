using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quake3MovementStyle
{
    public class Quake3HoldAndDropObjects : MonoBehaviour
    {
        [SerializeField] private float _pickUpRange = 5f;
        [SerializeField] private Transform _holdTransform;

        public void CheckForPickUpObject()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, _pickUpRange))
            {
                PickUpObject(hit.transform.transform);
            }
        }

        private void PickUpObject(Transform pickObject)
        {
            if (pickObject.GetComponent<Rigidbody>())
            {
                Rigidbody rigidbodyPickObject = pickObject.GetComponent<Rigidbody>();

                rigidbodyPickObject.useGravity = false;
            }
        }
    }
}
