using UnityEngine;

namespace Lessons.M5.Y5
{
    public class EnemyUnit : MonoBehaviour
    {
        public int maxHealth = 3;
        public float moveSpeed = 2f;
        public GameManager gameManager;
        public int reward = 3;

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
                Die();
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

        private void Die()
        {
            if (gameManager != null)
            {
                gameManager.AddCurrency(reward);
            }

            Destroy(gameObject);
        }
    }
}
