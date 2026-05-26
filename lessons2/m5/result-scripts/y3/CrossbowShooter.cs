using UnityEngine;

namespace Lessons.M5.Y3
{
    public class CrossbowShooter : MonoBehaviour
    {
        public GameObject boltPrefab;
        public Transform shootPoint;
        public float shootCooldown = 0.5f;

        private float nextShootTime;

        void Update()
        {
            if (!Input.GetMouseButton(0))
            {
                return;
            }

            if (!CanShoot())
            {
                return;
            }

            Shoot();
            nextShootTime = Time.time + shootCooldown;
        }

        public bool CanShoot()
        {
            return Time.time >= nextShootTime;
        }

        public void Shoot()
        {
            Instantiate(boltPrefab, shootPoint.position, Quaternion.identity);
        }
    }
}
