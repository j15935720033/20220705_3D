using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
/// <summary>
/// �ͦ��y��ϥΪ����
/// </summary>
public class SpawnBallObjectPool : MonoBehaviour
{
    [SerializeField]
    private GameObject prefabBall;

    /// <summary>
    /// �y�骫���
    /// </summary>
    private ObjectPool<GameObject> poolBall;

    private void Awake()
    {
        //��O�ƪ����=�غc�l(�إߡB������B�٪���B�W�X�ɳB�z�B�O�_�ݿ�X�T���B�e�q
        poolBall = new ObjectPool<GameObject>(
            CreatePool, GetBall, ReleaseBall, DestroyBall, false, 100
            );
        InvokeRepeating("spawn", 0, 0.1f);
    }
    /// <summary>
    /// �إߪ�����n�B�z���欰
    /// </summary>
    /// <returns></returns>
    private GameObject CreatePool()
    {
        return Instantiate(prefabBall);
    }
    /// <summary>
    /// �򪫥��������
    /// </summary>
    /// <param name="ball"></param>
    private void GetBall(GameObject ball)
    {
        ball.SetActive(true);
    }
    /// <summary>
    /// �⪫���ٵ������
    /// </summary>
    /// <param name="ball"></param>
    private void ReleaseBall(GameObject ball)
    {
        ball.SetActive(false);
    }
    /// <summary>
    /// �ƶq�W�X������e�q�n�����B�z
    /// </summary>
    /// <param name="ball"></param>
    private void DestroyBall(GameObject ball)
    {
        Destroy(ball);
    }
    /// <summary>
    /// �ͦ��y��
    /// </summary>
    private void spawn()
    {
        Vector3 pos;
        pos.x = Random.Range(56f, 83f);
        pos.y = Random.Range(5f, 7f);
        pos.z = Random.Range(105f, 125f);

        //�򪫥�����y��
        GameObject tempBall = poolBall.Get();
        tempBall.transform.position = pos;

        //�y��I�����ٵ������
        tempBall.GetComponent<BallObjectPool>().onHit = BallHitAndRelease;
    }
    private void BallHitAndRelease(GameObject ball)
    {
        //��y�ٵ������
        poolBall.Release(ball);
    }
}
