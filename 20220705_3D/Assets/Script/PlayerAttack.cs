using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace chia
{


    /// <summary>
    /// 玩家攻擊:透過輸入方式控制攻擊動畫與攻擊判定
    /// </summary>
    public class PlayerAttack : AttackSystem
    {
        private string parAttack = "觸發攻擊";
        private ThirdPersonController tpc;

        protected override void Awake()
        {
            base.Awake();
            tpc = GetComponent<ThirdPersonController>();
        }

        private void Update()
        {
            AttackInput();
        }

        /// <summary>
        /// 攻擊輸入判定
        /// </summary>
        private void AttackInput()
        {
            if (!canAttack) return;//如果不能攻擊，就跳出

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                tpc.enabled = false;//攻擊時，關掉移動
                ani.SetTrigger(parAttack);
                base.StartAttack();
            }
        }
        protected override void stopAttack()
        {
            tpc.enabled = true;
        }
    }
}