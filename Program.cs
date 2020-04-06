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
            int sequenceLength;
            int possibleColors;
            bool clearToGo = false;
            List<List<Dot>> allGuesses = new List<List<Dot>>();
            List<Dot> currentGuess = new List<Dot>();
            

            Console.WriteLine("How long should the sequence of dots be?");
            clearToGo = Int32.TryParse(Console.ReadLine(), out sequenceLength);

            while (!clearToGo || sequenceLength < 1 || sequenceLength > 10)
            {
                Console.WriteLine("\nInvalid input. Please type a number between 1 and 10, inclusive.");
                Console.WriteLine("How long should the sequence of dots be?");
                clearToGo = Int32.TryParse(Console.ReadLine(), out sequenceLength);
            }

            Console.WriteLine("\nHow many colors should be chosen from?");
            clearToGo = Int32.TryParse(Console.ReadLine(), out possibleColors);

            while (!clearToGo || possibleColors < 3 || possibleColors > 7)
            {
                Console.WriteLine("\nInvalid input. Please type a number between 3 and 7, inclusive.");
                Console.WriteLine("How long should the sequence of dots be?");
                clearToGo = Int32.TryParse(Console.ReadLine(), out possibleColors);
            }

            List<Dot> answer = Dot.NewAnswer(sequenceLength, possibleColors);

            Console.WriteLine();
            Console.WriteLine("R = Red\nG = Green\nB = Blue\nY = Yellow\nC = Cyan\nP = Purple\nW = White" +
                $"\n\nPlease type {sequenceLength} colors with no spaces using only the characters provided above. e.g. YBWPRP");
            clearToGo = IsValidGuess(Console.ReadLine(), sequenceLength, out currentGuess);

            while (!clearToGo)
            {
                Console.WriteLine($"\nInvalid input. Please type {sequenceLength} colors with " +
                    "no spaces using only the characters provided above. e.g. YBWPRP");
                clearToGo = IsValidGuess(Console.ReadLine(), sequenceLength, out currentGuess);
            }

            while (true)
            {
                NewTurn(ref allGuesses, ref currentGuess, ref clearToGo, ref sequenceLength);
            }
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
        /// Prints all previous guesses and asks the user to input a new guess
        /// </summary>
        /// <param name="allGuesses"></param>
        /// <param name="currentGuess"></param>
        /// <param name="clearToGo"></param>
        /// <param name="sequenceLength"></param>
        static void NewTurn(ref List<List<Dot>> allGuesses, ref List<Dot> currentGuess, ref bool clearToGo, ref int sequenceLength)
        {   
            allGuesses.Add(currentGuess);
            Dot.PrintGuesses(allGuesses);
            Console.WriteLine();
            Console.WriteLine($"Please type {sequenceLength} colors with no spaces using only the characters provided above. e.g. YBWPRP");
            clearToGo = IsValidGuess(Console.ReadLine(), sequenceLength, out currentGuess);

            while (!clearToGo)
            {
                Console.WriteLine($"\nInvalid input. Please type {sequenceLength} colors with " +
                    "no spaces using only the characters provided above. e.g. YBWPRP");
                clearToGo = IsValidGuess(Console.ReadLine(), sequenceLength, out currentGuess);
            }
        }
    }
}
