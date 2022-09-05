using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace chia
{
    /// <summary>
    /// �������a�O�_���i�ϰ줺,��ܴ��ܵe��,���䰻���ñҰʹ�ܨt��
    /// </summary>
    public class NPCSystem : MonoBehaviour
    {
       

        [SerializeField, Header("NPC ��ܨt��")]
        private DataNPC dataNpc;
        [SerializeField, Header("NPC ��v��")]
        private GameObject goCamera;

        private Animator aniTip;
        private string parTipFad= "Trigger_���ܲH�X�H�J";
        private bool isInTrigger;//���a�O�_���i��ܸI���ϰ줺
        private ThirdPersonController thirdPersonController;//���a����
        private DialogueSystem dialogueSystem;
        private Animator ani;
        private string parDialogue = "��ܶ}��";
        private void Awake()
        {
            aniTip = GameObject.Find("Image_���ܩ���").GetComponent<Animator>();
            thirdPersonController = FindObjectOfType<ThirdPersonController>();//FindObjectOfType:�j�M����A�ȭ��u���@��GameObjet���o�Ӥ���
            //t = FindObjectsOfType<test>();//��@��Script��b�ܦhGameObject�W�A�Ǧ^�}�C
            dialogueSystem = FindObjectOfType<DialogueSystem>();//��Script
            ani = GetComponent<Animator>();
        }
        private void Update()
        {
            InputKeyAndStarDialogue();
        }

        //�I���ƥ�
        //1.��Ӫ���ܤ֦��@�� Rigidbody
        //2.���Ŀ� Trigger �ϥ�OnTrigger �ƥ�:[Enter�BExit�Bstay]
        private void OnTriggerEnter(Collider other)
        {
            //print("�i�J�����ϰ�"+other.name);
            CheckPlayerAndAnimation(other.name,true);

        }
        private void OnTriggerExit(Collider other)
        {
            //print("���}�����ϰ�" + other.name);
            CheckPlayerAndAnimation(other.name,false);
        }

        /// <summary>
        /// �ˬd���a�O�_�i�J�����}�ç�s�ʵe
        /// </summary>
        /// <param name="nameHit">�I������W��</param>
        private void CheckPlayerAndAnimation(string nameHit,bool isInTrigger)
        {
            if (nameHit=="���Y����")
            {
                this.isInTrigger = isInTrigger;
                aniTip.SetTrigger(parTipFad);
            }
        }
        /// <summary>
        /// ��ܤ���E���
        /// </summary>
        private void InputKeyAndStarDialogue()
        {
            if (this.isInTrigger && Input.GetKeyDown(KeyCode.E))
            {
                //print("���U E �}�l���");

                
                if (dialogueSystem.isDialogue)//��ܤ��A�Nreturn 
                {
                    return;
                }
                goCamera.SetActive(true);
                aniTip.SetTrigger(parTipFad);
                thirdPersonController.enabled = false;//����������C�T���
                try
                {
                    ani.SetBool(parDialogue, true);//�}�ҹ�ܰʵe
                }
                catch(System.Exception)
                {
                    print("<Color=#993311>�ʤ֤�����~�ANPC�S�� Animation</color>");
                    //throw
                }
                StartCoroutine(dialogueSystem.StartDialogue(dataNpc, ResetControllerAndCloseCamera));//StartDialogue��k�OIEnumerator �ҥH�n��StartCoroutine �Ұʰ��P�{�ǡC�ѼƬOdelegate�ҥH�n�Ǥ�k
            }
        }
        /// <summary>
        /// ���s�]�w����P������v��
        /// </summary>
        private void ResetControllerAndCloseCamera()
        {
            goCamera.SetActive(false);//������v��GameObject
            thirdPersonController.enabled = true;
            aniTip.SetTrigger(parTipFad);
            
            try
            {
                ani.SetBool(parDialogue, false);//������ܰʵe
            }
            catch (System.Exception)
            {
                print("<Color=#993311>�ʤ֤�����~�ANPC�S�� Animation</color>");
                //throw
            }
        }
    }
}

