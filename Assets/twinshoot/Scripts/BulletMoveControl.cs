using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMoveControl : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 1f;

    bool actionActive = false;

    System.Action<BulletMoveControl> hitActionCallback;


    public Vector3 MoveVector
    {
        get;
        set;
    }

    public int hitCount = 0;

    Rigidbody rb;

    public System.Action<BulletMoveControl> HitActionCallback
    {
        set => hitActionCallback = value;
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

        hitCount = 0;
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
        // Debug.Log($"{name}.hitObj.{hitObj.gameObject.name} / {gameObject.tag}:{hitObj.gameObject.tag}");
        if (hitObj.gameObject.tag != gameObject.tag)
        {
            hitCount++;
            hitActionCallback?.Invoke(this);
        }
    }
}
