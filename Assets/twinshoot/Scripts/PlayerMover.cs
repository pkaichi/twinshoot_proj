using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField]
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var pe = PadPlus.Instance.Current;
        var pad = pe.pad;

        animator?.SetFloat("forward", pad.leftStick.up.value);
        var state = animator?.GetCurrentAnimatorStateInfo(0);

        if (pad.leftStick.up.value < 0.3f)
        {
            animator?.SetTrigger("idleTrigger");
        }
        else
        {
            animator?.SetTrigger("animationTrigger");
        }
        var b = animator.GetBool("animationTrigger");

        Debug.Log($"state: {state?.length},{state?.normalizedTime} : {b}");

        animator?.SetBool("runflag", (pe.push & (uint)PadExtend.Assign.LTLB) != 0);
    }
}
