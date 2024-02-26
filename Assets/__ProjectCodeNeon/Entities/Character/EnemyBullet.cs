using __ProjectCodeNeon.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int damage = 5;
    void OnTriggerEnter(Collider other)
    {
        CharacterGameController player = other.GetComponent<CharacterGameController>();

        if (player != null)
        {
            player.TakeDamage(damage);
        }

        if (!other.CompareTag("Enemy") && !other.CompareTag("EnemyBullet"))
        {
            Destroy(gameObject);
        }
    }
}
