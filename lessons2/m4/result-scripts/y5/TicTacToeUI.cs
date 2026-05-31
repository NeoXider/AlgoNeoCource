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
            twoPlayersButton.onClick.AddListener(OnTwoPlayersClicked);
            botButton.onClick.AddListener(OnBotClicked);
        }

        public void Start()
        {
            ShowModeSelection();
        }

        public void OnDestroy()
        {
            twoPlayersButton.onClick.RemoveListener(OnTwoPlayersClicked);
            botButton.onClick.RemoveListener(OnBotClicked);
        }

        public void ShowModeSelection()
        {
            modePanel.SetActive(true);
            gamePanel.SetActive(false);
        }

        public void ShowGamePanel()
        {
            modePanel.SetActive(false);
            gamePanel.SetActive(true);
        }

        public void SetStatusText(string text)
        {
            statusText.text = text;
        }

        public void OnTwoPlayersClicked()
        {
            game.StartTwoPlayersGame();
        }

        public void OnBotClicked()
        {
            game.StartBotGame();
        }
    }
}

