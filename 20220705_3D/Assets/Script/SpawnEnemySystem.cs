using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace chia
{
    /// <summary>
    /// 生成敵人系統
    /// </summary>

    [DefaultExecutionOrder(200)]
    public class SpawnEnemySystem : MonoBehaviour
    {
        [SerializeField, Header("重新生成時間範圍")]
        private Vector2 rangeRespawn = new Vector2(2,5);

        private ObjectPoolGolem objectPoolGolem;
        private GameObject enemyObject;

        // Start is called before the first frame update
        void Start()
        {
            objectPoolGolem = GameObject.Find("物件池_石頭人").GetComponent<ObjectPoolGolem>();
            Spawn();
        }

        /// <summary>
        /// 生成
        /// </summary>
        private void Spawn()
        {
            enemyObject = objectPoolGolem.GetPoolObject();
            enemyObject.transform.position = transform.position;//設定拿出來物件的位置

            enemyObject.GetComponent<EnemyHealth>().onDead= EnemyDead;
        }
        /// <summary>
        /// 怪物死亡
        /// </summary>
        private void EnemyDead()
        {
            objectPoolGolem.ReleasePoolObject(enemyObject);

            float randomTime = Random.Range(rangeRespawn.x, rangeRespawn.y);
            Invoke("Spawn", randomTime);
        }
    }
}