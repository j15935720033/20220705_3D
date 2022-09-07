using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;//引用編輯器命名空間
namespace chia
{
    
    /// <summary>
    /// 血量資料
    /// </summary>
    [CreateAssetMenu(menuName = "Chia/DataHealth", fileName = "DataHealth")]
    public class DataHealth : ScriptableObject
    {
        [Header("血量"), Range(0, 10000)]
        public float hp;
        [HideInInspector]
        public float hpMax => hp;
        [Header("是否掉落寶物")]
        public bool isDropProp;
        [HideInInspector,Header("寶物欲置物")]//讓打勾繪出現，取消會不見
        public GameObject goProp;
        [HideInInspector,Header("寶物掉落率"), Range(0f, 1f)]
        public float propProbability;
    }
    //自訂編輯器(類別(要自訂編輯器的類別)
    [CustomEditor(typeof(DataHealth))]
    public class DataHealthEditor : Editor
    {
        //編輯器_
        //序列化屬性 自訂名稱
        SerializedProperty spIsDropProp;//是否掉落寶物
        SerializedProperty spGoProp;//寶物欲置物
        SerializedProperty spPropProbability;//寶物掉落率

        //啟動事件:該物件或元件顯示時執行一次
        private void OnEnable()
        {
            //序列化物件.尋找屬性(名稱(類別.資料名稱))

            //serializedObject:此編輯器
            //找到DataHealth.isDropProp資料傳給spIsDropProp
            spIsDropProp = serializedObject.FindProperty(nameof(DataHealth.isDropProp));
            spGoProp = serializedObject.FindProperty(nameof(DataHealth.goProp));
            spPropProbability = serializedObject.FindProperty(nameof(DataHealth.propProbability));
        }

        //OnInspectorGUI:屬性面板介面
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();//畫出預設值介面
            serializedObject.Update();//更新

            if (spIsDropProp.boolValue)//spIsDropProp是物件化屬性資料，要轉成bool值
            {
                EditorGUILayout.PropertyField(spGoProp);//EditorGUILayout:編輯器介面，生成欄位
                EditorGUILayout.PropertyField(spPropProbability);
            }
            serializedObject.ApplyModifiedProperties();//編輯器要，套用以上變更
        }

    }

}