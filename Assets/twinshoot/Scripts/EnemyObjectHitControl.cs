using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectHitControl : MonoBehaviour
{

    void OnCollisionEnter(Collision hitObj)
    {
        Debug.Log($"{name}.OnCollisionEnter({hitObj.gameObject.name})");
    }
}
