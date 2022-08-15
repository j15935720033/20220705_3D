using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace chia
{
    public class EnemySystem : MonoBehaviour
    {
        #region ���
        [SerializeField, Header("�ĤH���")]
        private DataEnemy dataEnemy;
        private Animator ani;
        private NavMeshAgent nma;
        private Vector3 v3TargetPosition;//AI�b�餺�H�����ʪ��I


        [SerializeField]
        private StateEnemy stateEnemy;//AI���A

        private string parWalk = "�}������";
        private string parAttack = "Ĳ�o����";
        private float timerIdle;//���ݮɶ�(�ɶ��p�ɾ�)
        private float timeAttack;//�W�O�ɶ�(�ɶ��p�ɾ�)

        #endregion


        #region unity��k
        private void Awake()
        {
            ani = GetComponent<Animator>();
            nma = GetComponent<NavMeshAgent>();
            nma.speed = dataEnemy.speedWalk;//�]�wAI�t��
        }

        private void Update()
        {
            StateSwitcher();
            CheckerTargetInTrackRange();
        }

        private void OnDrawGizmosSelected()//����쪫��~�|��ܦۻsø�s�ϧ�
        {
            Gizmos.color = new Color(0, 1, 0.2f, 0.3f);
            Gizmos.DrawSphere(transform.position, dataEnemy.rangeTrack);//�����d��

            Gizmos.color = new Color(1, 0.2f, 0.2f, 0.3f);
            Gizmos.DrawSphere(transform.position, dataEnemy.rangeAttack);//�����d��
        }

        private void OnDrawGizmos()//���ο������N��ܦۻsø�s�ϧ�
        {
            /*Gizmos.color = new Color(0, 1, 0.2f, 0.3f);
            Gizmos.DrawSphere(transform.position, dataEnemy.rangeTrack);//�����d��

            Gizmos.color = new Color(1, 0.2f, 0.2f, 0.3f);
            Gizmos.DrawSphere(transform.position, dataEnemy.rangeAttack);//�����d��
            */
            Gizmos.color = new Color(1, 0.2f, 0.2f, 0.3f);
            Gizmos.DrawSphere(v3TargetPosition, 0.3f);//�C���d��
        }
        #endregion

        #region �ۭq��k
        /// <summary>
        /// ���A�ഫ��
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
        /// �C��
        /// </summary>
        private void Wander()
        {
            //print("�Ѿl�Z��"+ nma.remainingDistance);
            //�p�G�Ѿl�Z������0
            if (nma.remainingDistance == 0)//nma�p�G��}�l�S���y�СA�w�]remainingDistance�|�O0
            {
                //�H���y�� = AI�Ǫ���m + �H���餺���I * �l�ܽd��
                v3TargetPosition = transform.position + Random.insideUnitSphere * dataEnemy.rangeTrack;
                v3TargetPosition.y = transform.position.y;//���׳]�w����Ǫ��@�˰�
            }

            //SetDestination(��n���ʨ쪺�ت�vector3)
            nma.SetDestination(v3TargetPosition);//�n���h���y��
            //print(nma.velocity);
            ani.SetBool(parWalk, nma.velocity.magnitude > 0.1f);//.magnitude �V�q��
        }

        /// <summary>
        /// ����
        /// </summary>
        private void Idle()
        {
            nma.velocity = Vector3.zero;//�[�t���k0
            ani.SetBool(parWalk, false);//���������ʵe
            timerIdle += Time.deltaTime;//Time.deltaTime :�@�Ӽv��ɶ�
            //print("���ݮɶ�" + timerIdle);

            float r = Random.Range(dataEnemy.timeIdleRange.x, dataEnemy.timeIdleRange.y);//�H�����d�ɶ�

            //�p�ɾ��j�� ���d�ɶ��A�N�����C�����A
            if (timerIdle >= r)
            {
                timerIdle = 0;
                stateEnemy = StateEnemy.Wander;//�������C�����A
            }
        }

        /// <summary>
        /// �l��
        /// </summary>
        private void Track()
        {
            nma.SetDestination(v3TargetPosition);//�¦V�����쪺���a����
            ani.SetBool(parWalk, true);
            //print(nma.remainingDistance);
            if (Vector3.Distance(transform.position,v3TargetPosition) <= dataEnemy.rangeAttack)//�Ǫ��򪱮a�Z��:Vector3.Distance(transform.position,v3TargetPosition)
            {
                stateEnemy = StateEnemy.Attack;
                print("�i�J�������A");
            }

        }

        private void Attack()
        {
            ani.SetBool(parWalk, false);//���������ʵe
            nma.velocity = Vector3.zero;//�����

            if (timeAttack < dataEnemy.intervalAttack)//�W�O2��
            {
                timeAttack += Time.deltaTime;
            }
            else
            {
                ani.SetTrigger(parAttack);
                timeAttack = 0;//�W�O�p�ɾ��k0
            }
        }
        /// <summary>
        /// �ˬd�ؼЬO�_�b�l�ܽd��
        /// </summary>
        private void CheckerTargetInTrackRange()
        {
            if (stateEnemy == StateEnemy.Attack) return;//�p�G�b�������A�N���n�ˬd�ؼЬO�_�b�l�ܽd��

            Collider[] hits = Physics.OverlapSphere(transform.position, dataEnemy.rangeTrack, dataEnemy.layerMask);
            if (hits.Length > 0)
            {
                print("�I�쪺����" + hits[0].name);
                v3TargetPosition = hits[0].transform.position;//�����쪺��m
                stateEnemy = StateEnemy.Track;
            }
        }
        #endregion
    }



}

