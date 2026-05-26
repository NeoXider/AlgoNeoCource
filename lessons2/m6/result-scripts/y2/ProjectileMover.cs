using UnityEngine;

namespace Lessons.M6.Y2
{
    public class ProjectileMover : MonoBehaviour
    {
        [SerializeField] private float speed = 10f;
        [SerializeField] private float lifeTime = 2f;

        void Start()
        {
            Destroy(gameObject, lifeTime);
        }

        void Update()
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
    }
}
