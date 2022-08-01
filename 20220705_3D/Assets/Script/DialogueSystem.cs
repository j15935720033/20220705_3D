using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;//引用系統集合，資料結構與偕同程序
namespace chia
{
    /// <summary>
    /// 對話系統、淡入對話框，更新 NPC 資料名稱、內容、音效,淡出
    /// </summary>
    /// 
    //RequireComponent:加添加腳本時(加入此Scipt時)，自動加入元件
    [RequireComponent(typeof(AudioSource))]
    public class DialogueSystem : MonoBehaviour
    {
        [SerializeField, Header("畫布對話系統")]
        private CanvasGroup groupDialogue;

        [SerializeField, Header("說話者名稱")]
        private TextMeshProUGUI textName;

        [SerializeField, Header("對話內容")]
        private TextMeshProUGUI textContent;

        [SerializeField, Header("三角形")]
        private GameObject goTriangle;

        private AudioSource aud;
        public DataNPC dataNpc;





        private void Awake()
        {
            aud = GetComponent<AudioSource>();
            StartCoroutine(FadeIn());//啟動偕同程序

            textName.text = dataNpc.nameNPC;//
            textContent.text = "";//清空對話欄
        }

        /// <summary>
        /// 淡入效果
        /// </summary>
        private IEnumerator FadeIn()
        {
            for(int i = 0; i < 10; i++)
            {
                groupDialogue.alpha += 0.1f;
                yield return new WaitForSeconds(0.1f);
            }
            StartCoroutine(TypeEffect());//淡入文字
        }
        /// <summary>
        /// 打字淡入效果
        /// </summary>
        private IEnumerator TypeEffect()
        {
            aud.PlayOneShot(dataNpc.dataDialogue[0].sound);//播聲音
            string content = dataNpc.dataDialogue[0].content;
            for (int i=0;i<content.Length;i++)
            {
                textContent.text += content[i];
                yield return new WaitForSeconds(0.05f);
            }
            goTriangle.SetActive(true);
        }
        #region 偕同程序教學

        
        private IEnumerator Test()
        {
            print("第一行文字");
            yield return new WaitForSeconds(2);
            print("第二行文字");
        }
        #endregion
    }

}
