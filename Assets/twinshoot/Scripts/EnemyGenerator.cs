using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyGenerator : MonoBehaviour, IDisposable
{
    [SerializeField]
    int generateDelayCount = 10;

    int generateWait = 0;


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
        generateWait = generateDelayCount;
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
        if (--generateWait <= 0)
        {
            var obj = pool.Get();
            var objrb = obj.GetComponent<Rigidbody>();

            UnityEngine.Random.InitState(Time.renderedFrameCount);
            var xv = UnityEngine.Random.Range(0f, 1.5f) + 0.5f - 1.0f;
            var zv = UnityEngine.Random.Range(0f, 1.5f) + 0.5f - 1.0f;

            objrb?.AddRelativeForce(new Vector3(xv, 0f, zv) * 4, ForceMode.Impulse);
            generateWait = generateDelayCount;
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
