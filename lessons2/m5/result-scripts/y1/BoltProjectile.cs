using UnityEngine;

namespace Lessons.M5.Y1
{
    public class BoltProjectile : MonoBehaviour
    {
        public float speed = 7f;
        public float lifeTime = 4f;

        void Start()
        {
            Destroy(gameObject, lifeTime);
        }

        void Update()
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }
}
