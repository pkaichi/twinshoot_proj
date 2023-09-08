using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;


[DefaultExecutionOrder(-100)]
public class PadPlus : GameObjectSingleton<PadPlus>
{
    public class PadExtendInfo
    {
        public Gamepad pad;
        public int[] repeatCounterList;

        public uint push;
        public uint prev;
        public uint repeat;
        public uint pull;
    }

    ReadOnlyArray<Gamepad> padlist = default;
    Dictionary<int, PadExtendInfo> padExtendInfoList = new Dictionary<int, PadExtendInfo>();


    [SerializeField]
    int repeatWait = 30;
    [SerializeField]
    int repeatFrame = 1;


    public PadExtendInfo Current
    {
        get => padExtendInfoList[Gamepad.current.deviceId];
    }


    void Awake()
    {
        Debug.Log($"{name}.Awake()");
        Initialize();
    }

    public void Initialize()
    {
        PadStateInitialize();
    }

    public void PadStateInitialize()
    {
        padExtendInfoList.Clear();
        padlist = Gamepad.all;
        Debug.Log($"padList : {padlist.Count}");
        foreach (var p in padlist)
        {
            var pe = new PadExtendInfo
            {
                pad = p,
                repeatCounterList = new int[PadExtend.AssignButtonNum]
            };
            ResetRepeatCount(pe);

            padExtendInfoList.Add(p.deviceId, pe);
        }
    }

    public void UpdatePadState()
    {
        foreach (var pe in padExtendInfoList.Values)
        {
            UpdatePadextendState(pe);
            Debug.Log($"pad[{pe.pad.deviceId}]: {pe.push:x8} / {pe.pull:x8} / {pe.repeat:x8}");
        }
    }

    void ResetRepeatCount(PadExtendInfo pe)
    {
        for (int i = 0; i < pe.repeatCounterList.Length; ++i)
        {
            pe.repeatCounterList[i] = repeatWait;
        }
    }

    void UpdatePadextendState(PadExtendInfo pe)
    {
        var push = pe.pad.PushState();
        var pull = pe.pad.CalcPullState(pe.prev);
        uint repeatButton = 0;
        PadExtend.Assign[] buttonTbl = new PadExtend.Assign[10] {
            PadExtend.Assign.A,
            PadExtend.Assign.B,
            PadExtend.Assign.X,
            PadExtend.Assign.Y,
            PadExtend.Assign.LB,
            PadExtend.Assign.RB,
            PadExtend.Assign.LT,
            PadExtend.Assign.RT,
            PadExtend.Assign.L3,
            PadExtend.Assign.R3,
        };

        for (int i = 0; i < buttonTbl.Length; ++i)
        {
            uint btn = (uint)buttonTbl[i];
            if ((push & btn) != 0)
            {
                pe.repeatCounterList[i]--;
                if (pe.repeatCounterList[i] < 0)
                {
                    repeatButton |= btn;
                    pe.repeatCounterList[i] = repeatFrame;
                }

            }
            else
            {
                // 離されたのでカウンタはリセット
                pe.repeatCounterList[i] = repeatWait;
            }
        }

        Debug.Log($"push:{push:x8} / pull:{pull:x8} / rep:{repeatButton:x8}");
        pe.push = push;
        pe.repeat = repeatButton;
        pe.pull = pull;

    }

    void Update()
    {
        UpdatePadState();
    }

    void LateUpdate()
    {
        if (padlist.Count != Gamepad.all.Count)
        {
            PadStateInitialize();
        }

        var peList = padExtendInfoList.Values.ToList();
        for (int i = 0; i < padExtendInfoList.Count; ++i)
        {
            var pe = peList[i];
            pe.prev = pe.pad.PushState();
        }


    }
}
