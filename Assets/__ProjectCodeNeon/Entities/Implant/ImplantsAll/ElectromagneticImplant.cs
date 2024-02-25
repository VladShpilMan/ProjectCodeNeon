using log4net.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace __ProjectCodeNeon.Entities { 
    public class ElectromagneticImplant : Implant
    {
        public GameObject bulletPrefab;
        public GameObject FirePoint;
        public float bulletForce = 50f;
        string prefabPath = "Prefab/BlueProjectile";

        private Vector3 Position = new Vector3(0f, 1.96f, 1.03f);
        private Quaternion Rotation = Quaternion.identity;

        public ElectromagneticImplant()
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
            ShootBullet(FirePoint.transform.forward, 0);
            
            Vector3 rightOffset = Quaternion.Euler(0, 15, 0) * FirePoint.transform.forward;
            ShootBullet(rightOffset, 15);
            
            Vector3 leftOffset = Quaternion.Euler(0, -15, 0) * FirePoint.transform.forward;
            ShootBullet(leftOffset, -15);
        }

        private void ShootBullet(Vector3 direction, float angle)
        {
            GameObject bullet = Instantiate(bulletPrefab, FirePoint.transform.position, FirePoint.transform.rotation);
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            bulletRb.AddForce(direction * bulletForce, ForceMode.Impulse);

            bullet.transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
