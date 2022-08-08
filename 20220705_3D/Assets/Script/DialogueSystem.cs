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
        //�e��ñ�W�A�L�Ǧ^�P�L�Ѽ�
        public delegate void DelegateFinishDialogue();

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
        [SerializeField, Header("�H�J�ɶ�")]
        private float intervalFadIn = 0.1f;
        [SerializeField, Header("���r���j")]
        private float intervalType = 0.05f;

        

        #region Unity��k
        private void Awake()
        {
            aud = GetComponent<AudioSource>();
            //StartCoroutine(Fade());//�Ұʰ��P�{��
            //StartCoroutine(StartDialogue());//�Ұʰ��P�{��_��������

        }
        #endregion

        #region ���}��ƻP��k
        public bool isDialogue;//�O�_�b��ܤ�

        public IEnumerator StartDialogue(DataNPC _dataNPC,DelegateFinishDialogue callback)
        {
            isDialogue = true;//�]�w��ܤ�

            dataNpc = _dataNPC;
            textName.text = dataNpc.nameNPC;//
            textContent.text = "";//�M�Ź����
            yield return StartCoroutine(Fade());//�H�J�ĪG    yield return�]���o��~�|�]�U�@��

            for (int i = 0; i < dataNpc.dataDialogue.Length; i++)
            {
                yield return StartCoroutine(TypeEffect(i));//�H�J��r

                while (!Input.GetKeyDown(KeyCode.E)) //�p�G�٨S�� ���w����(E) �N���򵥫�
                {
                    yield return null;//�Ǧ^null�A�O����1�Ӽv�j�ɶ�(1/60��)
                }
            }

            StartCoroutine(Fade(false));//�H�X�ĪG

            isDialogue = false;//�]�w��ܵ���

            callback();//����^�I�[��
        }
        #endregion

        /// <summary>
        /// �H�J�ĪG
        /// </summary>
        private IEnumerator Fade(bool fadeIn=true)//���w�]��
        {
            //�T���B��l
            //���L��?���L�Ȭ� true:���L�Ȭ� false
            float increase=fadeIn?0.1f:-0.1f;

            for (int i = 0; i < 10; i++)
            {
                groupDialogue.alpha += increase;
                yield return new WaitForSeconds(intervalFadIn);
            }
            //StartCoroutine(TypeEffect());//�H�J��r
        }
        /// <summary>
        /// ���r�H�J�ĪG�B�����ܭ��ġB��ܤT����
        /// </summary>
        private IEnumerator TypeEffect(int indexDialogue)
        {
            textContent.text = "";//�M�Ź����
            aud.PlayOneShot(dataNpc.dataDialogue[indexDialogue].sound);//�����ܭ���
            string content = dataNpc.dataDialogue[indexDialogue].content;
            for (int i=0;i<content.Length;i++)//���r�H�J�ĪG
            {
                textContent.text += content[i];
                yield return new WaitForSeconds(intervalType);
            }
            goTriangle.SetActive(true);//��ܤT����
        }



        #region ���P�{�Ǳо�
        private IEnumerator Test()
        {
            print("�Ĥ@���r");
            yield return new WaitForSeconds(2);//��2��
            print("�ĤG���r");
        }
        #endregion
    }

}
