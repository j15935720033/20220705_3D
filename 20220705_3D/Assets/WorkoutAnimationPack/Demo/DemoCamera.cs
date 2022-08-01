using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorkoutAnimationPack
{
    public class DemoCamera : MonoBehaviour
    {
        public float RotateSpeed = 15f;
        public Transform RoteteCenter;

        [System.Obsolete]
        void Update()
        {
            transform.RotateAround(RoteteCenter.position, Vector3.up, RotateSpeed * Time.deltaTime);
        }
    }
}
