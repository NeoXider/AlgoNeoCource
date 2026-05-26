using UnityEngine;

namespace Lessons.M3.Y3
{
    public class ObstacleSpawner : MonoBehaviour
    {
        public GameObject obstaclePrefab;
        public float spawnInterval = 1.5f;

        private float timer;

        private void Update()
        {
            timer += Time.deltaTime;
            if (timer >= spawnInterval)
            {
                timer = 0f;
                Instantiate(obstaclePrefab, transform.position, Quaternion.identity);
            }
        }
    }
}

