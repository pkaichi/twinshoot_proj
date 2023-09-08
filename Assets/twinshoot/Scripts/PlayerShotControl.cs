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

    void Awake()
    {
        BulletPool.Instance.Initialize(bulletPrefab);
    }

    void Update()
    {
        var pe = PadPlus.Instance.Current;
        var pad = pe.pad;

        if (((pe.repeat | pe.pull) & (uint)PadExtend.Assign.RB) != 0)
        {
            var bullet = BulletPool.Instance.GetBullet();

            bullet.transform.position = shotRoot.position;
            bullet.transform.rotation = shotRoot.rotation;

            bullet.MoveVector = bullet.transform.forward;
        }

    }
}
