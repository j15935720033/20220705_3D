using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace chia
{
    public class PlayerHealth : HealthSystem
    {
        private ThirdPersonController tpc;//����a����

        protected override void Awake()
        {
            base.Awake();//�]��override�|�л\�����O��k�A�ҥH�٭n�G�s�����O����k�A
            tpc=GetComponent<ThirdPersonController>();
        }
        private void Update()
        {
            //print($"hp:{hp}  dataHealth.hpMax:{dataHealth.hpMax}");
        }
        protected override void Dead()
        {
            base.Dead();
            tpc.enabled = false;
        }
    }
}