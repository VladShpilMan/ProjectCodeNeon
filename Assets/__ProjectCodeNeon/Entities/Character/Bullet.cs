using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 10;
    void OnTriggerEnter(Collider other)
    {
        Enemy enemyHealth = other.GetComponent<Enemy>();

        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage);
        }

        if (!other.CompareTag("Player") && !other.CompareTag("PlayerBullet"))
        {
            Destroy(gameObject);
        }
    }
}
