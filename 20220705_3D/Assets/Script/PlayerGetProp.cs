using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace chia
{
    /// <summary>
    /// ���a���o�D��
    /// </summary>
    public class PlayerGetProp : MonoBehaviour
    {
        private ObjectPoolRock objectPoolRock;
        private string propRock = "���Y�H��";

        private int countRock = 0;
        private int countRockMax = 1;
        private TextMeshProUGUI textCount;

        private NPCSystem npcSystem;
        [SerializeField, Header("�������Ȫ����")]
        private DataNPC dataNPC;

        private void Awake()
        {
            //objectPoolRock = FindObjectOfType<ObjectPoolRock>();
            objectPoolRock = GameObject.Find("������H��").GetComponent<ObjectPoolRock>();

            textCount = GameObject.Find("TMP_�H���ƶq����").GetComponent<TextMeshProUGUI>();
            npcSystem= GameObject.Find("NPC").GetComponent<NPCSystem>();
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {

            if (hit.gameObject.name.Contains(propRock))
            {
                print("�I��" + hit.gameObject.name);
                objectPoolRock.ReleasePoolObject(hit.gameObject);
                UpdateUI();
            }
        }
        private void UpdateUI()
        {
            textCount.text = "�H��" + (++countRock) + "/" + countRockMax;
            if (countRock >= countRockMax) npcSystem.dataNpc = dataNPC;
        }
    }
}