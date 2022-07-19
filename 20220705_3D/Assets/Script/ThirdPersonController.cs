using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace chia
{

    /// <summary>
    /// �ĤT�H�ٱ��
    /// ���ʻP�򥻸��D����B�ʵe��s
    /// </summary>
    public class ThirdPersonController : MonoBehaviour
    {
        #region ���
        [SerializeField, Header("���ʳt��"), Range(0, 50)]
        private float speed = 0.2f;
        [SerializeField, Header("����t��"), Range(0, 50)]
        private float turn = 5f;
        [SerializeField, Header("���D�t��"), Range(0, 50)]
        private float jump = 7f;

        private Animator ani;
        private CharacterController characterController;
        private Vector3 direction;
        private Transform transMainCamera;
        private string parRun = "Blend_�B�I��";
        #endregion

        #region �ƥ�
        private void Awake()
        {
            ani = GetComponent<Animator>();
            characterController = GetComponent<CharacterController>();
            //�z�L�W�ٷj�M����A��ĳ��bAwake��Stat�Ϊ̰���@�����[�c��
            transMainCamera = GameObject.Find("Main Camera").transform;
            //transCamera = GameObject.Find("Main Camera").GetComponent<Transform>();
        }
        private void Update()
        {
            Move();
            Jump();
        }
        #endregion

        #region ��k
        /// <summary>
        /// ����
        /// </summary>
        private void Move()
        {
            float v = Input.GetAxisRaw("Vertical");//0�B1�B-1
            float h = Input.GetAxisRaw("Horizontal");//0�B1�B-1
            //print("<color=yellow>�����b�V:"+v+"</color>");
            //*****************����*********************//
            // transform.rotation = transMainCamera.rotation;//�S���L��
            //���a����=�|����.����(���a����,��v������,�t��*�C�V�ɶ�)
            transform.rotation = Quaternion.Lerp(transform.rotation, transMainCamera.rotation, turn * Time.deltaTime);
            //euler Angles 0�B45�B90�B180�B360��
            //�T�wX�PZ�b���׬�0
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            //transform.rotation = new Quaternion(0, transform.rotation.y, 0,1);

            direction.z = v;
            direction.x = h;
            direction = transform.TransformDirection(direction);//�N����ϰ�y�СA�ন�@�ɮy��

            //���ⱱ�
            //Time.deltaTime(�C�V�ɶ�)
            characterController.Move(direction * speed * Time.deltaTime);

            //�ʵe��s
            float vAxis = Input.GetAxis("Vertical");//����:-1~1
            float hAxis = Input.GetAxis("Horizontal");//����:-1~1
            if (Mathf.Abs(vAxis)>0.1f)
            {
                ani.SetFloat(parRun, Mathf.Abs(vAxis));
            }else if (Mathf.Abs(hAxis) > 0.1f)
            {
                ani.SetFloat(parRun, Mathf.Abs(hAxis));
            }
            else
            {
                ani.SetFloat(parRun, 0);
            }
        }
        /// <summary>
        /// ���D
        /// </summary>
        private void Jump()
        {
            //�p�G�b�a���W �åB ���U�ťի� �N���D
            if (characterController.isGrounded && Input.GetKeyDown(KeyCode.Space))
            {
                direction.y = jump;
            }
            //�a�ߤޤO -9.81
            direction.y += Physics.gravity.y * Time.deltaTime;
        }
        #endregion
    }
}