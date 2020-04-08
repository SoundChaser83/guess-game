using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guess_Color_game
{
    class Program
    {
        static void Main(string[] args)
        {
            List<List<Dot>> allGuesses = new List<List<Dot>>();
            List<int[]> allGuessStats = new List<int[]>();

            Console.WriteLine("How long should the sequence of dots be?");
            bool clearToGo = Int32.TryParse(Console.ReadLine(), out int sequenceLength);

            while (!clearToGo || sequenceLength < 1 || sequenceLength > 10)
            {
                Console.WriteLine("\nInvalid input. Please type a number between 1 and 10, inclusive.");
                Console.WriteLine("How long should the sequence of dots be?");
                clearToGo = Int32.TryParse(Console.ReadLine(), out sequenceLength);
            }

            Console.WriteLine("\nHow many colors should be chosen from?");
            clearToGo = Int32.TryParse(Console.ReadLine(), out int possibleColors);

            while (!clearToGo || possibleColors < 3 || possibleColors > 7)
            {
                Console.WriteLine("\nInvalid input. Please type a number between 3 and 7, inclusive.");
                Console.WriteLine("How many colors should be chosen from?");
                clearToGo = Int32.TryParse(Console.ReadLine(), out possibleColors);
            }

            List<Dot> answer = Dot.NewAnswer(sequenceLength, possibleColors);

            Console.WriteLine();
            Console.WriteLine("R = Red\nG = Green\nB = Blue\nY = Yellow\nC = Cyan\nP = Purple\nW = White\n");

            do
            {
                /*Console.WriteLine("\nThe correct answer is...");
                Dot.PrintDots(answer);
                Console.WriteLine();*/

                Console.WriteLine($"Please type {sequenceLength} colors with no spaces using only the characters provided above. e.g. YBWPRP");
                clearToGo = IsValidGuess(Console.ReadLine(), sequenceLength, out List<Dot> currentGuess);

                while (!clearToGo)
                {
                    Console.WriteLine($"\nInvalid input. Please type {sequenceLength} colors with " +
                        "no spaces using only the characters provided above. e.g. YBWPRP");
                    clearToGo = IsValidGuess(Console.ReadLine(), sequenceLength, out currentGuess);
                }

                allGuesses.Add(currentGuess);
                allGuessStats.Add(new int[2] { MatchedPositions(answer, currentGuess), MatchedColors(answer, currentGuess) });
                Dot.PrintGuessesInfo(allGuesses, allGuessStats);
                Console.WriteLine();
                
            } while (allGuessStats[allGuessStats.Count - 1][0] != sequenceLength);

            Console.Write("Hooray! The correct answer was");
            Dot.PrintDots(answer);
            Console.WriteLine($"\n\nIt took you {allGuessStats.Count} guesses to find the answer.");
            Console.ReadLine();
        }

        /// <summary>
        /// Returns a boolean indicating whether the given string is of the given length and
        /// contains only characters that can represent colors. Also sends out a list of dots
        /// corresponding to the input string
        /// </summary>
        /// <param name="guess"></param>
        /// <param name="length"></param>
        /// <param name="guessDots"></param>
        /// <returns></returns>
        static bool IsValidGuess(string guess, int length, out List<Dot> guessDots)
        {
            guessDots = new List<Dot>();
            guess = guess.ToUpper();

            if (guess.Length != length)
            {
                return false;
            }

            foreach (char c in guess)
            {
                if (c != 'R' && c != 'G' && c != 'B' && c != 'Y' 
                    && c != 'C' && c != 'P' && c != 'W')
                {
                    return false;
                }

                guessDots.Add(new Dot(c.ToString()));
            }

            return true;
        }

        /// <summary>
        /// Returns the number of dots in currentGuess which match position and color with answer
        /// </summary>
        /// <param name="answer"></param>
        /// <param name="currentGuess"></param>
        /// <returns></returns>
        static int MatchedPositions(List<Dot> answer, List<Dot> currentGuess)
        {
            int matches = 0;

            for (int i = 0; i < answer.Count; i++)
            {
                if (answer[i].Color == currentGuess[i].Color)
                {
                    matches++;
                }
            }

            return matches;
        }

        /// <summary>
        /// Returns the number of dots in currentGuess which match the colors in answer
        /// </summary>
        /// <param name="answer"></param>
        /// <param name="currentGuess"></param>
        /// <returns></returns>
        static int MatchedColors(List<Dot> answer, List<Dot> currentGuess)
        {
            int matches = 0;
            List<string> tempAnswer = new List<string>();
            List<string> tempGuess = new List<string>();

            for (int i = 0; i < answer.Count; i++)
            {
                tempAnswer.Add(answer[i].Color);
                tempGuess.Add(currentGuess[i].Color);
            }

            while (tempAnswer.Count > 0)
            {
                for (int i = 0; i < tempGuess.Count; i++)
                {
                    if (tempGuess[i] == tempAnswer[0])
                    {
                        matches++;
                        tempGuess.RemoveAt(i);
                        i = tempGuess.Count;
                    }
                }

                tempAnswer.RemoveAt(0);
            }

            return matches;
        }
    }
}
