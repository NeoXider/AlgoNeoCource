using UnityEngine;

namespace Lessons.M5.Y2
{
    public class CrossbowShooter : MonoBehaviour
    {
        public GameObject boltPrefab;
        public Transform shootPoint;
        public float shootCooldown = 0.5f;

        private float nextShootTime;

        void Update()
        {
            AimAtMouse();

            if (Input.GetMouseButton(0) && Time.time >= nextShootTime)
            {
                Shoot();
                nextShootTime = Time.time + shootCooldown;
            }
        }

        private void AimAtMouse()
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 aimDirection = mousePosition - transform.position;
            float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }

        public void Shoot()
        {
            Instantiate(boltPrefab, shootPoint.position, transform.rotation);
        }
    }
}