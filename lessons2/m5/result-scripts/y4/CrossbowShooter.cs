using UnityEngine;

namespace Lessons.M5.Y4
{
    public class CrossbowShooter : MonoBehaviour
    {
        public GameObject boltPrefab;
        public Transform shootPoint;
        public float shootCooldown = 0.5f;
        public int boltDamage = 1;

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
            GameObject boltObject = Instantiate(boltPrefab, shootPoint.position, Quaternion.identity);
            BoltProjectile bolt = boltObject.GetComponent<BoltProjectile>();

            if (bolt != null)
            {
                bolt.damage = boltDamage;
            }
        }
    }
}
