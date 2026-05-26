using UnityEngine;

namespace Lessons.M6.Y2
{
    public class AsteroidMover : MonoBehaviour
    {
        [SerializeField] private float speed = 3f;
        [SerializeField] private float destroyY = -6f;

        void Update()
        {
            transform.position += Vector3.down * speed * Time.deltaTime;

            if (transform.position.y < destroyY)
            {
                Destroy(gameObject);
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<ProjectileMover>() != null)
            {
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
        }
    }
}
