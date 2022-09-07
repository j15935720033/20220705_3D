using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace chia
{


    /// <summary>
    /// ���a����:�z�L��J�覡��������ʵe�P�����P�w
    /// </summary>
    public class PlayerAttack : AttackSystem
    {
        private string parAttack = "Ĳ�o����";
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
        /// ������J�P�w
        /// </summary>
        private void AttackInput()
        {
            if (!canAttack) return;//�p�G��������A�N���X

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                tpc.enabled = false;//�����ɡA��������
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