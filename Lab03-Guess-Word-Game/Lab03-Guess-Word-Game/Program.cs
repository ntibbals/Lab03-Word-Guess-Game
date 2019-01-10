using System;
using System.IO;

namespace Lab03_Guess_Word_Game
{
    public class Program
    {
        public static string Answers = "../../../../../GameWords.txt";
        public static string Guesses = "../../../../../GuessWords.txt";
        static void Main(string[] args)
        {
            bool displayMenu = true;
            while(displayMenu)
            {
                CreateFile(Answers);
                CreateFile(Guesses);

                MainMenu();

                   
            }
        }

        public static void MainMenu()
        {
            Console.Clear();
            try
            {
                Console.Clear();
                Console.WriteLine("Welcome to the Word Guess Game!");
                Console.WriteLine("1) Start game");
                Console.WriteLine("2) Admin");
                Console.WriteLine("3) Exit");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":      
                        StartGame();
                        break;

                    case "3":
                        DeleteFile(Answers);
                        DeleteFile(Guesses);
                        Environment.Exit(0);
                        break;

                }
            }
            catch (ArgumentNullException e) //catch null exception
            {
                Console.WriteLine($"You did not enter anything. Please try again.");
                Console.ReadLine();
            }
            catch (FormatException e) // catch format exception
            {
                Console.WriteLine($"Unable to read format. Please try agian.");
                Console.ReadLine();
            }
            catch (Exception e) // catch all exception(general)
            {
                Console.WriteLine($"You've hit the following exception: {e.Message}.");
                Console.ReadLine();
            }

        }

        static void StartGame()
        {
            Console.WriteLine("Game Running.");
            RandomHouse();
            //string guess = GetGuess();
            //UpdateFileWords(Guesses, guess);
            //ReadFileWords(Answers);
            Console.ReadLine();
            MainMenu();
        }

        static string GetGuess()
        {
            Console.WriteLine("Guess");
            string guess = Console.ReadLine();
            return guess;
        }
        public static string RandomHouse()
        {
            Random rand = new Random();
            string[] houseArray = { "Lannister", "Baratheon", "Greyjoy", "Stark", "Tyrell", "Bolton", "Targaryen" };
            int chosenHouse = rand.Next(houseArray.Length);
            Console.WriteLine(houseArray[chosenHouse]);
            return houseArray[chosenHouse];

        }
        static void CreateFile(string Path)
        {
            using (StreamWriter streamWriterG = new StreamWriter(Answers))
            {
                string[] houseArray = { "Lannister", "Baratheon", "Greyjoy", "Stark", "Tyrell", "Bolton", "Targaryen" };
                for (int i = 0; i < houseArray.Length; i++)
                {
                    streamWriterG.Write($" {houseArray[i]}");
                }              
            }
            using (StreamWriter streamWriterA = new StreamWriter(Guesses))
            {

            }
        }
        static void ReadFileWords(string Path)
        {
            string[] words = File.ReadAllLines(Path);
            //string key = guess;
            
            for (int i = 0; i < words.Length; i++)
            {
                    Console.WriteLine(words[i]);
            }
        }

        static void UpdateFileWords(string Path, string guess)
        {
            using (StreamWriter streamWriter = File.AppendText(Path))
            {
                streamWriter.WriteLine($"{guess}");
            }
        }

        static void DeleteFile(string Path)
        {
            File.Delete(Path);
        }
    }
}
