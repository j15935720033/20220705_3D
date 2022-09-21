using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
namespace chia
{
    public class EnemyHealth : HealthSystem
    {
        private EnemySystem enemySystem;//�ĤH�t��
        private Material matDissolve;
        private string nameDissolve = "DissolveValue";
        private float maxDissolve = 2f;
        private float minDissolve = -3f;

        private ObjectPoolRock objectPoolRock;//���Y�H�������
        protected override void Awake()
        {
            base.Awake();
            enemySystem = GetComponent<EnemySystem>();

            //Renderer �� Skinned Mesh Renderer �P Mesh Renderer ��������
            //���o Renderer �i�H�A�Ω�ҫ��M�Τ��P���󪺪��p
            //GetComponentsInChildren ���o�l����̪�����A�^�}�C
            matDissolve = GetComponentsInChildren<Renderer>()[0].material;

            objectPoolRock = FindObjectOfType<ObjectPoolRock>();
        }
        /// <summary>
        /// ���`
        /// </summary>
        protected override void Dead()
        {
            base.Dead();
            enemySystem.enabled = false;//�����ĤH�t��
            DropProp();
            StartCoroutine(Dissolve());
        }
        private IEnumerator Dissolve()
        {
            while(maxDissolve> minDissolve)
            {
                maxDissolve -= 0.1f;
                matDissolve.SetFloat(nameDissolve, maxDissolve);
                yield return new WaitForSeconds(0.03f);
            }
        }
        /// <summary>
        /// �����D��
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