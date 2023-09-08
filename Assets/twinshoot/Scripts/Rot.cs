using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rot : MonoBehaviour
{
    [SerializeField]
    Vector3 rotate;

    void Update()
    {
        transform.Rotate(rotate);
    }
}
