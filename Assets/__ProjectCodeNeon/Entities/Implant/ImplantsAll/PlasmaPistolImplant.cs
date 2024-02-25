using System.Collections;
using UnityEngine;

namespace __ProjectCodeNeon.Entities
{
    public class PlasmaPistolImplant : Implant
    {
        public GameObject bulletPrefab;
        public GameObject FirePoint;
        public float bulletForce = 60f;
        public float bulletLifetime = 3f; 
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

            if (FirePoint == null)
            {
                Debug.LogError("FirePoint not found in the scene.");
            }
        }

        public override void Action()
        {
            Debug.Log("shoot");
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