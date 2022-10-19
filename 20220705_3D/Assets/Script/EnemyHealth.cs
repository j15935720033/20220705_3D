using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
namespace chia
{
    public class EnemyHealth : HealthSystem
    {
        private EnemySystem enemySystem;//敵人系統
        private Material matDissolve;
        private string nameDissolve = "DissolveValue";
        private float maxDissolve = 2f;
        private float minDissolve = -3f;

        private ObjectPoolRock objectPoolRock;//石頭碎片物件池
        public delegate void delegateDead();
        /// <summary>
        /// 死亡事件
        /// </summary>
        public delegateDead onDead;

        /// <summary>
        /// 遊戲物件被隱藏時執行一次
        /// </summary>
        private void OnDisable()
        {
            
        }

        /// <summary>
        /// 遊戲物件被顯示時執行一次
        /// 熟投人數遽還原成初始值
        /// </summary>
        private void OnEnable()
        {
            hp = dataHealth.hp;
            imageHealth.fillAmount = 1;
            enemySystem.enabled = true;
            matDissolve.SetFloat(nameDissolve,2.5f);
            maxDissolve = 2.5f;
        }
        protected override void Awake()
        {
            base.Awake();
            enemySystem = GetComponent<EnemySystem>();

            //Renderer 為 Skinned Mesh Renderer 與 Mesh Renderer 的父物件
            //取得 Renderer 可以適用於模型套用不同元件的狀況
            //GetComponentsInChildren 取得子物件們的元件，回陣列
            matDissolve = GetComponentsInChildren<Renderer>()[0].material;

            //objectPoolRock = FindObjectOfType<ObjectPoolRock>();
            objectPoolRock = GameObject.Find("物件池碎片").GetComponent<ObjectPoolRock>();
            
            //onDead();寫到溶解效果
        }
        /// <summary>
        /// 死亡
        /// </summary>
        protected override void Dead()
        {
            base.Dead();
            enemySystem.enabled = false;//關閉敵人系統
            DropProp();
            StartCoroutine(Dissolve());
        }
        /// <summary>
        /// 溶解效果
        /// </summary>
        /// <returns></returns>
        private IEnumerator Dissolve()
        {
            while(maxDissolve> minDissolve)
            {
                maxDissolve -= 0.1f;
                matDissolve.SetFloat(nameDissolve, maxDissolve);
                yield return new WaitForSeconds(0.03f);
            }
            onDead();
        }
        /// <summary>
        /// 掉落道具
        /// </summary>
        private void DropProp()
        {
            float value = Random.value;
            if (value <= dataHealth.propProbability)
            {
                /*
                Instantiate(
                    dataHealth.goProp,
                    transform.position + Vector3.up * 6,
                    Quaternion.identity
                );
                */
                GameObject tempObject = objectPoolRock.GetPoolObject();
                tempObject.transform.position = transform.position + Vector3.up * 3;
            }

            
        }
    }
}