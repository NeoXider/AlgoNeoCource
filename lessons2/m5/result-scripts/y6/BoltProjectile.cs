using UnityEngine;

namespace Lessons.M5.Y6
{
    public class BoltProjectile : MonoBehaviour
    {
        public float speed = 7f;
        public float lifeTime = 4f;
        public int damage = 1;

        void Start()
        {
            Destroy(gameObject, lifeTime);
        }

        void Update()
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            EnemyUnit enemy = other.GetComponent<EnemyUnit>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
