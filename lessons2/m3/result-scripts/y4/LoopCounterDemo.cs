using UnityEngine;

namespace Lessons.M3.Y4
{
    public class LoopCounterDemo : MonoBehaviour
    {
        private void Start()
        {
            for (int i = 0; i < 5; i++)
            {
                Debug.Log("Спавн препятствия №" + i);
            }
        }
    }
}

