using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace chia
{

    /// <summary>
    /// 第三人稱控制器
    /// 移動與基本跳躍控制、動畫更新
    /// </summary>
    public class ThirdPersonController : MonoBehaviour
    {
        #region 資料
        [SerializeField, Header("移動速度"), Range(0, 50)]
        private float speed = 0.2f;
        [SerializeField, Header("旋轉速度"), Range(0, 50)]
        private float turn = 5f;
        [SerializeField, Header("跳躍速度"), Range(0, 50)]
        private float jump = 7f;

        private Animator ani;
        private CharacterController characterController;
        private Vector3 direction;
        private Transform transMainCamera;
        private string parRun = "Blend_浮點數";
        #endregion

        #region 事件
        private void Awake()
        {
            ani = GetComponent<Animator>();
            characterController = GetComponent<CharacterController>();
            //透過名稱搜尋物件，建議放在Awake或Stat或者執行一次的架構內
            transMainCamera = GameObject.Find("Main Camera").transform;
            //transCamera = GameObject.Find("Main Camera").GetComponent<Transform>();
        }
        private void Update()
        {
            Move();
            Jump();
        }
        #endregion

        #region 方法
        /// <summary>
        /// 移動
        /// </summary>
        private void Move()
        {
            float v = Input.GetAxisRaw("Vertical");//0、1、-1
            float h = Input.GetAxisRaw("Horizontal");//0、1、-1
            //print("<color=yellow>垂直軸向:"+v+"</color>");
            //*****************旋轉*********************//
            // transform.rotation = transMainCamera.rotation;//沒有過渡
            //玩家角度=四元數.插值(玩家角度,攝影機角度,速度*每幀時間)
            transform.rotation = Quaternion.Lerp(transform.rotation, transMainCamera.rotation, turn * Time.deltaTime);
            //euler Angles 0、45、90、180、360度
            //固定X與Z軸角度為0
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            //transform.rotation = new Quaternion(0, transform.rotation.y, 0,1);

            direction.z = v;
            direction.x = h;
            direction = transform.TransformDirection(direction);//將角色區域座標，轉成世界座標

            //角色控制器
            //Time.deltaTime(每幀時間)
            characterController.Move(direction * speed * Time.deltaTime);

            //動畫更新
            float vAxis = Input.GetAxis("Vertical");//垂直:-1~1
            float hAxis = Input.GetAxis("Horizontal");//水平:-1~1
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
        /// 跳躍
        /// </summary>
        private void Jump()
        {
            //如果在地面上 並且 按下空白建 就跳躍
            if (characterController.isGrounded && Input.GetKeyDown(KeyCode.Space))
            {
                direction.y = jump;
            }
            //地心引力 -9.81
            direction.y += Physics.gravity.y * Time.deltaTime;
        }
        #endregion
    }
}