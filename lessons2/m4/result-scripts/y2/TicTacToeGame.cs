using UnityEngine;

namespace Lessons.M4.Y2
{
    public class TicTacToeGame : MonoBehaviour
    {
        public BoardCell[] boardCells;
        public string xSymbol = "X";
        public string oSymbol = "O";
        public int[] board = new int[9];
        public int currentPlayer = 1;
        public bool isGameOver;

        public void Start()
        {
            PrepareBoardCells();
        }

        public void PrepareBoardCells()
        {
            for (int i = 0; i < boardCells.Length; i++)
            {
                boardCells[i].Setup(this, i);
            }
        }

        public void HandleCellClicked(int cellIndex)
        {
            if (isGameOver)
            {
                return;
            }

            if (cellIndex < 0 || cellIndex >= board.Length)
            {
                return;
            }

            if (board[cellIndex] != 0)
            {
                return;
            }

            MakeMove(cellIndex);
        }

        public void MakeMove(int cellIndex)
        {
            board[cellIndex] = currentPlayer;
            UpdateCell(cellIndex);
            SwitchPlayer();
        }

        public void SwitchPlayer()
        {
            if (currentPlayer == 1)
            {
                currentPlayer = 2;
            }
            else
            {
                currentPlayer = 1;
            }
        }

        public void UpdateCell(int cellIndex)
        {
            BoardCell boardCell = boardCells[cellIndex];

            if (board[cellIndex] == 1)
            {
                boardCell.SetSymbol(xSymbol);
            }
            else if (board[cellIndex] == 2)
            {
                boardCell.SetSymbol(oSymbol);
            }

            boardCell.SetInteractable(false);
        }
    }
}

