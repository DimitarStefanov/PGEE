using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToe
{
    class Program
    {
        const int BOARD_ROWS = 5;
        const int BOARD_COLUMNS = 8;
        const int MAX_NUMBER_OF_MOVES = BOARD_ROWS * BOARD_COLUMNS;
        const char PLAYER1MARK = 'X';
        const char PLAYER2MARK = 'O';
        const char EMPTY_FIELD_MARK = '-';
        const ConsoleColor PLAYER1COLOR = ConsoleColor.Blue;
        const ConsoleColor PLAYER2COLOR = ConsoleColor.Green;

        static void Main(string[] args)
        {
            char[,] board = InitBoard();            
            bool isFirstPlayerMove = true;

            for (int i = 0; i < MAX_NUMBER_OF_MOVES; i++)
            {
                char currentPlayerMark = isFirstPlayerMove ? PLAYER1MARK : PLAYER2MARK;
                PrintBoard(board, currentPlayerMark);

                board = AddNextMoveToBoard(board, currentPlayerMark);
                
                if (CheckForWinningMove(board, currentPlayerMark))
                {
                    Console.WriteLine($"Player {currentPlayerMark} wins");
                    break;
                }

                isFirstPlayerMove = !isFirstPlayerMove;
            }
        }

        static char[,] InitBoard()
        {
            char[,] result = new char[BOARD_ROWS, BOARD_COLUMNS];
            for(int i = 0; i < BOARD_ROWS; i++)
            {
                for (int j = 0; j < BOARD_COLUMNS; j++)
                {
                    result[i, j] = EMPTY_FIELD_MARK;
                }
            }

            return result;
        }

        static char[,] AddNextMoveToBoard(char[,] board, char playerMark)
        {
            int xCoord;
            int yCoord;

            bool isPositionTaken = true;

            do
            {
                do
                {
                    WriteLineTextInColor($"Please enter a valid X coordinate [0-{BOARD_ROWS-1}]", ConsoleColor.Yellow);
                    xCoord = int.Parse(Console.ReadLine());

                }
                while (xCoord > BOARD_ROWS - 1 || xCoord < 0);

                do
                {
                    WriteLineTextInColor($"Please enter a valid Y coordinate [0-{BOARD_COLUMNS-1}]", ConsoleColor.Red);
                    yCoord = int.Parse(Console.ReadLine());

                }
                while (yCoord > BOARD_COLUMNS - 1 || yCoord < 0);

                if(board[xCoord, yCoord] != EMPTY_FIELD_MARK)
                {
                    isPositionTaken = true;
                    Console.WriteLine("These coordinates are already taken");
                }
                else
                {
                    isPositionTaken = false;
                }
            }
            while (isPositionTaken);


            board[xCoord, yCoord] = playerMark;

            return board;
        }

        private static void PrintBoard(char[,] board, char currentPlayerMark)
        {
            Console.Clear();

            WriteTextInColor(" ", ConsoleColor.Red, ConsoleColor.DarkGray);

            for (int i = 0; i < BOARD_COLUMNS; i++)
            {
                WriteTextInColor(i.ToString(), ConsoleColor.Red, ConsoleColor.DarkGray);
            }

            WriteLineTextInColor("", ConsoleColor.Red, ConsoleColor.DarkGray);

            for (int i = 0; i < BOARD_ROWS; i++)
            {
                WriteTextInColor(i.ToString(), ConsoleColor.Yellow, ConsoleColor.DarkGray);

                for (int j = 0; j < BOARD_COLUMNS; j++)
                {
                    WriteTextInColor(board[i, j].ToString(), PrintBoardElement(board, i, j));
                }

                Console.WriteLine();
            }
            Console.WriteLine($"Player {currentPlayerMark} turn");
        }

        static bool CheckForWinningMove(char[,] board, char playerMark)
        {
            int playerMarkCounter = 0;

            for (int i = 0; i < BOARD_ROWS; i++)
            {
                for (int j = 0; j < BOARD_COLUMNS; j++)
                {
                    if(board[i, j] == playerMark)
                    {
                        playerMarkCounter++;
                    }
                }

                if(playerMarkCounter == BOARD_ROWS)
                {
                    return true;
                }

                playerMarkCounter = 0;
            }

            for (int i = 0; i < BOARD_ROWS; i++)
            {
                for (int j = 0; j < BOARD_COLUMNS; j++)
                {
                    if (board[i, j] == playerMark)
                    {
                        playerMarkCounter++;
                    }
                }

                if (playerMarkCounter == BOARD_COLUMNS)
                {
                    return true;
                }

                playerMarkCounter = 0;
            }

            if (BOARD_ROWS == BOARD_COLUMNS)
            {
                for (int i = 0; i < BOARD_ROWS; i++)
                {
                    if(board[i, i] == playerMark)
                    {
                        playerMarkCounter++;
                    }

                    if (playerMarkCounter == BOARD_COLUMNS)
                    {
                        return true;
                    }

                    playerMarkCounter = 0;

                    if (board[(BOARD_ROWS -1) - i, i] == playerMark)
                    {
                        playerMarkCounter++;
                    }

                    if (playerMarkCounter == BOARD_COLUMNS)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        static void WriteLineTextInColor(string text, ConsoleColor foregroundColor, ConsoleColor backgroundColor = ConsoleColor.Black)
        {
            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        static void WriteTextInColor(string text, ConsoleColor foregroundColor, ConsoleColor backgroundColor = ConsoleColor.Black)
        {
            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.Write(text);
            Console.ResetColor();
        }

        /// <summary>
        /// This method prints a single element from the game board array,
        /// deciding which color to use based on the player.
        /// </summary>
        /// <param name="board">The game board array.</param>
        /// <param name="i">The X element in the array.</param>
        /// <param name="j">The Y element in the array.</param>
        /// <returns></returns>
        private static ConsoleColor PrintBoardElement(char[,] board, int i, int j)
        {
            ConsoleColor elementColor = ConsoleColor.Gray;

            switch (board[i, j])
            {
                case PLAYER1MARK:
                    elementColor = PLAYER1COLOR;
                    break;
                case PLAYER2MARK:
                    elementColor = PLAYER2COLOR;
                    break;
                default:
                    elementColor = ConsoleColor.Gray;
                    break;
            }

            return elementColor;
        }
    }
}
