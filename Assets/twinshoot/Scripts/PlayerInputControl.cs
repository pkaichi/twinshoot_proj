using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class PlayerInputControl : MonoBehaviour
{
    [SerializeField]
    Vector3 moveVector;
    [SerializeField, Range(0f, 90f)]
    float yAxisRot = 1.0f;

    [SerializeField, Range(0f, 0.5f)]
    float analogBlindRange = 0.2f;

    [SerializeField, Range(0f, 4f)]
    float moveSpeed = 1f;



    [SerializeField]
    GameObject bulletPrefab;

    [SerializeField]
    TextMeshProUGUI textArea;

    bool moveEnable = true;




    // Update is called once per frame
    void Update()
    {

        var pe = PadPlus.Instance.Current;
        var pad = pe.pad;

        moveEnable = true;
#if UNITY_EDITOR
        textArea.text = $"{pad.leftStick.value},{pad.leftStick.value.magnitude}";
#endif
        // leftstickで移動
        var lStickValue = pad.leftStick.value;
        float xmove = 0f;
        float zmove = 0f;
        if (analogBlindRange <= Mathf.Abs(lStickValue.x))
        {
            xmove = pad.leftStick.value.x;
        }
        if (analogBlindRange <= Mathf.Abs(lStickValue.y))
        {
            zmove = pad.leftStick.value.y;
        }

        moveVector = new Vector3(xmove, 0f, zmove) * moveSpeed;

        // rightstickで向き換え

        var rStickValue = pad.rightStick.value;
        if (analogBlindRange <= Mathf.Abs(rStickValue.x) || analogBlindRange <= Mathf.Abs(rStickValue.y))
        {
            var yAngle = Mathf.Atan2(rStickValue.x, rStickValue.y) * Mathf.Rad2Deg;

            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, yAngle, transform.localEulerAngles.z);
        }
    }

    void FixedUpdate()
    {
        if (moveEnable)
        {
            transform.localPosition += moveVector;
        }
    }

    void OnCollisionEnter(Collision hitObj)
    {
        Debug.Log($"{gameObject.name} : hitObj.{hitObj.gameObject.name}[({hitObj.gameObject.tag})]");
        if (hitObj.gameObject.tag.Equals("Field"))
        {
            transform.localPosition = Vector3.zero;
            moveEnable = false;
        }
        else
        {
            Debug.Log($"hit tag : ({hitObj.gameObject.tag})");
        }
    }
}
