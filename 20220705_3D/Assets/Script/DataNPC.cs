using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace chia
{


    /// <summary>
    /// NPC 資料:名稱、對話內容與音效
    /// ScriptableObject:腳本化物件(將程式內容儲存為物件放在Project內
    /// </summary>
    [CreateAssetMenu(menuName ="Chia/Data NPC",fileName ="Data NPC")]
    public class DataNPC : ScriptableObject
    {
        [Header("DataNPC 名稱")]
        public string nameNPC;

        //NonReorderable:不要排列，解決陣列在屬性面板顯示Bug
        [Header("對話內容、音效陣列"),NonReorderable]
        public DataDialogue[] dataDialogue;
    }

    [System.Serializable]
    public class DataDialogue
    {
        [Header("對話內容")]
        public string content;
        [Header("對話音效")]
        public AudioClip sound;
    }
}
