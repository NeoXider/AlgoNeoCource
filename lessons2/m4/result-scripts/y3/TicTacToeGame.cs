using System.Collections.Generic;
using UnityEngine;

namespace Lessons.M4.Y3
{
    public class TicTacToeGame : MonoBehaviour
    {
        public List<BoardCell> boardCells = new List<BoardCell>();
        public string xSymbol = "X";
        public string oSymbol = "O";
        public int[] board = new int[9];
        public int currentPlayer = 1;
        public bool isGameOver;

        public void Start()
        {
            PrepareBoardCells();
            ResetBoard();
        }

        public void PrepareBoardCells()
        {
            for (int i = 0; i < boardCells.Count; i++)
            {
                boardCells[i].Setup(this, i);
            }
        }

        public void ResetBoard()
        {
            for (int i = 0; i < board.Length; i++)
            {
                board[i] = 0;
            }

            foreach (BoardCell boardCell in boardCells)
            {
                boardCell.ClearCell();
            }

            currentPlayer = 1;
            isGameOver = false;
        }

        public void HandleCellClicked(int cellIndex)
        {
            if (isGameOver)
            {
                return;
            }

            if (!IsCellIndexValid(cellIndex))
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

            if (CheckWinner(currentPlayer))
            {
                FinishGameWithWinner(currentPlayer);
                return;
            }

            if (IsBoardFull())
            {
                FinishGameWithDraw();
                return;
            }

            SwitchPlayer();
        }

        public bool IsCellIndexValid(int cellIndex)
        {
            return cellIndex >= 0 && cellIndex < board.Length;
        }

        public bool MatchesLine(int player, int a, int b, int c)
        {
            return board[a] == player && board[b] == player && board[c] == player;
        }

        public bool CheckWinner(int player)
        {
            if (MatchesLine(player, 0, 1, 2)) return true;
            if (MatchesLine(player, 3, 4, 5)) return true;
            if (MatchesLine(player, 6, 7, 8)) return true;

            if (MatchesLine(player, 0, 3, 6)) return true;
            if (MatchesLine(player, 1, 4, 7)) return true;
            if (MatchesLine(player, 2, 5, 8)) return true;

            if (MatchesLine(player, 0, 4, 8)) return true;
            if (MatchesLine(player, 2, 4, 6)) return true;

            return false;
        }

        public bool IsBoardFull()
        {
            for (int i = 0; i < board.Length; i++)
            {
                if (board[i] == 0)
                {
                    return false;
                }
            }

            return true;
        }

        public void FinishGameWithWinner(int winner)
        {
            isGameOver = true;
            SetBoardInteractable(false);
            Debug.Log("Победил игрок: " + winner);
        }

        public void FinishGameWithDraw()
        {
            isGameOver = true;
            SetBoardInteractable(false);
            Debug.Log("Ничья!");
        }

        public void SetBoardInteractable(bool isInteractable)
        {
            foreach (BoardCell boardCell in boardCells)
            {
                boardCell.SetInteractable(isInteractable);
            }
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

