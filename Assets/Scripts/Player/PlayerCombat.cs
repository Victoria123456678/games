using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Настройки атаки")]
    public float attackDamage = 25f;
    public float attackRange = 5f;
    public float attackCooldown = 0.6f;

    private float lastAttackTime = 0f;
    private Camera playerCamera;

    void Start()
    {
        playerCamera = Camera.main;
    }

    void Update()
    {
        // Атака по клавише F
        if (Input.GetKeyDown(KeyCode.F) && Time.time - lastAttackTime >= attackCooldown)
        {
            lastAttackTime = Time.time;
            Attack();
        }
    }

    void Attack()
{
    Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
    RaycastHit hit;


    if (Physics.Raycast(ray, out hit, attackRange))
    {
        Debug.Log("Raycast попал в: " + hit.collider.gameObject.name);

        EnemyBase enemy = hit.collider.GetComponent<EnemyBase>();
        if (enemy != null)
        {
            enemy.TakeDamage((int)attackDamage);
            Debug.Log("✅ УСПЕШНО ПОПАЛ ПО " + hit.collider.gameObject.name);
        }
        else
        {
            
            EnemyBase enemyParent = hit.collider.GetComponentInParent<EnemyBase>();
            if (enemyParent != null)
            {
                enemyParent.TakeDamage((int)attackDamage);
                Debug.Log("✅ Попал через GetComponentInParent!");
            }
        }
    }
    else
    {
        Debug.Log("Raycast никуда не попал");
    }
}
}