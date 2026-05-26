using UnityEngine;

namespace Lessons.M5.Y2
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

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
