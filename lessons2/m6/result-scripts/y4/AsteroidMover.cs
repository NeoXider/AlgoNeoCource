using UnityEngine;

namespace Lessons.M6.Y4
{
    public class AsteroidMover : MonoBehaviour
    {
        [SerializeField] private float baseSpeed = 3f;
        [SerializeField] private float destroyY = -6f;
        [SerializeField] private int scoreValue = 10;
        [SerializeField] private GameObject explosionPrefab;

        public GameManager gameManager;

        void Update()
        {
            // После проигрыша астероид замирает
            if (gameManager != null && gameManager.IsGameOver)
            {
                return;
            }

            // Если менеджера нет, множитель обычный — 1
            float multiplier = gameManager != null ? gameManager.SpeedMultiplier : 1f;
            transform.position += Vector3.down * baseSpeed * multiplier * Time.deltaTime;

            if (transform.position.y < destroyY)
            {
                Destroy(gameObject);
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<ProjectileMover>() != null)
            {
                if (gameManager != null)
                {
                    gameManager.AddScore(scoreValue);
                }

                SpawnExplosion();
                Destroy(other.gameObject);
                Destroy(gameObject);
                return;
            }

            if (other.GetComponent<PlayerShipController>() != null)
            {
                if (gameManager != null)
                {
                    gameManager.FinishGame();
                }

                SpawnExplosion();
                Destroy(gameObject);
            }
        }

        // Создаёт эффект взрыва на месте астероида
        private void SpawnExplosion()
        {
            if (explosionPrefab != null)
            {
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            }
        }
    }
}
