using System;
using UnityEngine;
using UnityEngine.InputSystem;

public static class PadExtend
{
    [Flags]
    public enum Assign
    {
        A = 0x01,
        B = 0x02,
        X = 0x04,
        Y = 0x08,
        LB = 0x10,
        RB = 0x20,
        L3 = 0x40,
        R3 = 0x80,
    };

    public static uint PushState(this Gamepad pad)
    {
        bool[] keyStates = {
            pad.aButton.isPressed,
            pad.bButton.isPressed,
            pad.xButton.isPressed,
            pad.yButton.isPressed,
            pad.leftShoulder.isPressed,
            pad.rightShoulder.isPressed,
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

}
