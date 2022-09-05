using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �������
/// </summary>
[CreateAssetMenu(menuName = "Chia/Data Attack", fileName = "DataAttack")]
public class DataAttack : ScriptableObject
{
    [Header("�����O"), Range(0, 1000)]
    public float attack;
    [Header("�����ϰ��C��]�w")]
    public Color attackAreaColor = new Color(1,0,0,0.5f);
    public Vector3 attackAreaSize = Vector3.one;
    public Vector3 attackAreaOffset;
    [Header("�����ؼйϼh")]
    public LayerMask layerTarget;
    [Header("��������ɶ�"), Range(0, 3)]
    public float delayAttack = 1.5f;

    [Header("�����ʵe��")]
    public AnimationClip animationAttack;
    /// <summary>
    /// ���ݧ�������:�ʵe�ɶ� - ��������
    /// </summary>
    public float waitAttackEnd => animationAttack.length - delayAttack;
}
