using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace chia
{
    [CreateAssetMenu(menuName = "Chia/Data Enemy", fileName ="DataEnemy",order =1)]
    public class DataEnemy : ScriptableObject
    {
        [Header("��q"), Range(0, 2000)]
        public float hp;
        [Header("�����O"), Range(0, 200)]
        public float attack;
        [Header("�l�ܶZ��"), Range(0, 200)]
        public float rangeTrack;
        [Header("�����Z��"), Range(0, 10)]
        public float rangeAttack;
        [Header("�����t��"), Range(0, 100)]
        public float speedWalk;
        [Header("�����D����v"), Range(0, 1)]
        public float propBilityProp;
        [Header("�����D��"), Range(0, 2000)]
        public GameObject goProp;
        [Header("���ݮɶ�")]
        public Vector2 timeIdleRange;
        [Header("�n�l�ܪ��ؼйϼh")]
        public LayerMask layerMask;
        [Header("�������j"), Range(0, 5)]
        public float intervalAttack;
    }

}
