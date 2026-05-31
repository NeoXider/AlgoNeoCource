using UnityEngine;

namespace Lessons.M3.Y3
{
    public class ObstacleTrigger : MonoBehaviour
    {
        private void Start()
        {
            Destroy(gameObject, 10f);
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Столкновение с препятствием!");
        }
    }
}

