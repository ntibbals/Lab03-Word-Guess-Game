using System;
using System.IO;

namespace Lab03_Guess_Word_Game
{
    public class Program
    {
        public static string Path = "../../../../../GameWords.txt";

        static void Main(string[] args)
        {
            bool displayMenu = true;
            while(displayMenu)
            {
                CreateFile(Path);
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
                        DeleteFile(Path);
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
            string guess = GetGuess();
            UpdateFileWords(Path, guess);
            ReadFileWords(Path);
            Console.ReadLine();
            MainMenu();
        }

        static string GetGuess()
        {
            Console.WriteLine("Guess");
            string guess = Console.ReadLine();
            return guess;
        }
        static void CreateFile(string Path)
        {
            using (StreamWriter streamWriter = new StreamWriter(Path))
            {
                streamWriter.WriteLine("Test1");
                
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
