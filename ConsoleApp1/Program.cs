using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToe
{
    class Program
    {
        
        const int BOARD_ROWS = 3;
        const int BOARD_COLUMNS = 3;
        const int MAX_NUMBER_OF_MOVES = BOARD_ROWS * BOARD_COLUMNS;
        const char PLAYER1MARK = 'X';
        const char PLAYER2MARK = 'O';
        const ConsoleColor PLAYER1COLOR = ConsoleColor.DarkYellow;
        const ConsoleColor PLAYER2COLOR = ConsoleColor.DarkGreen;

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
                    Console.WriteLine();
                    WriteLineInColors($"Player {currentPlayerMark} wins",ConsoleColor.Green);
                    Console.WriteLine();
                    break;
                }
                if (currentPlayerMark =='X')
                {
                    WriteLineInColors(currentPlayerMark.ToString(),ConsoleColor.Yellow);
                }
                else
                {
                    WriteLineInColors(currentPlayerMark.ToString(), ConsoleColor.Green);
                }
                isFirstPlayerMove = !isFirstPlayerMove;
            }
            
        }
        static char[,] InitBoard()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            char[,] result = new char[BOARD_ROWS, BOARD_COLUMNS];
            char value = '1';

            for (int i = 0; i < BOARD_ROWS; i++)
            {
                for (int j = 0; j < BOARD_COLUMNS; j++)
                {
                    result[i, j] = value;
                    value++;
                }
            }
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            return result;
        }
        static char[,] AddNextMoveToBoard(char[,] board, char playerMark)
        {
            int xCoord;
            int yCoord;
            bool isPositionTaken = true;
           
            do
            {
                int userInputIndex;

                do
                {
                    WriteInColors("Въведете число от 1 до 9: ", ConsoleColor.Cyan);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    userInputIndex = int.Parse(Console.ReadLine());
                    Console.ResetColor();
                }
                while (userInputIndex < 1 || userInputIndex > 9);

                userInputIndex = userInputIndex - 1;

                yCoord = (userInputIndex % BOARD_ROWS);
                xCoord = (userInputIndex / BOARD_ROWS);

                Dictionary<char, int> PositionTaken = new Dictionary<char, int>();

                PositionTaken.Add('X', 2);
                try
                {
                    PositionTaken.Add('X', 3);
                }
                catch (ArgumentException)
                {
                    WriteLineInColors("Кординатите са заети", Console.ForegroundColor=ConsoleColor.Red);
                }

                for (int i = 0; i < 1; i++)
                {
                    if (board[xCoord, yCoord] == 'X' || board[xCoord, yCoord] == 'O')
                    {
                        isPositionTaken = true;
                    }
                    else
                    {
                        isPositionTaken = false;
                    }
                }
            }
            while (isPositionTaken);
            board[xCoord, yCoord] = playerMark;
            
            return board;
        }

        private static void PrintBoard(char[,] board, char currentPlayerMark)
        {
            Console.Clear();

            int[] numbers = new int[BOARD_COLUMNS];
            Console.ForegroundColor = ConsoleColor.DarkCyan;

            for (int i = 0; i < numbers.Length; i++)
            {
                Console.Write(" " + i);
            }

            Console.WriteLine();

            for (int i = 0; i < BOARD_ROWS; i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write(i + " ");
                for (int j = 0; j < BOARD_COLUMNS; j++)
                {
                    WriteInColors(board[i, j].ToString(), Console.ForegroundColor = ConsoleColor.Red, PrintBoardElement(board, i, j));
                }
                Console.WriteLine();
            }

            int[] numberss = new int[BOARD_COLUMNS];

            for (int i = 0; i < numbers.Length; i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write(" " + i);
                Console.ResetColor();
            }

            Console.WriteLine("\n");

            if(currentPlayerMark == 'X')
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Player 1 turn: ");
                WriteLineInColors(currentPlayerMark.ToString(), ConsoleColor.DarkYellow);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Player 2 turn: ");
                WriteLineInColors(currentPlayerMark.ToString(), ConsoleColor.DarkGreen);
            }
            Console.WriteLine();
        }
        static void PrintDiagonals(int n)
        {
            Console.WriteLine($"diagonal 1");
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"{i} {i}");
            }
            Console.WriteLine($"diagonal 2");
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"{i} {n - (i + 1)}");
            }
        }
        static bool CheckForWinningMove(char[,] board, char playerMark)
        {
            int playerMarkCounter = 0;

            for (int i = 0; i < BOARD_ROWS; i++)
            {
                for (int j = 0; j < BOARD_COLUMNS; j++)
                {
                    if (board[i, j] == playerMark)
                    {
                        playerMarkCounter++;
                    }
                }

                if (playerMarkCounter == BOARD_ROWS)
                {
                    return true;
                }

                playerMarkCounter = 0;
            }

            for (int i = 0; i < BOARD_COLUMNS; i++)
            {
                for (int j = 0; j < BOARD_ROWS; j++)
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
                    if (board[i, i] == playerMark)
                    {
                        playerMarkCounter++;
                    }

                    if (playerMarkCounter == BOARD_COLUMNS)
                    {
                        return true;
                    }

                    playerMarkCounter = 0;

                    if (board[(BOARD_ROWS - 1) - i, i] == playerMark)
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
        static void WriteLineInColors(string text, ConsoleColor foregroundColor, ConsoleColor backgroundColor = ConsoleColor.Black)
        {
            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.WriteLine(text);
            Console.ResetColor();
        }
        static void WriteInColors(string text, ConsoleColor foregroundColor, ConsoleColor backgroundColor = ConsoleColor.Black)
        {
            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.Write(text);
           
            Console.ResetColor();
        }
        static void WriteNumbersInColors(int number, ConsoleColor foregroundColor, ConsoleColor backgroundColor = ConsoleColor.Black)
        {
            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.Write(number);

            Console.ResetColor();
        }
        private static ConsoleColor PrintBoardElement(char[,] board, int i, int j, ConsoleColor backgroundColor = ConsoleColor.Black)
        {
            ConsoleColor elementColor = ConsoleColor.Black;

            switch (board[i, j])
            {
                case PLAYER1MARK:
                    elementColor = PLAYER1COLOR;
                    break;
                case PLAYER2MARK:
                    elementColor = PLAYER2COLOR;
                    break;
                default:
                    elementColor = ConsoleColor.Black;
                    break;
            }

            return elementColor;
        }
    }
}
