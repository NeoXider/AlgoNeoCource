using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Lessons.M4.Y5
{
    public class TicTacToeUI : MonoBehaviour
    {
        public TicTacToeGame game;
        public TMP_Text statusText;
        public GameObject modePanel;
        public GameObject gamePanel;
        public Button twoPlayersButton;
        public Button botButton;

        public void Awake()
        {
            if (twoPlayersButton != null)
            {
                twoPlayersButton.onClick.AddListener(OnTwoPlayersClicked);
            }

            if (botButton != null)
            {
                botButton.onClick.AddListener(OnBotClicked);
            }
        }

        public void Start()
        {
            ShowModeSelection();
        }

        public void OnDestroy()
        {
            if (twoPlayersButton != null)
            {
                twoPlayersButton.onClick.RemoveListener(OnTwoPlayersClicked);
            }

            if (botButton != null)
            {
                botButton.onClick.RemoveListener(OnBotClicked);
            }
        }

        public void ShowModeSelection()
        {
            if (modePanel != null)
            {
                modePanel.SetActive(true);
            }

            if (gamePanel != null)
            {
                gamePanel.SetActive(false);
            }
        }

        public void ShowGamePanel()
        {
            if (modePanel != null)
            {
                modePanel.SetActive(false);
            }

            if (gamePanel != null)
            {
                gamePanel.SetActive(true);
            }
        }

        public void SetStatusText(string text)
        {
            if (statusText != null)
            {
                statusText.text = text;
            }
        }

        public void OnTwoPlayersClicked()
        {
            if (game != null)
            {
                game.StartTwoPlayersGame();
            }
        }

        public void OnBotClicked()
        {
            if (game != null)
            {
                game.StartBotGame();
            }
        }
    }
}

