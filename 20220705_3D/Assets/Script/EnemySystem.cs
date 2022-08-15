using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace chia
{
    public class EnemySystem : MonoBehaviour
    {
        #region 資料
        [SerializeField, Header("敵人資料")]
        private DataEnemy dataEnemy;
        private Animator ani;
        private NavMeshAgent nma;
        private Vector3 v3TargetPosition;//AI在園內隨機移動的點


        [SerializeField]
        private StateEnemy stateEnemy;//AI狀態

        private string parWalk = "開關走路";
        private string parAttack = "觸發攻擊";
        private float timerIdle;//等待時間(時間計時器)
        private float timeAttack;//蓄力時間(時間計時器)

        #endregion


        #region unity方法
        private void Awake()
        {
            ani = GetComponent<Animator>();
            nma = GetComponent<NavMeshAgent>();
            nma.speed = dataEnemy.speedWalk;//設定AI速度
        }

        private void Update()
        {
            StateSwitcher();
            CheckerTargetInTrackRange();
        }

        private void OnDrawGizmosSelected()//有選到物件才會顯示自製繪製圖形
        {
            Gizmos.color = new Color(0, 1, 0.2f, 0.3f);
            Gizmos.DrawSphere(transform.position, dataEnemy.rangeTrack);//偵測範圍

            Gizmos.color = new Color(1, 0.2f, 0.2f, 0.3f);
            Gizmos.DrawSphere(transform.position, dataEnemy.rangeAttack);//攻擊範圍
        }

        private void OnDrawGizmos()//不用選取物件就顯示自製繪製圖形
        {
            /*Gizmos.color = new Color(0, 1, 0.2f, 0.3f);
            Gizmos.DrawSphere(transform.position, dataEnemy.rangeTrack);//偵測範圍

            Gizmos.color = new Color(1, 0.2f, 0.2f, 0.3f);
            Gizmos.DrawSphere(transform.position, dataEnemy.rangeAttack);//攻擊範圍
            */
            Gizmos.color = new Color(1, 0.2f, 0.2f, 0.3f);
            Gizmos.DrawSphere(v3TargetPosition, 0.3f);//遊走範圍
        }
        #endregion

        #region 自訂方法
        /// <summary>
        /// 狀態轉換器
        /// </summary>
        private void StateSwitcher()
        {
            switch (stateEnemy)
            {
                case StateEnemy.Idle:
                    Idle();
                    break;
                case StateEnemy.Wander:
                    Wander();
                    break;
                case StateEnemy.Track:
                    Track();
                    break;
                case StateEnemy.Attack:
                    Attack();
                    break;
                default:
                    break;
            }

        }

        /// <summary>
        /// 遊走
        /// </summary>
        private void Wander()
        {
            //print("剩餘距離"+ nma.remainingDistance);
            //如果剩餘距離等於0
            if (nma.remainingDistance == 0)//nma如果剛開始沒給座標，預設remainingDistance會是0
            {
                //隨機座標 = AI怪物位置 + 隨機園內的點 * 追蹤範圍
                v3TargetPosition = transform.position + Random.insideUnitSphere * dataEnemy.rangeTrack;
                v3TargetPosition.y = transform.position.y;//高度設定成跟怪物一樣高
            }

            //SetDestination(放要移動到的目的vector3)
            nma.SetDestination(v3TargetPosition);//要走去的座標
            //print(nma.velocity);
            ani.SetBool(parWalk, nma.velocity.magnitude > 0.1f);//.magnitude 向量化
        }

        /// <summary>
        /// 等待
        /// </summary>
        private void Idle()
        {
            nma.velocity = Vector3.zero;//加速度歸0
            ani.SetBool(parWalk, false);//關閉走路動畫
            timerIdle += Time.deltaTime;//Time.deltaTime :一個影格時間
            //print("等待時間" + timerIdle);

            float r = Random.Range(dataEnemy.timeIdleRange.x, dataEnemy.timeIdleRange.y);//隨機停留時間

            //計時器大於 停留時間，就切成遊走狀態
            if (timerIdle >= r)
            {
                timerIdle = 0;
                stateEnemy = StateEnemy.Wander;//切換成遊走狀態
            }
        }

        /// <summary>
        /// 追蹤
        /// </summary>
        private void Track()
        {
            nma.SetDestination(v3TargetPosition);//朝向偵測到的玩家移動
            ani.SetBool(parWalk, true);
            //print(nma.remainingDistance);
            if (Vector3.Distance(transform.position,v3TargetPosition) <= dataEnemy.rangeAttack)//怪物跟玩家距離:Vector3.Distance(transform.position,v3TargetPosition)
            {
                stateEnemy = StateEnemy.Attack;
                print("進入攻擊狀態");
            }

        }

        private void Attack()
        {
            ani.SetBool(parWalk, false);//關閉走路動畫
            nma.velocity = Vector3.zero;//停止移動

            if (timeAttack < dataEnemy.intervalAttack)//蓄力2秒
            {
                timeAttack += Time.deltaTime;
            }
            else
            {
                ani.SetTrigger(parAttack);
                timeAttack = 0;//蓄力計時器歸0
            }
        }
        /// <summary>
        /// 檢查目標是否在追蹤範圍
        /// </summary>
        private void CheckerTargetInTrackRange()
        {
            if (stateEnemy == StateEnemy.Attack) return;//如果在攻擊狀態就不要檢查目標是否在追蹤範圍

            Collider[] hits = Physics.OverlapSphere(transform.position, dataEnemy.rangeTrack, dataEnemy.layerMask);
            if (hits.Length > 0)
            {
                print("碰到的物件" + hits[0].name);
                v3TargetPosition = hits[0].transform.position;//偵測到的位置
                stateEnemy = StateEnemy.Track;
            }
        }
        #endregion
    }



}

