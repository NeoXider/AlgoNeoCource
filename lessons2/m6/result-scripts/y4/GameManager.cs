using TMPro;
using UnityEngine;

namespace Lessons.M6.Y4
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private GameObject gameOverPanel;
        [SerializeField] private float difficultyDelay = 10f;
        [SerializeField] private float difficultyInterval = 10f;
        [SerializeField] private float difficultyStep = 0.2f;

        public int Score { get; private set; }
        public bool IsGameOver { get; private set; }
        public float SpeedMultiplier { get; private set; } = 1f;

        private void Start()
        {
            gameOverPanel.SetActive(false);
            UpdateScoreText();
            InvokeRepeating(nameof(IncreaseDifficulty), difficultyDelay, difficultyInterval);
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
            CancelInvoke(nameof(IncreaseDifficulty));
            gameOverPanel.SetActive(true);
        }

        private void IncreaseDifficulty()
        {
            if (IsGameOver)
            {
                return;
            }

            SpeedMultiplier += difficultyStep;
        }

        private void UpdateScoreText()
        {
            scoreText.text = "Очки: " + Score;
        }
    }
}
