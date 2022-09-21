using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ²yÅé
/// </summary>
public class Ball : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("Cube_¦aªO"))
        {
            Destroy(gameObject);
        }
    }
}
