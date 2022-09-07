using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���V��v��
/// </summary>
public class LookAtCamera : MonoBehaviour
{
    private Transform mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main.transform;//���main Camera��m
    }


    // Update is called once per frame
    void Update()
    {
        LookAt();
    }
    /// <summary>
    /// ������v��
    /// </summary>
    private void LookAt()
    {
        transform.LookAt(mainCamera);//���}����bCavas�W�A�ҥH�o�̪�transform�OCanvas��
    }
}
