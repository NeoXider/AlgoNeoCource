using UnityEngine;

namespace Lessons.M6.Y4
{
    public class ScrollingBackground : MonoBehaviour
    {
        [SerializeField] private float speed = 0.5f;
        [SerializeField] private float resetY = -10f;
        [SerializeField] private float startY = 10f;

        void Update()
        {
            // Медленно тянем фон вниз
            transform.position += Vector3.down * speed * Time.deltaTime;

            if (transform.position.y < resetY)
            {
                // Ушёл слишком низко — возвращаем наверх
                Vector3 position = transform.position;
                position.y = startY;
                transform.position = position;
            }
        }
    }
}
