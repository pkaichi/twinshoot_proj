using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShotControl : MonoBehaviour
{
    [SerializeField]
    Transform shotRoot;
    [SerializeField]
    BulletMoveControl bulletPrefab;

    uint prevInput;

    void Awake()
    {
        BulletPool.Instance.Initialize(bulletPrefab);
    }


    void Update()
    {
        var pad = Gamepad.current;

        var pull = pad.CalcPullState(prevInput);
        //        var push = pad.PushState();

        if ((pull & (uint)PadExtend.Assign.RB) != 0)
        {
            var bullet = BulletPool.Instance.GetBullet();

            bullet.transform.position = shotRoot.position;
            bullet.transform.rotation = shotRoot.rotation;

            bullet.MoveVector = bullet.transform.forward;
        }


        prevInput = pad.PushState();
    }
}
