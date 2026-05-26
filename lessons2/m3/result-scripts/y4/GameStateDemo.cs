using UnityEngine;

namespace Lessons.M3.Y4
{
    public class GameStateDemo : MonoBehaviour
    {
        public bool isGameOver = false;

        private void Start()
        {
            if (isGameOver)
            {
                Debug.Log("Экран Game Over");
            }
            else
            {
                Debug.Log("Играем");
            }
        }
    }
}

