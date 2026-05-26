using UnityEngine;

namespace Lessons.M6.Y3
{
    public class AsteroidMover : MonoBehaviour
    {
        [SerializeField] private float baseSpeed = 3f;
        [SerializeField] private float destroyY = -6f;
        [SerializeField] private int scoreValue = 10;

        public GameManager gameManager;

        void Update()
        {
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

                Destroy(gameObject);
            }
        }
    }
}
