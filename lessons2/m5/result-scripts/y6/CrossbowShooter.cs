using UnityEngine;

namespace Lessons.M5.Y6
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
            AimAtMouse();

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

        private void AimAtMouse()
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 aimDirection = mousePosition - transform.position;
            float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }

        public bool CanShoot()
        {
            return Time.time >= nextShootTime;
        }

        public void Shoot()
        {
            GameObject boltObject = Instantiate(boltPrefab, shootPoint.position, transform.rotation);
            BoltProjectile bolt = boltObject.GetComponent<BoltProjectile>();

            if (bolt != null)
            {
                bolt.damage = boltDamage;
            }
        }
    }
}