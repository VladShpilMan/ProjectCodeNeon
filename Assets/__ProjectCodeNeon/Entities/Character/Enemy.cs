using __ProjectCodeNeon.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : EnemyBase
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void Die()
    {
        base.Die();
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }
}
