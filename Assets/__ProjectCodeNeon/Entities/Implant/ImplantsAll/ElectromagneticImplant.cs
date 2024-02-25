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
        public float bulletForce = 10f;
        string prefabPath = "Prefab/BlueProjectile";

        private Vector3 Position = new Vector3(0f, 1.96f, 1.03f);
        private Quaternion Rotation = Quaternion.identity;

        public ElectromagneticImplant()
        {
            FirePoint = GameObject.Find("PlayerFirePoint");
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
            GameObject bullet = Instantiate(bulletPrefab, FirePoint.transform.position, FirePoint.transform.rotation);
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            bulletRb.AddForce(FirePoint.transform.forward * bulletForce, ForceMode.Impulse);
        }
    }
}
