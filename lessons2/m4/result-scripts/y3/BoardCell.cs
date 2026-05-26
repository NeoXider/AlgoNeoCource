using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Lessons.M4.Y3
{
    public class BoardCell : MonoBehaviour
    {
        public int cellIndex;
        public Button button;
        public TMP_Text symbolText;
        public TicTacToeGame game;

        public void Awake()
        {
            button = GetComponent<Button>();
            symbolText = GetComponentInChildren<TMP_Text>();

            if (button != null)
            {
                button.onClick.AddListener(OnCellClicked);
            }
        }

        public void OnDestroy()
        {
            if (button != null)
            {
                button.onClick.RemoveListener(OnCellClicked);
            }
        }

        public void Setup(TicTacToeGame ticTacToeGame, int newCellIndex)
        {
            game = ticTacToeGame;
            cellIndex = newCellIndex;
        }

        public void SetSymbol(string symbol)
        {
            if (symbolText != null)
            {
                symbolText.text = symbol;
            }
        }

        public void SetInteractable(bool isInteractable)
        {
            if (button != null)
            {
                button.interactable = isInteractable;
            }
        }

        public void ClearCell()
        {
            if (symbolText != null)
            {
                symbolText.text = string.Empty;
            }

            SetInteractable(true);
        }

        public void OnCellClicked()
        {
            if (game != null)
            {
                game.HandleCellClicked(cellIndex);
            }
        }
    }
}

