using UnityEngine;

public class PoolInitializer : MonoBehaviour
{
    [Header("Префабы врагов для пула")]
    public GameObject[] enemyPrefabs;

    [Header("Размер пула для каждого")]
    public int poolSize = 20;

    void Start()
    {
        if (PoolManager.Instance == null)
        {
            Debug.LogError("PoolManager не найден в сцене!");
            return;
        }

        Debug.Log("=== Инициализация пулов ===");

        foreach (GameObject prefab in enemyPrefabs)
        {
            if (prefab != null)
            {
                PoolManager.Instance.CreatePool(prefab, poolSize);
            }
            else
            {
                Debug.LogWarning("В PoolInitializer есть пустой слот в enemyPrefabs!");
            }
        }

        Debug.Log("Пулы успешно инициализированы!");
    }
}