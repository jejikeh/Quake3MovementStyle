using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Quake3MovementStyle
{
    public class Quake3HeadBob : MonoBehaviour
    {

        private float _timer;

        [Header("Head bob force")]
        [SerializeField] private float _headBobForceRun;
        [SerializeField] private float _headBobForceIdle;

        [Header("Head bob speed")]
        [SerializeField] private float _headBobSpeedRun;
        [SerializeField] private float _headBobSpeedIdle;


        public void HeadBob(Transform characterTransform,Transform cameraTransform, Vector3 characterVelocity){

            

            if (Mathf.Abs(characterVelocity.x) > 0.1f || Mathf.Abs(characterVelocity.z) > 0.1f)
            {
                _timer += Time.deltaTime * _headBobSpeedRun;
                cameraTransform.localPosition = new Vector3(0 + Mathf.Cos(_timer/2) * _headBobForceRun, 0 + Mathf.Sin(_timer) * _headBobForceRun, cameraTransform.localPosition.z);
            }
            else
            {
                _timer += Time.deltaTime * _headBobSpeedIdle;
                cameraTransform.localPosition = new Vector3(cameraTransform.localPosition.x, 0 + Mathf.Sin(_timer) * _headBobForceIdle, cameraTransform.localPosition.z);
            }
        }
    }
}
