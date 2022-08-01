using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorkoutAnimationPack
{
    [RequireComponent(typeof(Animator))]
    public class DemoScript : MonoBehaviour
    {
        public string AnimationName;

        void OnEnable()
        {
            Animator anim = GetComponent<Animator>();
            anim.Play(AnimationName);
        }
    }
}
