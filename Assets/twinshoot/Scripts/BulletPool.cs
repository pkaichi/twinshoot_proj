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
        Debug.Log($"{name}.Awake()");
        pool = new LinkedPool<GameObject>(

            () => GameObject.Instantiate(bulletPrefab),
            (obj) => { obj.SetActive(true); },
            (obj) => { obj.SetActive(false); },
            (obj) => GameObject.Destroy(obj),
            true,
            1024
        );
    }

    public void Initialize(GameObject prefab)
    {
        Debug.Log($"{name}.Initialize()");
        OnDestroy();
        bulletPrefab = prefab;
    }

    public GameObject GetBullet()
    {
        Debug.Log($"pool as {pool}");
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
