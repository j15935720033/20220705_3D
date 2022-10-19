using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace chia
{
    /// <summary>
    /// �ͦ��ĤH�t��
    /// </summary>

    [DefaultExecutionOrder(200)]
    public class SpawnEnemySystem : MonoBehaviour
    {
        [SerializeField, Header("���s�ͦ��ɶ��d��")]
        private Vector2 rangeRespawn = new Vector2(2,5);

        private ObjectPoolGolem objectPoolGolem;
        private GameObject enemyObject;

        // Start is called before the first frame update
        void Start()
        {
            objectPoolGolem = GameObject.Find("�����_���Y�H").GetComponent<ObjectPoolGolem>();
            Spawn();
        }

        /// <summary>
        /// �ͦ�
        /// </summary>
        private void Spawn()
        {
            enemyObject = objectPoolGolem.GetPoolObject();
            enemyObject.transform.position = transform.position;//�]�w���X�Ӫ��󪺦�m

            enemyObject.GetComponent<EnemyHealth>().onDead= EnemyDead;
        }
        /// <summary>
        /// �Ǫ����`
        /// </summary>
        private void EnemyDead()
        {
            objectPoolGolem.ReleasePoolObject(enemyObject);

            float randomTime = Random.Range(rangeRespawn.x, rangeRespawn.y);
            Invoke("Spawn", randomTime);
        }
    }
}