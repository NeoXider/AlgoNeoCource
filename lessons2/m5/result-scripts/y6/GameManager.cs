using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lessons.M5.Y6
{
    public class GameManager : MonoBehaviour
    {
        public int currency;
        public int enemiesKilled;
        public int playerHealth = 10;
        public TMP_Text currencyText;
        public TMP_Text scoreText;
        public TMP_Text upgradeText;
        public GameObject gameOverPanel;
        public TMP_Text gameOverText;
        public CrossbowShooter crossbow;
        public int damageUpgradeCost = 10;
        public int damageUpgradeLevel = 1;

        void Start()
        {
            Time.timeScale = 1f;
            gameOverPanel.SetActive(false);
            UpdateCurrencyText();
            UpdateScoreText();
            UpdateUpgradeText();
        }

        public void AddCurrency(int amount)
        {
            currency += amount;
            enemiesKilled += 1;
            UpdateCurrencyText();
            UpdateScoreText();
        }

        public void BuyDamageUpgrade()
        {
            if (currency < damageUpgradeCost)
            {
                return;
            }

            currency -= damageUpgradeCost;
            crossbow.boltDamage += 1;
            damageUpgradeLevel += 1;
            UpdateCurrencyText();
            UpdateUpgradeText();
        }

        public void GameOver()
        {
            gameOverText.text = "Игра окончена\nСчёт: " + enemiesKilled;
            gameOverPanel.SetActive(true);
            Time.timeScale = 0f;
        }

        public void RestartGame()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void UpdateCurrencyText()
        {
            currencyText.text = "Монеты: " + currency;
        }

        private void UpdateScoreText()
        {
            scoreText.text = "Счёт: " + enemiesKilled + "\nЗдоровье: " + playerHealth;
        }

        public void UpdateHealthText(int health)
        {
            playerHealth = health;
            UpdateScoreText();
        }

        private void UpdateUpgradeText()
        {
            upgradeText.text = "Урон +1\nУровень: " + damageUpgradeLevel + "\nЦена: " + damageUpgradeCost;
        }
    }
}