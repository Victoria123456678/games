using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;

    private Dictionary<GameObject, Queue<GameObject>> pools = new Dictionary<GameObject, Queue<GameObject>>();
    private Dictionary<GameObject, GameObject> prefabLookup = new Dictionary<GameObject, GameObject>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CreatePool(GameObject prefab, int initialSize = 20)
    {
        if (prefab == null || pools.ContainsKey(prefab)) return;

        Queue<GameObject> pool = new Queue<GameObject>();

        for (int i = 0; i < initialSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            obj.name = prefab.name + " (Pooled)";

            pool.Enqueue(obj);
            prefabLookup[obj] = prefab;
        }

        pools[prefab] = pool;
        Debug.Log($"Пул создан: {prefab.name} × {initialSize}");
    }

    public GameObject GetObject(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        if (prefab == null) return null;


        if (!pools.ContainsKey(prefab))
        {
            CreatePool(prefab, 10);
        }

        Queue<GameObject> pool = pools[prefab];

        GameObject obj;
        if (pool.Count > 0)
        {
            obj = pool.Dequeue();
        }
        else
        {
  
            obj = Instantiate(prefab);
            prefabLookup[obj] = prefab;       
            obj.name = prefab.name + " (Pooled)";
            Debug.LogWarning($"Пул для {prefab.name} расширен");
        }

        obj.transform.SetPositionAndRotation(position, rotation);
        obj.SetActive(true);

        if (obj.TryGetComponent<EnemyBase>(out var enemy))
            enemy.ResetState();

        return obj;
    }

    public void ReturnObject(GameObject obj)
    {
        if (obj == null) return;

        obj.SetActive(false);

        if (prefabLookup.TryGetValue(obj, out GameObject prefab) && pools.ContainsKey(prefab))
        {
            pools[prefab].Enqueue(obj);
            return;
        }

 
        Destroy(obj);
    }
}