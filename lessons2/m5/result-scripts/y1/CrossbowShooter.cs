using UnityEngine;

namespace Lessons.M5.Y1
{
    public class CrossbowShooter : MonoBehaviour
    {
        public GameObject boltPrefab;
        public Transform shootPoint;
        public float shootCooldown = 0.5f;

        private float nextShootTime;

        void Update()
        {
            if (Input.GetMouseButton(0) && Time.time >= nextShootTime)
            {
                Shoot();
                nextShootTime = Time.time + shootCooldown;
            }
        }

        public void Shoot()
        {
            Instantiate(boltPrefab, shootPoint.position, Quaternion.identity);
        }
    }
}
