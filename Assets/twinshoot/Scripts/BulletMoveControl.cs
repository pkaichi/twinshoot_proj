using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMoveControl : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 1f;

    bool actionActive = false;

    System.Action<BulletMoveControl> dieActionCallback;


    public Vector3 MoveVector
    {
        get;
        set;
    }

    Rigidbody rb;

    public System.Action<BulletMoveControl> DieActionCallback
    {
        set => dieActionCallback = value;
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnEnable()
    {
        actionActive = true;
        rb.velocity = Vector3.zero;
        rb.ResetInertiaTensor();
        transform.rotation = Quaternion.identity;
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
            rb.AddForce(MoveVector * moveSpeed, ForceMode.VelocityChange);
            actionActive = false;
        }
    }

    void OnCollisionEnter(Collision hitObj)
    {
        Debug.Log($"{name}.hitObj.{hitObj.gameObject.name}");
        if (hitObj.gameObject.tag != gameObject.tag)
        {
            dieActionCallback?.Invoke(this);
        }
    }
}
