using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �y��
/// </summary>
public class Ball : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("Cube_�a�O"))
        {
            Destroy(gameObject);
        }
    }
}
