using UnityEngine;

namespace Lessons.M6.Y4
{
    public class PlayerShipController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float minX = -7f;
        [SerializeField] private float maxX = 7f;
        [SerializeField] private float minY = -4f;
        [SerializeField] private float maxY = 4f;
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private Transform firePoint;
        [SerializeField] private GameManager gameManager;

        void Update()
        {
            if (gameManager != null && gameManager.IsGameOver)
            {
                return;
            }

            Move();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Fire();
            }
        }

        private void Move()
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");

            Vector3 position = transform.position;
            position.x += moveX * moveSpeed * Time.deltaTime;
            position.y += moveY * moveSpeed * Time.deltaTime;
            position.x = Mathf.Clamp(position.x, minX, maxX);
            position.y = Mathf.Clamp(position.y, minY, maxY);
            transform.position = position;
        }

        private void Fire()
        {
            Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        }
    }
}
