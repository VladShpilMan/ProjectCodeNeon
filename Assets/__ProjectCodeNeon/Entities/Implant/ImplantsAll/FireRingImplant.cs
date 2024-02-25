using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace __ProjectCodeNeon.Entities
{
    public class FireRingImplant : Implant
    {
        public GameObject bulletPrefab;
        public GameObject FirePoint;
        public float bulletForce = 40f;
        string prefabPath = "Prefab/OrangeProjectile";

        private Vector3 Position = new Vector3(0f, 1.96f, 1.03f);
        private Quaternion Rotation = Quaternion.identity;

        public FireRingImplant()
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
            for (int i = 0; i < 8; i++)
            {
                float angle = i * 45f;
                Vector3 direction = Quaternion.Euler(0, angle, 0) * FirePoint.transform.forward;
                ShootBullet(direction);
            }
        }

        private void ShootBullet(Vector3 direction)
        {
            GameObject bullet = Instantiate(bulletPrefab, FirePoint.transform.position, FirePoint.transform.rotation);
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            bulletRb.AddForce(direction * bulletForce, ForceMode.Impulse);

            bullet.transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}