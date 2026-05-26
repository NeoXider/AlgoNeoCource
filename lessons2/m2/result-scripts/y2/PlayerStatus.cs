using UnityEngine;

namespace Lessons.M2.Y2
{
    public class PlayerStatus : MonoBehaviour
    {
        public string playerName = "Hero";
        public int health = 100;
        public float speed = 5f;

        private void Start()
        {
            Debug.Log("Player: " + playerName);
            Debug.Log("Health: " + health);
            Debug.Log("Speed: " + speed);
        }
    }
}

