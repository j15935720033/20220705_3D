using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace chia
{
    public class PlayerHealth : HealthSystem
    {
        private ThirdPersonController tpc;//控制玩家移動

        protected override void Awake()
        {
            base.Awake();//因為override會覆蓋父類別方法，所以還要乎叫父類別的方法，
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