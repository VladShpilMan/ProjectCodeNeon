using __ProjectCodeNeon.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour
{

    public Animator animator;
    public CharacterGameController player;
    public float detectionRange = 10f;
    public float shootingDistance = 5f;
    public float shootingInterval = 1.5f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float health = 100f;

    protected NavMeshAgent navMeshAgent;
    protected float lastShootTime;

    protected virtual void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<CharacterGameController>();
        lastShootTime = -shootingInterval;
    }

    protected virtual void Update()
    {
        Chase();
        UpdateAnimations();
    }

    protected virtual void Chase()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < detectionRange)
        {
            navMeshAgent.SetDestination(player.transform.position);

            if (Vector3.Distance(transform.position, player.transform.position) < shootingDistance)
            {
                transform.LookAt(player.transform.position);

                if (Time.time - lastShootTime > shootingInterval)
                {
                    Shoot();
                    lastShootTime = Time.time;
                }
            }
        }
    }

    protected virtual void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            navMeshAgent.isStopped = true;
            Chase();
        }
    }

    protected virtual void UpdateAnimations()
    {
        if (navMeshAgent.velocity.magnitude > 0.1f)
        {
            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }

        if (Vector3.Distance(transform.position, player.transform.position) < shootingDistance)
        {
            animator.SetBool("IsAttacking", true);
        }
        else
        {
            animator.SetBool("IsAttacking", false);
        }
    }

    public virtual void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        if (animator != null)
        {
            animator.SetTrigger("Die");
        }

        navMeshAgent.enabled = false;
        Destroy(gameObject, 3f); 
    }


}
