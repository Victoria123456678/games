using UnityEngine;
using UnityEngine.AI;

public class WizardRanged : EnemyBase
{
    private void Start()
    {
        base.Start();
        distanceToAttack = 7f;
        
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    public void HitEnd()
    {
        Debug.Log("attack end");
        _isAttacking = false;

        if (_animator != null)
        {
            _animator.ResetTrigger("Attack");
            _animator.SetTrigger("Moving");
        }

        if (_agent != null && player != null)
        {
            _agent.SetDestination(player.position);
        }
    }
}