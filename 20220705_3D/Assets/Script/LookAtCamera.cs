using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 面向攝影機
/// </summary>
public class LookAtCamera : MonoBehaviour
{
    private Transform mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main.transform;//抓取main Camera位置
    }


    // Update is called once per frame
    void Update()
    {
        LookAt();
    }
    /// <summary>
    /// 面相攝影機
    /// </summary>
    private void LookAt()
    {
        transform.LookAt(mainCamera);//此腳本放在Cavas上，所以這裡的transform是Canvas的
    }
}
