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

        void Start()
        {
            UpdateCurrencyText();
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
            UpdateCurrencyText();
        }

        private void UpdateCurrencyText()
        {
            currencyText.text = "Монеты: " + currency;
        }
    }
}
