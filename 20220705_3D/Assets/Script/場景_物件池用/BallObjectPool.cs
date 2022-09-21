using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 球體:物件池版本
/// </summary>
public class BallObjectPool : MonoBehaviour
{
    public delegate void delegateHit(GameObject ball);
    public delegateHit onHit;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("Cube_地板"))
        {
            onHit(gameObject);
        }
    }
}
