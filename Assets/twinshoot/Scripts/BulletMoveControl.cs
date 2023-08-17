using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMoveControl : MonoBehaviour
{

    bool actionActive = false;

    public Vector3 MoveVector
    {
        get;
        set;
    }

    Rigidbody rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnEnable()
    {
        actionActive = true;
    }
    public void OnDisable()
    {
        actionActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!actionActive)
        {
            return;
        }
        if (rb != null)
        {
            rb.AddForce(MoveVector * 15, ForceMode.VelocityChange);
            actionActive = false;
        }
    }
}
