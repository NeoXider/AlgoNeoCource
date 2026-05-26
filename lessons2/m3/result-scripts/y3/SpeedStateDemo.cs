using UnityEngine;

namespace Lessons.M3.Y3
{
    public class SpeedStateDemo : MonoBehaviour
    {
        public float speed = 6f;

        private void Start()
        {
            if (speed >= 8f)
            {
                Debug.Log("Скорость: высокая");
            }
            else if (speed >= 5f)
            {
                Debug.Log("Скорость: нормальная");
            }
            else
            {
                Debug.Log("Скорость: низкая");
            }
        }
    }
}

