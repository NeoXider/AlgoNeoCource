using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Lessons.M2.Y3
{
    public class ClickerGame : MonoBehaviour, IPointerDownHandler
    {
        public float score = 0;
        public int clickPower = 1;
        public int penaltyAmount = 5;
        public TMP_Text textScore;

        private void Start()
        {
            Debug.Log("Начальный счёт: " + score);
            Debug.Log("Сила клика: " + clickPower);
            UpdateScoreText();
        }

        private void Update()
        {
            score -= penaltyAmount * Time.deltaTime;
            transform.localScale = Vector3.one * (1 + score / 100f);
            UpdateScoreText();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            score += clickPower;
            Debug.Log("Счёт: " + score);
            UpdateScoreText();
        }

        private void UpdateScoreText()
        {
            if (textScore != null)
            {
                textScore.text = ((int)score).ToString();
            }
        }
    }
}

