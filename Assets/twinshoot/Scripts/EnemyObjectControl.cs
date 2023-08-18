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



    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {

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

}
