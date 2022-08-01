using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace chia
{


    /// <summary>
    /// NPC ���:�W�١B��ܤ��e�P����
    /// ScriptableObject:�}���ƪ���(�N�{�����e�x�s�������bProject��
    /// </summary>
    [CreateAssetMenu(menuName ="Chia/Data NPC",fileName ="Data NPC")]
    public class DataNPC : ScriptableObject
    {
        [Header("DataNPC �W��")]
        public string nameNPC;

        //NonReorderable:���n�ƦC�A�ѨM�}�C�b�ݩʭ��O���Bug
        [Header("��ܤ��e�B���İ}�C"),NonReorderable]
        public DataDialogue[] dataDialogue;
    }

    [System.Serializable]
    public class DataDialogue
    {
        [Header("��ܤ��e")]
        public string content;
        [Header("��ܭ���")]
        public AudioClip sound;
    }
}
