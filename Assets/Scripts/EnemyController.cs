using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float detectionRadius = 5f;
    public float attackRange = 2f;
    public LayerMask playerLayer;
    public Animator enemyAnimator;
    public int damageValue = 10;

    private Transform player;
    private bool isPlayerInRange;
    private bool isPlayerInAttackRange;
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        CheckPlayerDistance();
        UpdateAnimations();
        MoveTowardsPlayer();
    }

    void CheckPlayerDistance()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        isPlayerInRange = distanceToPlayer <= detectionRadius;
        isPlayerInAttackRange = distanceToPlayer <= attackRange;

        if (isPlayerInAttackRange)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
             
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damageValue);
        }
    }
    void UpdateAnimations()
    {
        if (isPlayerInRange && !isPlayerInAttackRange)
        {
            enemyAnimator.SetBool("Run", true);
        }
        else
        {
            enemyAnimator.SetBool("Run", false);
        }

        if (isPlayerInAttackRange)
        {
            enemyAnimator.SetBool("Attack", true);
        }
        else
        {
            enemyAnimator.SetBool("Attack", false);
        }
    }

    void MoveTowardsPlayer()
    {
        if (isPlayerInRange)
        {
            if (!isPlayerInAttackRange)
            {
                navMeshAgent.SetDestination(player.position);
            }
            else
            {
                navMeshAgent.ResetPath();
            }
        }
    }
}