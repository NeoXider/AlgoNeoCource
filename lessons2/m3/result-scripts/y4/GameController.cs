using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lessons.M3.Y4
{
    public class GameController : MonoBehaviour
    {
        public bool isGameOver;
        public TMP_Text scoreText;
        public GameObject gameOverPanel;

        private float score;

        private void Start()
        {
            if (gameOverPanel != null)
            {
                gameOverPanel.SetActive(false);
            }
        }

        private void Update()
        {
            if (isGameOver)
            {
                return;
            }

            score += Time.deltaTime;
            if (scoreText != null)
            {
                scoreText.text = ((int)score).ToString();
            }
        }

        public void GameOver()
        {
            isGameOver = true;
            if (gameOverPanel != null)
            {
                gameOverPanel.SetActive(true);
            }
        }

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}

