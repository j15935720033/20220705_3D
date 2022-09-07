using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;//�ޥνs�边�R�W�Ŷ�
namespace chia
{
    
    /// <summary>
    /// ��q���
    /// </summary>
    [CreateAssetMenu(menuName = "Chia/DataHealth", fileName = "DataHealth")]
    public class DataHealth : ScriptableObject
    {
        [Header("��q"), Range(0, 10000)]
        public float hp;
        [HideInInspector]
        public float hpMax => hp;
        [Header("�O�_�����_��")]
        public bool isDropProp;
        [HideInInspector,Header("�_�����m��")]//������ø�X�{�A�����|����
        public GameObject goProp;
        [HideInInspector,Header("�_�������v"), Range(0f, 1f)]
        public float propProbability;
    }
    //�ۭq�s�边(���O(�n�ۭq�s�边�����O)
    [CustomEditor(typeof(DataHealth))]
    public class DataHealthEditor : Editor
    {
        //�s�边_
        //�ǦC���ݩ� �ۭq�W��
        SerializedProperty spIsDropProp;//�O�_�����_��
        SerializedProperty spGoProp;//�_�����m��
        SerializedProperty spPropProbability;//�_�������v

        //�Ұʨƥ�:�Ӫ���Τ�����ܮɰ���@��
        private void OnEnable()
        {
            //�ǦC�ƪ���.�M���ݩ�(�W��(���O.��ƦW��))

            //serializedObject:���s�边
            //���DataHealth.isDropProp��ƶǵ�spIsDropProp
            spIsDropProp = serializedObject.FindProperty(nameof(DataHealth.isDropProp));
            spGoProp = serializedObject.FindProperty(nameof(DataHealth.goProp));
            spPropProbability = serializedObject.FindProperty(nameof(DataHealth.propProbability));
        }

        //OnInspectorGUI:�ݩʭ��O����
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();//�e�X�w�]�Ȥ���
            serializedObject.Update();//��s

            if (spIsDropProp.boolValue)//spIsDropProp�O������ݩʸ�ơA�n�নbool��
            {
                EditorGUILayout.PropertyField(spGoProp);//EditorGUILayout:�s�边�����A�ͦ����
                EditorGUILayout.PropertyField(spPropProbability);
            }
            serializedObject.ApplyModifiedProperties();//�s�边�n�A�M�ΥH�W�ܧ�
        }

    }

}