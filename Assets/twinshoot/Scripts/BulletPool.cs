using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletPool : GameObjectSingleton<BulletPool>
{
    BulletMoveControl bulletPrefab;
    LinkedPool<BulletMoveControl> pool;

    void Awake()
    {
        pool = new LinkedPool<BulletMoveControl>(

            () => GameObject.Instantiate<BulletMoveControl>(bulletPrefab),
            (obj) =>
            {
                obj.gameObject.SetActive(true);
            },
            (obj) => { obj.gameObject.SetActive(false); },
            (obj) => GameObject.Destroy(obj),
            true,
            1024
        );
    }

    public void Initialize(BulletMoveControl prefab)
    {
        bulletPrefab = prefab;
    }

    public BulletMoveControl GetBullet()
    {
        var obj = pool.Get();

        obj.HitActionCallback = (b) =>
        {
            b.hitCount--;

            if (b.hitCount <= 0)
            {
                pool.Release(b);
            }

        };
        return obj;
    }

    void OnDestroy()
    {
        pool?.Dispose();
        pool = null;
    }
}
