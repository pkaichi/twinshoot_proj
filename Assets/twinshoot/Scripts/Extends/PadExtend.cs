using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
public static class PadExtend
{
    [Flags]
    public enum Assign : uint
    {
        A = 0x01,
        B = 0x02,
        X = 0x04,
        Y = 0x08,
        LB = 0x10,
        RB = 0x20,
        LT = 0x40,
        RT = 0x80,
        L3 = 0x100,
        R3 = 0x200,

        // util
        ABXY = PadExtend.Assign.A | PadExtend.Assign.B | PadExtend.Assign.X | PadExtend.Assign.Y,

        LBRB = PadExtend.Assign.LB | PadExtend.Assign.RB,
        LTRT = PadExtend.Assign.LT | PadExtend.Assign.RT,

        LTLB = PadExtend.Assign.LT | PadExtend.Assign.LB,
        RTRB = PadExtend.Assign.RT | PadExtend.Assign.RB,

        LR = PadExtend.Assign.LTRT | PadExtend.Assign.LBRB,

        ABXYLBRB = PadExtend.Assign.ABXY | PadExtend.Assign.LBRB,
        ABXYLR = PadExtend.Assign.ABXY | PadExtend.Assign.LR,

    };

    public static readonly int AssignButtonNum = 10;

    public static uint PushState(this Gamepad pad)
    {
        // この並びはAssignの並びと揃ってないといけない
        bool[] keyStates = {
            pad.aButton.isPressed,
            pad.bButton.isPressed,
            pad.xButton.isPressed,
            pad.yButton.isPressed,
            pad.leftShoulder.isPressed,
            pad.rightShoulder.isPressed,
            pad.leftTrigger.isPressed,
            pad.rightTrigger.isPressed,
            pad.leftStick.IsPressed(),
            pad.rightStick.IsPressed()
        };

        uint state = 0;

        for (int i = 0; i < keyStates.Length; ++i)
        {
            state |= (Convert.ToUInt32(keyStates[i]) << i);
        }


        return state;
    }

    public static uint CalcPullState(this Gamepad pad, uint prevState)
    {
        var ns = pad.PushState();
        return (ns ^ prevState) & prevState;
    }

}
