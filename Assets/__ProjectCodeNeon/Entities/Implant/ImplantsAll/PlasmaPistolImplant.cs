﻿using UnityEngine;

namespace __ProjectCodeNeon.Entities
{
    public class PlasmaPistolImplant : Implant
    {
        public GameObject bulletPrefab;
        public GameObject FirePoint;
        public float bulletForce = 60f;
        string prefabPath = "Prefab/GreenProjectile";

        private Vector3 Position = new Vector3(0f, 1.96f, 1.03f);
        private Quaternion Rotation = Quaternion.identity;

        public PlasmaPistolImplant()
        {
            FirePoint = GameObject.Find("FirePoint");
            bulletPrefab = Resources.Load<GameObject>(prefabPath);

            if (bulletPrefab == null)
            {
                Debug.LogError("Failed to load bullet prefab from Resources folder.");
            }

            if (bulletPrefab == null)
            {
                Debug.LogError("Failed to load bullet prefab from Resources folder.");
            }
        }

        public override void Action()
        {
            ShootBullet(FirePoint.transform.forward);
        }

        private void ShootBullet(Vector3 direction)
        {
            GameObject bullet = Instantiate(bulletPrefab, FirePoint.transform.position, FirePoint.transform.rotation);
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            bulletRb.AddForce(direction * bulletForce, ForceMode.Impulse);
        }
    }
}