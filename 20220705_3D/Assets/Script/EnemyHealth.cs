using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace chia
{
    public class EnemyHealth : HealthSystem
    {
        private EnemySystem enemySystem;//�ĤH�t��

        protected override void Awake()
        {
            base.Awake();
            enemySystem = GetComponent<EnemySystem>();
        }
        /// <summary>
        /// ���`
        /// </summary>
        protected override void Dead()
        {
            base.Dead();
            enemySystem.enabled = false;//�����ĤH�t��
            DropProp();
        }
        /// <summary>
        /// �����D��
        /// </summary>
        private void DropProp()
        {
            float value = Random.value;
            if (value <= dataHealth.propProbability)
            {
                Instantiate(
                    dataHealth.goProp,
                    transform.position + Vector3.up * 6,
                    Quaternion.identity
                );
            }

            
        }
    }
}