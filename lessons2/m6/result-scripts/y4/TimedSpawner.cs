using UnityEngine;

namespace Lessons.M6.Y4
{
    public class TimedSpawner : MonoBehaviour
    {
        [Header("Астероиды")]
        [SerializeField] private AsteroidMover asteroidPrefab;
        [SerializeField] private GameManager gameManager;
        [SerializeField] private float minX = -7f;
        [SerializeField] private float maxX = 7f;
        [SerializeField] private float spawnY = 6f;
        [SerializeField] private float asteroidStartDelay = 1f;
        [SerializeField] private float asteroidRate = 1.5f;

        void Start()
        {
            InvokeRepeating(nameof(SpawnAsteroid), asteroidStartDelay, asteroidRate);
        }

        private void SpawnAsteroid()
        {
            if (gameManager != null && gameManager.IsGameOver)
            {
                return;
            }

            float randomX = Random.Range(minX, maxX);
            Vector3 position = new Vector3(randomX, spawnY, 0f);
            AsteroidMover asteroid = Instantiate(asteroidPrefab, position, Quaternion.identity);
            asteroid.gameManager = gameManager;
        }
    }
}
