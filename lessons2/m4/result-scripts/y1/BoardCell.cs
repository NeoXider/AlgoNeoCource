using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Lessons.M4.Y1
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
            button.onClick.AddListener(OnCellClicked);
        }

        public void OnDestroy()
        {
            button.onClick.RemoveListener(OnCellClicked);
        }

        public void Setup(TicTacToeGame ticTacToeGame, int newCellIndex)
        {
            game = ticTacToeGame;
            cellIndex = newCellIndex;
        }

        public void SetSymbol(string symbol)
        {
            symbolText.text = symbol;
        }

        public void OnCellClicked()
        {
            game.HandleCellClicked(cellIndex);
        }
    }
}

