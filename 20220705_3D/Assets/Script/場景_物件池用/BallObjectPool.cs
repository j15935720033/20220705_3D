using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �y��:���������
/// </summary>
public class BallObjectPool : MonoBehaviour
{
    public delegate void delegateHit(GameObject ball);
    public delegateHit onHit;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("Cube_�a�O"))
        {
            onHit(gameObject);
        }
    }
}
