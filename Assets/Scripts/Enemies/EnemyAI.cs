using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public GameObject player;
    private NavMeshAgent _agent;
    private Animator _animator;
    public bool _isAttacking;
    

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _agent.SetDestination(transform.position);
    }

    private void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < 4f)
        {
            Attack();
        }
        else
        {
            if (!_isAttacking)
                _agent.SetDestination(player.transform.position);
        }
    }

    public void Attack()
    {
        if (!_isAttacking)
        {
            _isAttacking = true;
            _animator.ResetTrigger("Moving");
            _animator.SetTrigger("Attack");
            _agent.SetDestination(transform.position);
        }
    }

    public void HitEnd()
    {
        Debug.Log("attack end");
        _isAttacking = false;
        _animator.ResetTrigger("Attack");
        _animator.SetTrigger("Moving");
        _agent.SetDestination(player.transform.position);
    }

    public void OnCollisionStay(Collision other)
    {
        // Debug.Log(other.gameObject.name);
        if (_isAttacking && other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hurt the Player");
        }
    }
}