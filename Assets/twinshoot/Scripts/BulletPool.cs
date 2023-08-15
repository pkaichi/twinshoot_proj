using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletPool : GameObjectSingleton<BulletPool>
{
    GameObject bulletPrefab;
    LinkedPool<GameObject> pool;

    void Awake()
    {
        pool = new LinkedPool<GameObject>(

            () => GameObject.Instantiate(bulletPrefab) as GameObject,
            (obj) => { },
            (obj) => { },
            (obj) => GameObject.Destroy(obj),
            true,
            1024
        );
    }

    public void Initialize(GameObject prefab)
    {
        OnDestroy();
        bulletPrefab = prefab;
    }

    public GameObject GetBullet()
    {
        return pool.Get();
    }

    void OnDestroy()
    {
        if (pool != null)
        {
            pool.Dispose();
        }
        pool = null;
    }
}
