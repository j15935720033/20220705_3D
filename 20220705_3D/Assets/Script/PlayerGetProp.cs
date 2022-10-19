using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace chia
{
    /// <summary>
    /// 玩家取得道具
    /// </summary>
    public class PlayerGetProp : MonoBehaviour
    {
        private ObjectPoolRock objectPoolRock;
        private string propRock = "石頭碎片";

        private int countRock = 0;
        private int countRockMax = 1;
        private TextMeshProUGUI textCount;

        private NPCSystem npcSystem;
        [SerializeField, Header("完成任務的對話")]
        private DataNPC dataNPC;

        private void Awake()
        {
            //objectPoolRock = FindObjectOfType<ObjectPoolRock>();
            objectPoolRock = GameObject.Find("物件池碎片").GetComponent<ObjectPoolRock>();

            textCount = GameObject.Find("TMP_碎片數量介面").GetComponent<TextMeshProUGUI>();
            npcSystem= GameObject.Find("NPC").GetComponent<NPCSystem>();
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {

            if (hit.gameObject.name.Contains(propRock))
            {
                print("碰到" + hit.gameObject.name);
                objectPoolRock.ReleasePoolObject(hit.gameObject);
                UpdateUI();
            }
        }
        private void UpdateUI()
        {
            textCount.text = "碎片" + (++countRock) + "/" + countRockMax;
            if (countRock >= countRockMax) npcSystem.dataNpc = dataNPC;
        }
    }
}