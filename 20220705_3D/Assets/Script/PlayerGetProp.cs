using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���a���o�D��
/// </summary>
public class PlayerGetProp : MonoBehaviour
{
    private ObjectPoolRock objectPoolRock;
    private string propRock = "���Y�H��";

    private void Awake()
    {
        objectPoolRock = FindObjectOfType<ObjectPoolRock>();
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
       
        if (hit.gameObject.name.Contains(propRock))
        {
            print("�I��" + hit.gameObject.name);
            objectPoolRock.ReleasePoolObject(hit.gameObject);
        }
    }
}
