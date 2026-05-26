using UnityEngine;

namespace Lessons.M5.Y6
{
    public class EnemySpawner : MonoBehaviour
    {
        public EnemyUnit enemyPrefab;
        public Transform bottomPoint;
        public Transform topPoint;
        public GameManager gameManager;
        public float spawnInterval = 2f;
        public int enemiesPerWave = 3;
        public float wavePause = 4f;

        private float nextSpawnTime;
        private int spawnedInWave;

        void Update()
        {
            if (Time.time < nextSpawnTime)
            {
                return;
            }

            if (spawnedInWave < enemiesPerWave)
            {
                SpawnEnemy();
                spawnedInWave++;
                nextSpawnTime = Time.time + spawnInterval;
            }
            else
            {
                enemiesPerWave++;
                spawnedInWave = 0;
                nextSpawnTime = Time.time + wavePause;
                Debug.Log("Новая волна. Врагов: " + enemiesPerWave);
            }
        }

        private void SpawnEnemy()
        {
            float y = Random.Range(bottomPoint.position.y, topPoint.position.y);
            Vector3 spawnPosition = new Vector3(transform.position.x, y, 0f);

            EnemyUnit enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            enemy.gameManager = gameManager;
        }
    }
}
