using __ProjectCodeNeon.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float rotationSpeed = 5f;
    public float detectionRange = 10f;
    private CharacterGameController player;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 2f;
    private float fireCooldown = 0f;
    public float bulletForce = 10f;

    private void Start()
    {
        player = FindObjectOfType<CharacterGameController>();
    }

    void Update()
    {
        if (IsTargetInDetectionRange())
        {
            RotateTurretTowardsTarget();

            if (CanFire())
            {
                Fire();
            }
        }
    }

    bool IsTargetInDetectionRange()
    {
        if (player.transform != null && Vector3.Distance(transform.position, player.transform.position) <= detectionRange)
        {
            return true;
        }

        return false;
    }

    void RotateTurretTowardsTarget()
    {
        Vector3 targetDirection = player.transform.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    bool CanFire()
    {
        return Time.time > fireCooldown;
    }

    void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        if (bulletRb != null)
        {
            bulletRb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
        }

        fireCooldown = Time.time + 1f / fireRate;
    }
}
