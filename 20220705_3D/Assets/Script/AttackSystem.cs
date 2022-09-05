using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �����򩳨t��
/// </summary>
public class AttackSystem : MonoBehaviour
{
    [SerializeField, Header("�������")]
    private DataAttack dataAttack;

    protected bool canAttack = true;
    #region unity��k
    private void OnDrawGizmos()
    {
        Gizmos.color = dataAttack.attackAreaColor;
        Gizmos.matrix = Matrix4x4.TRS(
transform.position + transform.TransformDirection(dataAttack.attackAreaOffset),//�ഫ�@�ɮy��
            transform.rotation,
            transform.localScale
            );

        Gizmos.DrawCube(
            Vector3.zero,
            dataAttack.attackAreaSize
            //transform.position+dataAttack.attackAreaOffset,dataAttack.attackAreaSize
            );
    }

    private void Update()
    {

    }
    #endregion

    /// <summary>
    /// �}�l����
    /// </summary>
    public void StartAttack()
    {
        if (!canAttack) return;
        StartCoroutine(AttackFlow());
    }
    /// <summary>
    /// �����y�{
    /// </summary>
    /// <returns></returns>
    private IEnumerator AttackFlow()
    {
        canAttack = false;
        yield return new WaitForSeconds(dataAttack.delayAttack);
        CheckAttackArea();

        yield return new WaitForSeconds(dataAttack.waitAttackEnd);
        canAttack = true;
        stopAttack();
    }
    /// <summary>
    /// ��������F�O�@,���� �A���\�l���O�мg
    /// </summary>
    protected virtual void stopAttack()
    {

    }
    /// <summary>
    /// �ˬd�����ϰ�O�_�I����ؼйϼh
    /// </summary>
    private void CheckAttackArea()
    {
        Collider[] hits = Physics.OverlapBox(
            transform.position + transform.TransformDirection(dataAttack.attackAreaOffset),
            dataAttack.attackAreaSize / 2,
            transform.rotation,
            dataAttack.layerTarget);

        //���I��F��
        if (hits.Length > 0)
        {
            print(hits[0].name);
        }
    }
}
