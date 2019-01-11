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
            //string guess = GetGuess();
            //UpdateFileWords(Guesses, guess);
            //ReadFileWords(Answers);
            string key = RandomHouse().ToLower();
            RunGame(key);
            Console.ReadLine();
            MainMenu();
        }

        static string GetGuess()
        {
            Console.WriteLine();
            Console.WriteLine("Guess");
            string guess = Console.ReadLine();
            return guess;
        }
        public static string RandomHouse()
        {
            Random rand = new Random();
            string[] houseArray = ReadFileWords(Answers);
            int chosenHouse = rand.Next(houseArray.Length);
            return houseArray[chosenHouse];

        }

        //public static string[] GameDefault(string key)
        //{
        //    string wrong = "_";
        //    string[] defaultArray = new string[key.Length];
        //    for (int i = 0; i < key.Length; i++)
        //    {
        //        keyArray[i] = wrong;
        //        Console.Write($"{keyArray[i]} ");
        //    }
        //    return keyArray;
            
        //}
        public static void RunGame(string key)
        {
            string wrong = "_";
            
            char[] charKeyArray;
            charKeyArray = key.ToCharArray();
            string[] defaultArray = new string[charKeyArray.Length];
            bool play = true;
            for (int i = 0; i < key.Length; i++)
            {
                defaultArray[i] = wrong;
            }
            while (play)
            {

                for (int i = 0; i < key.Length; i++)
                {
                    Console.Write($"{defaultArray[i]} ");
                }
                //string[] keyArray = new string[key.Length];
                string guess = GetGuess().ToLower();
                for (int i = 0; i < charKeyArray.Length; i++)
                {
                if (key[i].ToString() == guess)
                {
                    defaultArray[i] = charKeyArray[i].ToString();
                }

                }
            }
        }

        static void CreateFile(string Path)
        {
            using (StreamWriter streamWriterG = new StreamWriter(Answers))
            {
                string[] houseArray = { "Lannister", "Baratheon", "Greyjoy", "Stark", "Tyrell", "Bolton", "Targaryen" };
                for (int i = 0; i < houseArray.Length; i++)
                {
                    streamWriterG.WriteLine($"{houseArray[i]}");
                }              
            }
            using (StreamWriter streamWriterA = new StreamWriter(Guesses))
            {

            }
        }
        static string[] ReadFileWords(string Path)
        {
            string[] words = File.ReadAllLines(Path);
            string[] answerKeyList = new string[words.Length];

            for (int i = 0; i < words.Length; i++)
            {
                answerKeyList[i] = words[i];
            }
            return answerKeyList;
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
