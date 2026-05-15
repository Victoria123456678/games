using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;

    [Header("Враги")]
    public GameObject[] enemyPrefabs;

    [Header("Точки спавна")]
    public Transform[] spawnPoints;

    [Header("Настройки")]
    public float spawnInterval = 3f;
    public int maxEnemiesAlive = 12;

    private int enemiesAlive = 0;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (spawnPoints.Length == 0)
            Debug.LogError("Нет точек спавна!");

        InvokeRepeating("TrySpawnEnemy", 2f, spawnInterval);
    }

    void TrySpawnEnemy()
    {
        if (enemiesAlive >= maxEnemiesAlive) return;
        if (enemyPrefabs.Length == 0 || spawnPoints.Length == 0) return;

        GameObject prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        GameObject enemy = PoolManager.Instance.GetObject(prefab, spawnPoint.position, Quaternion.identity);

        if (enemy != null)
        {
            // Подписываемся на смерть
            EnemyBase enemyBase = enemy.GetComponent<EnemyBase>();
            if (enemyBase != null)
            {
                enemyBase.OnDeath += HandleEnemyDeath;   // ← Важно!
            }

            enemiesAlive++;
        }
    }

   
    private void HandleEnemyDeath(EnemyBase enemy)
    {
        if (enemy != null)
        {
            enemy.OnDeath -= HandleEnemyDeath;   // отписываемся
        }
        
        EnemyDied();
    }

    public void EnemyDied()
    {
        enemiesAlive = Mathf.Max(0, enemiesAlive - 1);
    }
}