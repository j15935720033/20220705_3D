using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
/// <summary>
/// 生成球體使用物件池
/// </summary>
public class SpawnBallObjectPool : MonoBehaviour
{
    [SerializeField]
    private GameObject prefabBall;

    /// <summary>
    /// 球體物件池
    /// </summary>
    private ObjectPool<GameObject> poolBall;

    private void Awake()
    {
        //實力化物件池=建構子(建立、拿物件、還物件、超出時處理、是否看輸出訊息、容量
        poolBall = new ObjectPool<GameObject>(
            CreatePool, GetBall, ReleaseBall, DestroyBall, false, 100
            );
        InvokeRepeating("spawn", 0, 0.1f);
    }
    /// <summary>
    /// 建立物件池要處理的行為
    /// </summary>
    /// <returns></returns>
    private GameObject CreatePool()
    {
        return Instantiate(prefabBall);
    }
    /// <summary>
    /// 跟物件池拿物件
    /// </summary>
    /// <param name="ball"></param>
    private void GetBall(GameObject ball)
    {
        ball.SetActive(true);
    }
    /// <summary>
    /// 把物件還給物件池
    /// </summary>
    /// <param name="ball"></param>
    private void ReleaseBall(GameObject ball)
    {
        ball.SetActive(false);
    }
    /// <summary>
    /// 數量超出物件池容量要做的處理
    /// </summary>
    /// <param name="ball"></param>
    private void DestroyBall(GameObject ball)
    {
        Destroy(ball);
    }
    /// <summary>
    /// 生成球體
    /// </summary>
    private void spawn()
    {
        Vector3 pos;
        pos.x = Random.Range(56f, 83f);
        pos.y = Random.Range(5f, 7f);
        pos.z = Random.Range(105f, 125f);

        //跟物件池拿球體
        GameObject tempBall = poolBall.Get();
        tempBall.transform.position = pos;

        //球體碰撞時還給物件池
        tempBall.GetComponent<BallObjectPool>().onHit = BallHitAndRelease;
    }
    private void BallHitAndRelease(GameObject ball)
    {
        //把球還給物件池
        poolBall.Release(ball);
    }
}
