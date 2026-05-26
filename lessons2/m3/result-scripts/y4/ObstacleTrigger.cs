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
            if (other.GetComponent<DinoController>() == null)
            {
                return;
            }

            GameController gameController = FindFirstObjectByType<GameController>();
            if (gameController != null)
            {
                gameController.GameOver();
            }
        }
    }
}

