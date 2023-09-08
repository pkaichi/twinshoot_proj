using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectControl : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField]
    GameObject visualizeObject;

    [SerializeField]
    float floatingRatio = 1f;
    [SerializeField, Range(0f, 5f)]
    float floatingSpeed = 1f;

    float floatingValue;
    Vector3 floatingPos;

    Coroutine resetRBCoroutine;

    System.Action<EnemyObjectControl> dieActionCallback;

    public System.Action<EnemyObjectControl> DieActionCallback
    {
        set => dieActionCallback = value;
    }


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        floatingValue = Mathf.Sin(Time.time * floatingSpeed) * floatingRatio;
        floatingPos = new Vector3(0f, floatingValue, 0f);
    }

    void FixedUpdate()
    {
        visualizeObject.transform.localPosition = floatingPos;
    }

    void OnCollisionEnter(Collision hitObject)
    {
        Debug.Log($"{gameObject.name}.OnCollisionEnter({hitObject.gameObject.name})");

        if (gameObject.layer != hitObject.gameObject.layer)
        {

            if (resetRBCoroutine != null)
            {
                StopCoroutine(resetRBCoroutine);
            }
            visualizeObject?.SetActive(false);
            resetRBCoroutine = StartCoroutine(ResetRBCoroutine());

        }
    }

    IEnumerator ResetRBCoroutine(float delay = 2.0f)
    {
        yield return new WaitForSeconds(delay);
        ResetRB();
        yield break;
    }

    void ResetRB()
    {
        dieActionCallback?.Invoke(this);
        visualizeObject?.SetActive(true);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.rotation = Quaternion.identity;
        //        rb.ResetInertiaTensor();
    }

}
