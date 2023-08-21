using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyGenerator : MonoBehaviour, IDisposable
{
    [SerializeField]
    EnemyObjectControl enemyPrefab;

    LinkedPool<EnemyObjectControl> pool;
    private bool disposedValue;

    public void Dispose()
    {
        pool?.Dispose();
        pool = null;
    }

    void Awake()
    {
        Initialize();
    }

    void Initialize()
    {
        pool = new LinkedPool<EnemyObjectControl>(
            () => GenerateEnemyInstance(enemyPrefab),
            (obj) => obj.gameObject.SetActive(true),
            (obj) => obj.gameObject.SetActive(false),
            (obj) => GameObject.Destroy(obj.gameObject),
            true,
            32
        );
    }

    void Update()
    {
        var enableCnt = 0;
        foreach (Transform c in transform)
        {
            if (c.gameObject.activeInHierarchy)
            {
                enableCnt++;
            }
        }
        if (enableCnt <= 0)
        {
            pool.Get();
        }
    }

    EnemyObjectControl GenerateEnemyInstance(EnemyObjectControl prefab)
    {
        var e = GameObject.Instantiate<EnemyObjectControl>(prefab);
        e.transform.SetParent(transform);
        e.transform.localPosition = new Vector3(0f, e.transform.position.y, 0f);

        e.DieActionCallback = (obj) =>
        {
            // おそらくe == objの筈
            pool.Release(obj);
        };

        return e;
    }

    public void DestroyEnemy(EnemyObjectControl pooledObj)
    {
        pool.Release(pooledObj);
    }

    public EnemyObjectControl CreateEnemy()
    {
        var enemy = pool.Get();

        return enemy;
    }



}
