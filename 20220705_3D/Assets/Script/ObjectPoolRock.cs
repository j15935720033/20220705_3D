using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public class ObjectPoolRock : MonoBehaviour
{
    [SerializeField, Header("碎片")]
    private GameObject prefabRock;
    [SerializeField, Header("碎片最大數量")]
    private int countMaxRock = 30;
    private int count;
    /// <summary>
    /// 碎片物件池
    /// </summary>
    private ObjectPool<GameObject> poolRock;

    private void Awake()
    {
        //實力化物件池=建構子(建立、拿物件、還物件、超出時處理、是否看輸出訊息、容量
        poolRock = new ObjectPool<GameObject>(
            CreatePool, GetRock, ReleaseRock, DestroyRock, false, 100
            );
    }
    /// <summary>
    /// 建立物件池要處理的行為
    /// </summary>
    /// <returns></returns>
    private GameObject CreatePool()
    {
        count++;
        GameObject temp = Instantiate(prefabRock);
        temp.name = prefabRock.name + " " + count;
        return temp;
    }
    /// <summary>
    /// 跟物件池拿物件
    /// </summary>
    /// <param name="ball"></param>
    private void GetRock(GameObject rock)
    {
        rock.SetActive(true);
    }
    /// <summary>
    /// 把物件還給物件池
    /// </summary>
    /// <param name="ball"></param>
    private void ReleaseRock(GameObject rock)
    {
        rock.SetActive(false);
    }
    /// <summary>
    /// 數量超出物件池容量要做的處理
    /// </summary>
    /// <param name="ball"></param>
    private void DestroyRock(GameObject rock)
    {
        Destroy(rock);
    }
    /// <summary>
    /// 取得物件池的物件
    /// </summary>
    public GameObject GetPoolObject()
    {
        return poolRock.Get();
    }
    public void ReleasePoolObject(GameObject rock)
    {
        poolRock.Release(rock);
    }
}
