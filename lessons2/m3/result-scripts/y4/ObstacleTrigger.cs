using UnityEngine;

namespace Lessons.M3.Y4
{
    public class ObstacleTrigger : MonoBehaviour
    {
        private void Start()
        {
            Destroy(gameObject, 10f);
        }

        private void OnTriggerEnter(Collider other)
        {
            GameController gameController = FindFirstObjectByType<GameController>();
            gameController.GameOver();
        }
    }
}

