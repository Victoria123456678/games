using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour
{
    protected bool isDead = false;
    [Header("Здоровье")]
    public int maxHealth = 50;
    public int currentHealth;

    [Header("Атака")]
    public float attackDamage = 10f;
    public float attackCooldown = 1.5f;
    protected float lastAttackTime = 0f;

    protected float distanceToAttack = 4f;

    protected NavMeshAgent _agent;
    protected Animator _animator;
    protected bool _isAttacking = false;
    protected Transform player;

   
    public event System.Action<EnemyBase> OnDeath;

    protected virtual void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        currentHealth = maxHealth;

        if (_animator == null)
            _animator = GetComponentInChildren<Animator>();
    }

    protected virtual void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
    }

    protected virtual void Update()
    {
        if (_agent == null || player == null || !_agent.isOnNavMesh) 
            return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= distanceToAttack)
        {
            _agent.ResetPath();
            TryAttack();
        }
        else
        {
            _agent.SetDestination(player.position);
        }
    }

    protected virtual void TryAttack()
    {
        if (Time.time - lastAttackTime < attackCooldown) return;

        lastAttackTime = Time.time;
        Debug.Log(gameObject.name + " → пытается атаковать!");

        HitEnd();                  

        if (_animator != null)
            _animator.SetTrigger("Attack");
    }

    public virtual void HitEnd()
    {
        _isAttacking = false;

        if (_animator != null)
        {
            _animator.ResetTrigger("Attack");
            _animator.SetTrigger("Moving");
        }

        if (player == null) return;

        PlayerHealth ph = player.GetComponent<PlayerHealth>();
        if (ph != null)
        {
            ph.TakeDamage((int)attackDamage);
            Debug.Log($"✅ {gameObject.name} УДАРИЛ ИГРОКА на {attackDamage} урона!");
        }
    }

   public virtual void TakeDamage(int damage)
{
    if (isDead) return;                    

    currentHealth -= damage;
    if (currentHealth <= 0)
    {
        isDead = true;
        Die();
    }
}

    
protected virtual void Die()
{
    Debug.Log($"[DIE] {gameObject.name} умер. CurrentHealth = {currentHealth}");


    OnDeath?.Invoke(this);


    if (RewardManager.Instance != null)
    {
        Debug.Log($"[DIE] RewardManager найден. Пытаемся дропнуть артефакт...");
        RewardManager.Instance.DropReward(transform.position);
    }
    else
    {
        Debug.LogError($"[DIE] RewardManager.Instance == null! Дроп невозможен.");
    }

  
    if (PoolManager.Instance != null)
    {
        Debug.Log($"[DIE] Возвращаем {gameObject.name} в пул");
        PoolManager.Instance.ReturnObject(gameObject);
    }
    else
    {
        Debug.LogWarning($"[DIE] PoolManager не найден. Уничтожаем.");
        Destroy(gameObject);
    }
}
   
    public virtual void ResetState()
    {
        currentHealth = maxHealth;
        lastAttackTime = 0f;
        _isAttacking = false;

        if (_agent != null)
        {
            _agent.ResetPath();
            _agent.enabled = true;
        }

        gameObject.SetActive(true);
    }
}