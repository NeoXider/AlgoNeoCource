using TMPro;
using UnityEngine;

namespace Lessons.M5.Y5
{
    public class GameManager : MonoBehaviour
    {
        public int currency;
        public TMP_Text currencyText;
        public CrossbowShooter crossbow;
        public int damageUpgradeCost = 10;
        public int damageUpgradeLevel = 1;
        public TMP_Text upgradeText;

        void Start()
        {
            UpdateCurrencyText();
            UpdateUpgradeText();
        }

        public void AddCurrency(int amount)
        {
            currency += amount;
            UpdateCurrencyText();
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

        private void UpdateCurrencyText()
        {
            currencyText.text = "Монеты: " + currency;
        }

        private void UpdateUpgradeText()
        {
            upgradeText.text = "Урон +1\nУровень: " + damageUpgradeLevel + "\nЦена: " + damageUpgradeCost;
        }
    }
}