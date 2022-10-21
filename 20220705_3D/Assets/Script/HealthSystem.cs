using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace chia
{
    /// <summary>
    /// 血量系統
    /// </summary>
    public class HealthSystem : MonoBehaviour
    {
        [SerializeField, Header("血量資料")]
        protected DataHealth dataHealth;
        [SerializeField, Header("image_血條")]
        protected Image imageHealth;
        public float hp;
        private Animator ani;
        private string parHurt = "觸發受傷";
        private string parDead = "開關死亡";
        private AttackSystem attackSystem;

        protected virtual void Awake()
        {
            ani = GetComponent<Animator>();
            attackSystem = GetComponent<AttackSystem>();
            hp = dataHealth.hp;
        }
      
        /// <summary>
        /// 受傷
        /// </summary>
        /// <param name="damage"></param>
        public void Hurt(float damage)
        {
            hp -= damage;
            ani.SetTrigger(parHurt);
            if (hp <= 0) Dead();
            imageHealth.fillAmount = hp / dataHealth.hpMax;
            
        }
        /// <summary>
        /// 死亡
        /// </summary>
        protected virtual void Dead()
        {
            hp = 0;
            ani.SetBool(parDead,true);
            attackSystem.enabled = false;
        }
    }
}