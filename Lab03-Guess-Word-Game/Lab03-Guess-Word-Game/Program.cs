using System;
using System.IO;
using System.Linq;

namespace Lab03_Guess_Word_Game
{
    public class Program
    {
        public static string Answers = "../../../../../GameWords.txt";
        static void Main(string[] args)
        {
            bool displayMenu = true;
            CreateFile(Answers);
            while (displayMenu)
            {


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
                    case "2":
                        Admin();
                        break;

                    case "3":
                        DeleteFile(Answers);
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
        public static void Admin()
        {
            Console.Clear();
            try
            {
                Console.Clear();
                Console.WriteLine("1) Add a house");
                Console.WriteLine("2) Delete a house");
                Console.WriteLine("3) View all houses");
                Console.WriteLine("4) Go back to main menu");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        DisplayFileHouses(Answers);
                        AddHouse(AskForNewHouse());
                        DisplayFileHouses(Answers);
                        Console.ReadLine();
                        break;

                    case "2":
                        DisplayFileHouses(Answers);
                        CreateUpdatedFile(DeleteHouse(AskForDeletion(), Answers), Answers);
                        DisplayFileHouses(Answers);
                        Console.ReadLine();
                        break;

                    case "3":
                        DisplayFileHouses(Answers);
                        Console.ReadLine();
                        break;

                    case "4":
                        MainMenu();
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
            Console.Clear();
            Console.WriteLine("What is your house?");
            RunGame(RandomHouse().ToUpper());
            Console.ReadLine();
            MainMenu();
        }

        static string GetGuess()
        {
            Console.WriteLine();
            string guess;
            try
            {
                Console.WriteLine("Guess");
                guess = Console.ReadLine().ToUpper();
            }
            catch (Exception e)
            {
                throw e;
            }
            return guess;
        }
        public static string RandomHouse()
        {
            Random rand = new Random();
            string[] houseArray = ReadFileWords(Answers);
            int chosenHouse = rand.Next(houseArray.Length);
            return houseArray[chosenHouse];

        }

        public static string[] DefaultArray(string key)
        {
            char[] charKeyArray = key.ToCharArray();
            string[] defaultArray = new string[charKeyArray.Length];
            for (int i = 0; i < key.Length; i++)
            {
                defaultArray[i] = "_";
            }
            return defaultArray;
        }
        public static void RunGame(string key)
        {
            
            string wrong = "_";
            char[] charKeyArray;
            charKeyArray = key.ToCharArray();
            string[] defaultArray = DefaultArray(key);
            string[] guessArray = new string[25];
            int counter = 0;
            while (defaultArray.Contains(wrong) && counter < 24)
            {

                Console.Write($"{String.Join(" ", defaultArray)}");
                Console.WriteLine();
                if (counter > 0)
                {
                    Console.WriteLine($"Previous guesses: {String.Join(" ", guessArray)}");
                }

                string guess = GetGuess();
                for (int i = 0; i < charKeyArray.Length; i++)
                {
                    
                    if (key[i].ToString() == guess)
                {
                    defaultArray[i] = charKeyArray[i].ToString();
                }

                }
                counter++;
                guessArray[counter] = guess;
  
            }
            if(defaultArray.Contains(wrong))
            {
                Console.WriteLine($"Sorry but you've had one too many guesses. The house is {key}. Try again.");
            }
            else
            {
                Console.WriteLine($"{key}");
                Console.WriteLine($"Hooray! Your are in house {key}");
            }

            try
            {
                Console.WriteLine("Play again? y/n");
                string again = Console.ReadLine();
                if (again == "y")
                {
                    StartGame();
                }
                else if (again == "n")
                {
                    DeleteFile(Answers);
                    Environment.Exit(0);
                }
                Console.ReadLine();
            }
            catch(Exception e)
            {
                throw e;
            }

        }

        static void CreateFile(string Path)
        {
            using (StreamWriter streamWriterG = new StreamWriter(Answers))
            {
                string[] houseArray = { "LANNISTER", "BARATHEON", "GREYJOY", "STARK", "TYRELL", "BOLTON", "TARGARYEN" };
                for (int i = 0; i < houseArray.Length; i++)
                {
                    streamWriterG.WriteLine($"{houseArray[i]}");
                }              
            }
        }
        static string[] ReadFileWords(string Path)
        {
            string[] words = File.ReadAllLines(Answers);
            string[] answerKeyList = new string[words.Length];

            for (int i = 0; i < words.Length; i++)
            {
                answerKeyList[i] = words[i];
            }
            return answerKeyList;
        }

        static string[] DisplayFileHouses(string Path)
        {
            string[] words = File.ReadAllLines(Answers);
            string[] answerKeyList = new string[words.Length];
            Console.WriteLine("Current list of houses:");
            Console.WriteLine();
            for (int i = 0; i < words.Length; i++)
            {
                answerKeyList[i] = words[i];
            }
            foreach(string house in words)
            {
                Console.WriteLine(house);
            }
            return answerKeyList;
        }

        static string AskForNewHouse ()
        {
            string input;
            try
            {
                Console.WriteLine("What house would you like to add?");
                input = Console.ReadLine().ToUpper();
            }
            catch (Exception e)
            {
                throw e;
            }

            return input;
        }
        static void AddHouse(string input)
        {
            using (StreamWriter streamWriter = File.AppendText(Answers))
            {
                streamWriter.WriteLine($"{input}");
            }
        }

        static string AskForDeletion ()
        {
            string input;
            try
            {
                Console.WriteLine("What house would you like to remove?");
                input = Console.ReadLine().ToUpper();
            }
            catch (Exception e)
            {
                throw e;
            }

            return input;
        }

        static string[] DeleteHouse(string input, string Path)
        {
            string[] words = File.ReadAllLines(Answers);
            string[] answerKeyList = new string[words.Length];
            string[] newKeyList = new string[answerKeyList.Length - 1];

            for (int i = 0; i < words.Length; i++)
            {
                answerKeyList[i] = words[i];
            }
            for (int i = 0; i < newKeyList.Length; i++)
            {
                if (input != answerKeyList[i])
                {
                    newKeyList[i] = answerKeyList[i];
                }
            }
            return newKeyList;
        }

        static void CreateUpdatedFile(string[] newList, string Path)
        {
            DeleteFile(Answers);
            using (StreamWriter streamWriterG = new StreamWriter(Answers))
            {
                for (int i = 0; i < newList.Length; i++)
                {
                    streamWriterG.WriteLine($"{newList[i]}");
                }
            }
        }
        static void DeleteFile(string Path)
        {
            File.Delete(Answers);
        }
    }
}
