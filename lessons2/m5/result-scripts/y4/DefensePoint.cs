using UnityEngine;

namespace Lessons.M5.Y4
{
    public class DefensePoint : MonoBehaviour
    {
        public int health = 10;

        public void TakeDamage(int damage)
        {
            health -= damage;

            if (HasLost())
            {
                Debug.Log("База разрушена!");
            }
        }

        public bool HasLost()
        {
            return health <= 0;
        }
    }
}
