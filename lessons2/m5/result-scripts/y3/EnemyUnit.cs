using UnityEngine;

namespace Lessons.M5.Y3
{
    public class EnemyUnit : MonoBehaviour
    {
        public int maxHealth = 3;
        public float moveSpeed = 2f;

        private int currentHealth;

        void Start()
        {
            currentHealth = maxHealth;
        }

        void Update()
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }

        public bool IsAlive()
        {
            return currentHealth > 0;
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;

            if (!IsAlive())
            {
                Destroy(gameObject);
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            DefensePoint defensePoint = other.GetComponent<DefensePoint>();

            if (defensePoint == null)
            {
                return;
            }

            defensePoint.TakeDamage(1);
            Destroy(gameObject);
        }
    }
}
