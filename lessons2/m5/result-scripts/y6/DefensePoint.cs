using UnityEngine;

namespace Lessons.M5.Y6
{
    public class DefensePoint : MonoBehaviour
    {
        public int health = 10;
        public GameManager gameManager;

        void Start()
        {
            gameManager.UpdateHealthText(health);
        }

        public void TakeDamage(int damage)
        {
            health -= damage;
            gameManager.UpdateHealthText(health);

            if (HasLost())
            {
                gameManager.GameOver();
            }
        }

        public bool HasLost()
        {
            return health <= 0;
        }
    }
}