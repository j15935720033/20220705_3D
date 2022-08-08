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
        //委派簽名，無傳回與無參數
        public delegate void DelegateFinishDialogue();

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
        [SerializeField, Header("淡入時間")]
        private float intervalFadIn = 0.1f;
        [SerializeField, Header("打字間隔")]
        private float intervalType = 0.05f;

        

        #region Unity方法
        private void Awake()
        {
            aud = GetComponent<AudioSource>();
            //StartCoroutine(Fade());//啟動偕同程序
            //StartCoroutine(StartDialogue());//啟動偕同程序_對話欄測試

        }
        #endregion

        #region 公開資料與方法
        public bool isDialogue;//是否在對話中

        public IEnumerator StartDialogue(DataNPC _dataNPC,DelegateFinishDialogue callback)
        {
            isDialogue = true;//設定對話中

            dataNpc = _dataNPC;
            textName.text = dataNpc.nameNPC;//
            textContent.text = "";//清空對話欄
            yield return StartCoroutine(Fade());//淡入效果    yield return跑完這行才會跑下一行

            for (int i = 0; i < dataNpc.dataDialogue.Length; i++)
            {
                yield return StartCoroutine(TypeEffect(i));//淡入文字

                while (!Input.GetKeyDown(KeyCode.E)) //如果還沒按 指定按鍵(E) 就持續等待
                {
                    yield return null;//傳回null，是等待1個影隔時間(1/60秒)
                }
            }

            StartCoroutine(Fade(false));//淡出效果

            isDialogue = false;//設定對話結束

            callback();//執行回呼涵式
        }
        #endregion

        /// <summary>
        /// 淡入效果
        /// </summary>
        private IEnumerator Fade(bool fadeIn=true)//給預設值
        {
            //三元運算子
            //布林直?布林值為 true:布林值為 false
            float increase=fadeIn?0.1f:-0.1f;

            for (int i = 0; i < 10; i++)
            {
                groupDialogue.alpha += increase;
                yield return new WaitForSeconds(intervalFadIn);
            }
            //StartCoroutine(TypeEffect());//淡入文字
        }
        /// <summary>
        /// 打字淡入效果、播放對話音效、顯示三角形
        /// </summary>
        private IEnumerator TypeEffect(int indexDialogue)
        {
            textContent.text = "";//清空對話欄
            aud.PlayOneShot(dataNpc.dataDialogue[indexDialogue].sound);//播放對話音效
            string content = dataNpc.dataDialogue[indexDialogue].content;
            for (int i=0;i<content.Length;i++)//打字淡入效果
            {
                textContent.text += content[i];
                yield return new WaitForSeconds(intervalType);
            }
            goTriangle.SetActive(true);//顯示三角形
        }



        #region 偕同程序教學
        private IEnumerator Test()
        {
            print("第一行文字");
            yield return new WaitForSeconds(2);//等2秒
            print("第二行文字");
        }
        #endregion
    }

}
