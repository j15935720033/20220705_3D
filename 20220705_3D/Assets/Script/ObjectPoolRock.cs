using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public class ObjectPoolRock : MonoBehaviour
{
    [SerializeField, Header("�H��")]
    private GameObject prefabRock;
    [SerializeField, Header("�H���̤j�ƶq")]
    private int countMaxRock = 30;
    private int count;
    /// <summary>
    /// �H�������
    /// </summary>
    private ObjectPool<GameObject> poolRock;

    private void Awake()
    {
        //��O�ƪ����=�غc�l(�إߡB������B�٪���B�W�X�ɳB�z�B�O�_�ݿ�X�T���B�e�q
        poolRock = new ObjectPool<GameObject>(
            CreatePool, GetRock, ReleaseRock, DestroyRock, false, 100
            );
    }
    /// <summary>
    /// �إߪ�����n�B�z���欰
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
    /// �򪫥��������
    /// </summary>
    /// <param name="ball"></param>
    private void GetRock(GameObject rock)
    {
        rock.SetActive(true);
    }
    /// <summary>
    /// �⪫���ٵ������
    /// </summary>
    /// <param name="ball"></param>
    private void ReleaseRock(GameObject rock)
    {
        rock.SetActive(false);
    }
    /// <summary>
    /// �ƶq�W�X������e�q�n�����B�z
    /// </summary>
    /// <param name="ball"></param>
    private void DestroyRock(GameObject rock)
    {
        Destroy(rock);
    }
    /// <summary>
    /// ���o�����������
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
