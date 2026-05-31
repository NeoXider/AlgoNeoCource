using UnityEngine;

namespace Lessons.M3.Y4
{
    public class ObstacleSpawner : MonoBehaviour
    {
        public GameObject obstaclePrefab;
        public float spawnInterval = 1.5f;

        private float timer;
        private GameController gameController;

        private void Start()
        {
            gameController = FindFirstObjectByType<GameController>();
        }

        private void Update()
        {
            if (gameController.isGameOver)
            {
                return;
            }

            timer += Time.deltaTime;
            if (timer >= spawnInterval)
            {
                timer = 0f;
                Instantiate(obstaclePrefab, transform.position, Quaternion.identity);
            }
        }
    }
}

