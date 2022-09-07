using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace chia
{


    /// <summary>
    /// 攻擊基底系統
    /// </summary>
    public class AttackSystem : MonoBehaviour
    {
        [SerializeField, Header("攻擊資料")]
        private DataAttack dataAttack;
        [SerializeField, Header("攻擊動畫名稱")]
        private string nameAnimation;
        protected Animator ani;
        protected bool canAttack = true;
        #region unity方法

        protected virtual void Awake()
        {
            ani = GetComponent<Animator>();
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = dataAttack.attackAreaColor;
            Gizmos.matrix = Matrix4x4.TRS(
    transform.position + transform.TransformDirection(dataAttack.attackAreaOffset),//轉換世界座標
                transform.rotation,
                transform.localScale
                );

            Gizmos.DrawCube(
                Vector3.zero,
                dataAttack.attackAreaSize
                //transform.position+dataAttack.attackAreaOffset,dataAttack.attackAreaSize
                );
        }

        private void Update()
        {

        }
        #endregion

        /// <summary>
        /// 開始攻擊
        /// </summary>
        public void StartAttack()
        {
            if (!canAttack) return;
            StartCoroutine(AttackFlow());
        }
        /// <summary>
        /// 攻擊流程
        /// </summary>
        /// <returns></returns>
        private IEnumerator AttackFlow()
        {
            canAttack = false;
            yield return new WaitForSeconds(dataAttack.delayAttack);
            CheckAttackArea();

            yield return new WaitForSeconds(dataAttack.waitAttackEnd);
            canAttack = true;
            stopAttack();
        }
        /// <summary>
        /// 停止攻擊；保護,虛擬 ，允許子類別覆寫
        /// </summary>
        protected virtual void stopAttack()
        {

        }
        /// <summary>
        /// 檢查攻擊區域是否碰撞到目標圖層
        /// </summary>
        private void CheckAttackArea()
        {
            //如果不是攻擊動畫，就跳出
            if (!ani.GetCurrentAnimatorStateInfo(0).IsName(nameAnimation)) return;

            Collider[] hits = Physics.OverlapBox(
                transform.position + transform.TransformDirection(dataAttack.attackAreaOffset),
                dataAttack.attackAreaSize / 2,
                transform.rotation,
                dataAttack.layerTarget);

            //有碰到東西
            if (hits.Length > 0)
            {
                print(hits[0].name);
                hits[0].GetComponent<HealthSystem>().Hurt(dataAttack.attack);//傳攻擊力，到血量系統的Hrut
            }
        }
    }
}