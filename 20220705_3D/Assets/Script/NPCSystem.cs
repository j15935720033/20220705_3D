using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace chia
{
    /// <summary>
    /// 偵測玩家是否走進區域內,顯示提示畫面,按鍵偵測並啟動對話系統
    /// </summary>
    public class NPCSystem : MonoBehaviour
    {
       

        [SerializeField, Header("NPC 對話系統")]
        private DataNPC dataNpc;
        [SerializeField, Header("NPC 攝影機")]
        private GameObject goCamera;

        private Animator aniTip;
        private string parTipFad= "Trigger_提示淡出淡入";
        private bool isInTrigger;//玩家是否走進對話碰撞區域內
        private ThirdPersonController thirdPersonController;//玩家移動
        private DialogueSystem dialogueSystem;
        private Animator ani;
        private string parDialogue = "對話開關";
        private void Awake()
        {
            aniTip = GameObject.Find("Image_提示底圖").GetComponent<Animator>();
            thirdPersonController = FindObjectOfType<ThirdPersonController>();//FindObjectOfType:搜尋元件，僅限只有一個GameObjet有這個元件
            //t = FindObjectsOfType<test>();//找一個Script放在很多GameObject上，傳回陣列
            dialogueSystem = FindObjectOfType<DialogueSystem>();//找Script
            ani = GetComponent<Animator>();
        }
        private void Update()
        {
            InputKeyAndStarDialogue();
        }

        //碰撞事件
        //1.兩個物件至少有一個 Rigidbody
        //2.有勾選 Trigger 使用OnTrigger 事件:[Enter、Exit、stay]
        private void OnTriggerEnter(Collider other)
        {
            //print("進入偵測區域"+other.name);
            CheckPlayerAndAnimation(other.name,true);

        }
        private void OnTriggerExit(Collider other)
        {
            //print("離開偵測區域" + other.name);
            CheckPlayerAndAnimation(other.name,false);
        }

        /// <summary>
        /// 檢查玩家是否進入或離開並更新動畫
        /// </summary>
        /// <param name="nameHit">碰撞物件名稱</param>
        private void CheckPlayerAndAnimation(string nameHit,bool isInTrigger)
        {
            if (nameHit=="骨頭先生")
            {
                this.isInTrigger = isInTrigger;
                aniTip.SetTrigger(parTipFad);
            }
        }
        /// <summary>
        /// 對話中按E對話
        /// </summary>
        private void InputKeyAndStarDialogue()
        {
            if (this.isInTrigger && Input.GetKeyDown(KeyCode.E))
            {
                //print("按下 E 開始對話");

                
                if (dialogueSystem.isDialogue)//對話中，就return 
                {
                    return;
                }
                goCamera.SetActive(true);
                aniTip.SetTrigger(parTipFad);
                thirdPersonController.enabled = false;//關閉此元件。禁止移動
                try
                {
                    ani.SetBool(parDialogue, true);//開啟對話動畫
                }
                catch(System.Exception)
                {
                    print("<Color=#993311>缺少元件錯誤，NPC沒有 Animation</color>");
                    //throw
                }
                StartCoroutine(dialogueSystem.StartDialogue(dataNpc, ResetControllerAndCloseCamera));//StartDialogue方法是IEnumerator 所以要用StartCoroutine 啟動偕同程序。參數是delegate所以要傳方法
            }
        }
        /// <summary>
        /// 重新設定控制器與關閉攝影機
        /// </summary>
        private void ResetControllerAndCloseCamera()
        {
            goCamera.SetActive(false);//關閉攝影機GameObject
            thirdPersonController.enabled = true;
            aniTip.SetTrigger(parTipFad);
            
            try
            {
                ani.SetBool(parDialogue, false);//關閉對話動畫
            }
            catch (System.Exception)
            {
                print("<Color=#993311>缺少元件錯誤，NPC沒有 Animation</color>");
                //throw
            }
        }
    }
}

