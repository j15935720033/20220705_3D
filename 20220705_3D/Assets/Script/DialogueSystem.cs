using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;//�ޥΨt�ζ��X�A��Ƶ��c�P���P�{��
namespace chia
{
    /// <summary>
    /// ��ܨt�ΡB�H�J��ܮءA��s NPC ��ƦW�١B���e�B����,�H�X
    /// </summary>
    /// 
    //RequireComponent:�[�K�[�}����(�[�J��Scipt��)�A�۰ʥ[�J����
    [RequireComponent(typeof(AudioSource))]
    public class DialogueSystem : MonoBehaviour
    {
        [SerializeField, Header("�e����ܨt��")]
        private CanvasGroup groupDialogue;

        [SerializeField, Header("���ܪ̦W��")]
        private TextMeshProUGUI textName;

        [SerializeField, Header("��ܤ��e")]
        private TextMeshProUGUI textContent;

        [SerializeField, Header("�T����")]
        private GameObject goTriangle;

        private AudioSource aud;
        public DataNPC dataNpc;





        private void Awake()
        {
            aud = GetComponent<AudioSource>();
            StartCoroutine(FadeIn());//�Ұʰ��P�{��

            textName.text = dataNpc.nameNPC;//
            textContent.text = "";//�M�Ź����
        }

        /// <summary>
        /// �H�J�ĪG
        /// </summary>
        private IEnumerator FadeIn()
        {
            for(int i = 0; i < 10; i++)
            {
                groupDialogue.alpha += 0.1f;
                yield return new WaitForSeconds(0.1f);
            }
            StartCoroutine(TypeEffect());//�H�J��r
        }
        /// <summary>
        /// ���r�H�J�ĪG
        /// </summary>
        private IEnumerator TypeEffect()
        {
            aud.PlayOneShot(dataNpc.dataDialogue[0].sound);//���n��
            string content = dataNpc.dataDialogue[0].content;
            for (int i=0;i<content.Length;i++)
            {
                textContent.text += content[i];
                yield return new WaitForSeconds(0.05f);
            }
            goTriangle.SetActive(true);
        }
        #region ���P�{�Ǳо�

        
        private IEnumerator Test()
        {
            print("�Ĥ@���r");
            yield return new WaitForSeconds(2);
            print("�ĤG���r");
        }
        #endregion
    }

}
