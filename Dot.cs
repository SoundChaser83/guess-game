using System;
using System.Collections.Generic;

namespace Guess_Color_game
{
    class Dot
    {
        string Symbol { get; set; } = " █";
        public string Color { get; set; }
        static Random rng = new Random();

        public Dot(string color)
        {
            Color = color;
        }

        Dot(int rngColor)
        {
            if (rngColor == 0)
            {
                Color = "R";
            }
            else if (rngColor == 1)
            {
                Color = "G";
            }
            else if (rngColor == 2)
            {
                Color = "B";
            }
            else if (rngColor == 3)
            {
                Color = "Y";
            }
            else if (rngColor == 4)
            {
                Color = "C";
            }
            else if (rngColor == 5)
            {
                Color = "P";
            }
            else if (rngColor == 6)
            {
                Color = "W";
            }
        }

        /// <summary>
        /// Prints all members of list dots to the console in their associated colors
        /// </summary>
        /// <param name="dots"></param>
        public static void PrintDots(List<Dot> dots)
        {
            foreach (Dot d in dots)
            {
                if (d.Color == "R")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(d.Symbol, Console.ForegroundColor);
                }
                else if (d.Color == "G")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(d.Symbol, Console.ForegroundColor);
                }
                else if (d.Color == "B")
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write(d.Symbol, Console.ForegroundColor);
                }
                else if (d.Color == "Y")
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(d.Symbol, Console.ForegroundColor);
                }
                else if (d.Color == "C")
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(d.Symbol, Console.ForegroundColor);
                }
                else if (d.Color == "P")
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write(d.Symbol, Console.ForegroundColor);
                }
                else if (d.Color == "W")
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(d.Symbol, Console.ForegroundColor);
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Prints all guesses to the console along with their match statistics
        /// </summary>
        /// <param name="guesses"></param>
        /// <param name="guessStats"></param>
        public static void PrintGuessesInfo(List<List<Dot>> guesses, List<int[]> guessStats)
        {
            for (int i = 0; i < guesses.Count; i++)
            {                
                Console.WriteLine();
                PrintDots(guesses[i]);
                Console.Write($"     Matched positions: {guessStats[i][0]}     Matched colors: {guessStats[i][1]}");
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Generates a new list of dots of the given length and possible number of colors
        /// </summary>
        /// <param name="length"></param>
        /// <param name="possibleColors"></param>
        /// <returns></returns>
        public static List<Dot> NewAnswer(int length, int possibleColors)
        {
            List<Dot> answer = new List<Dot>();

            for (int i = 0; i < length; i++)
            {
                answer.Add(new Dot(rng.Next(possibleColors)));
            }

            return answer;
        }
    }
}