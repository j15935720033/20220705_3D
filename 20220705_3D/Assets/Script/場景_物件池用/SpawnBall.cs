using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 生成球體，不使用物件池
/// </summary>
public class SpawnBall : MonoBehaviour
{
    [SerializeField]
    private GameObject prefabBall;
    private void Awake()
    {
        InvokeRepeating("spawn",0,0.1f);
    }
    /// <summary>
    /// 生成
    /// </summary>
    private void spawn()
    {
        Vector3 pos;
        pos.x = Random.Range(56f,83f);
        pos.y = Random.Range(5f, 7f);
        pos.z = Random.Range(105f, 125f);

        Instantiate(prefabBall,pos,Quaternion.identity);
    }
}
