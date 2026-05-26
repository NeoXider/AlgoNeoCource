using UnityEngine;

namespace Lessons.M2.Y2
{
    public class ClickerGame : MonoBehaviour
    {
        public float score = 0;
        public int clickPower = 1;
        public int penaltyAmount = 5;

        private void Start()
        {
            Debug.Log("Счёт: " + score);
            Debug.Log("Сила клика: " + clickPower);
            Debug.Log("Штраф: " + penaltyAmount);
        }
    }
}

