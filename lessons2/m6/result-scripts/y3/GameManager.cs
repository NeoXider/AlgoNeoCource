using TMPro;
using UnityEngine;

namespace Lessons.M6.Y3
{
    public class GameManager : MonoBehaviour
    {
        [Header("Настройки")]
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private GameObject gameOverPanel;

        public int Score { get; private set; }
        public bool IsGameOver { get; private set; }

        private void Start()
        {
            gameOverPanel.SetActive(false);
            UpdateScoreText();
        }

        public void AddScore(int points)
        {
            if (IsGameOver)
            {
                return;
            }

            Score += points;
            UpdateScoreText();
        }

        public void FinishGame()
        {
            if (IsGameOver)
            {
                return;
            }

            IsGameOver = true;
            gameOverPanel.SetActive(true);
        }

        private void UpdateScoreText()
        {
            scoreText.text = "Очки: " + Score;
        }
    }
}
