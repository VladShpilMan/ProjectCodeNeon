using __ProjectCodeNeon.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    //protected override void Start()
    //{
    //    base.Start();
    //}

    //protected override void Update()
    //{
    //    base.Update();
    //}

    //public override void Die()
    //{
    //    base.Die();
    //}

    //public override void TakeDamage(float damage)
    //{
    //    base.TakeDamage(damage);
    //}

    private NavMeshAgent navMeshAgent;
    private Transform player;
    private bool isChasing = false;

    public float chaseDistance = 10f;
    public float minMoveTime = 1f;
    public float maxMoveTime = 4f;
    public float rotationSpeed = 5f;
    public GameObject bulletPrefab;
    public string prefabPath = "Prefab/OrangeProjectile";
    public Transform firePoint;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<CharacterGameController>().transform;
        //bulletPrefab = Resources.Load<GameObject>(prefabPath);
        // ������ �������� ��� ���������� ��������
        StartCoroutine(MoveRandomly());
    }

    void Update()
    {
        // �������� ��������� �� ������
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // ���� ����� � �������� ���������, �������� �������������
        if (distanceToPlayer < chaseDistance)
        {
            isChasing = true;
            StopCoroutine(MoveRandomly());

            // ���������� ����� � ������
            navMeshAgent.SetDestination(player.position);

            // ������������ � ������� ������
            Vector3 direction = (player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

            // �������� ����� ��������
            Shoot();
        }
        else if (isChasing)
        {
            isChasing = false;
            StartCoroutine(MoveRandomly());
        }
    }

    IEnumerator MoveRandomly()
    {
        while (true)
        {
            if (!isChasing)
            {
                Vector3 randomDirection = Random.insideUnitSphere * 10f;
                randomDirection += transform.position;
                NavMeshHit hit;
                NavMesh.SamplePosition(randomDirection, out hit, 10f, NavMesh.AllAreas);

                navMeshAgent.SetDestination(hit.position);

                float moveTime = Random.Range(minMoveTime, maxMoveTime);
                yield return new WaitForSeconds(moveTime);
            }

            yield return null;
        }
    }

    void Shoot()
    {
            Debug.Log("outside");
        // ������ ������� �������� � �������������� ����
        if (firePoint != null && bulletPrefab != null)
        {
            Debug.Log("inside");
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            bulletRb.AddForce(firePoint.forward * 10, ForceMode.Impulse);
        }
    }
}
