using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace chia
{
    public class EnemyHealth : HealthSystem
    {
        private EnemySystem enemySystem;//敵人系統

        protected override void Awake()
        {
            base.Awake();
            enemySystem = GetComponent<EnemySystem>();
        }
        /// <summary>
        /// 死亡
        /// </summary>
        protected override void Dead()
        {
            base.Dead();
            enemySystem.enabled = false;//關閉敵人系統
            DropProp();
        }
        /// <summary>
        /// 掉落道具
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