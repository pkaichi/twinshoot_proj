using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShotControl : MonoBehaviour
{
    [SerializeField]
    GameObject bulletPrefab;

    uint prevInput;

    void Awake()
    {
        BulletPool.Instance.Initialize(bulletPrefab);
    }


    void Update()
    {
        var pad = Gamepad.current;

        var ps = prevInput;
        var ns = pad.PushState();
        var pull = (ns ^ ps) & ps;

        if ((pull & (uint)PadExtend.Assign.RB) != 0)
        {
            var bullet = BulletPool.Instance.GetBullet();
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;
        }


        prevInput = pad.PushState();
    }
}
