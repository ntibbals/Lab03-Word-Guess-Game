using System;
using System.IO;
using System.Linq;

namespace Lab03_Guess_Word_Game
{
    public class Program
    {
        /// <summary>
        /// instantiating the file path
        /// </summary>
        public static string Answers = "../../../../../GameWords.txt";
        /// <summary>
        /// Main method for continuous applicaiton
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            bool displayMenu = true;
            CreateFile(Answers);
            while (displayMenu)
            {


                MainMenu();

                   
            }
        }
        /// <summary>
        /// Menu method to control initial display switch
        /// </summary>
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
        /// <summary>
        /// Administrative switch method
        /// </summary>
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

        /// <summary>
        /// Initializes the game
        /// </summary>
        public static void StartGame()
        {
            Console.Clear();
            Console.WriteLine("What is your house?");
            RunGame(RandomHouse().ToUpper());
            Console.ReadLine();
            MainMenu();
        }

        /// <summary>
        /// retrieves the guess from end user
        /// </summary>
        /// <returns>guess</returns>
        public static string GetGuess()
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
        /// <summary>
        /// Selects a random house from file created
        /// </summary>
        /// <returns>random house for the game</returns>
        public static string RandomHouse()
        {
            Random rand = new Random();
            string[] houseArray = ReadFileWords(Answers);
            int chosenHouse = rand.Next(houseArray.Length);
            return houseArray[chosenHouse];

        }
        /// <summary>
        /// Instantiates a the game default array to display
        /// </summary>
        /// <param name="key">random house string</param>
        /// <returns>default array for game load</returns>
        public static string[] DefaultArray(string key)
        {
            char[] charKeyArray = key.ToCharArray();
            string[] defaultArray = new string[charKeyArray.Length]; ///instantiates a new array allocating enough memory based on the length of the key
            for (int i = 0; i < key.Length; i++)
            {
                defaultArray[i] = "_"; ///fills each position of array with character
            }
            return defaultArray;
        }
        /// <summary>
        /// Starts game logic
        /// </summary>
        /// <param name="key">random house key</param>
        public static void RunGame(string key)
        {
            string wrong = "_";
            char[] charKeyArray;
            charKeyArray = key.ToCharArray();
            string[] defaultArray = DefaultArray(key); /// initializes new array and calls defaultarray method
            string[] guessArray = new string[25]; ///set maximum amount of guess
            int counter = 0;
            while (defaultArray.Contains(wrong) && counter < 24) ///sets while loop to end if user guesses word or runs out of guesses
            {

                Console.Write($"{String.Join(" ", defaultArray)}"); ///rewrites default array each time based on users guesses
                Console.WriteLine();
                if (counter > 0) /// only displays previous guess array if greater than 0
                {
                    Console.WriteLine($"Previous guesses: {String.Join(" ", guessArray)}");
                }

                string guess = GetGuess(); /// retrieves guess each turn
                for (int i = 0; i < charKeyArray.Length; i++)
                {
                    
                    if (key[i].ToString() == guess) /// looks for guesses equal to the key
                {
                    defaultArray[i] = charKeyArray[i].ToString(); /// if true, replaces each position of default array with each position of key array
                }

                }
                counter++;
                guessArray[counter] = guess; /// set value for each guess in array - index based on counter position
  
            }
            if(defaultArray.Contains(wrong)) /// if user still has not guessed house - displays
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
        /// <summary>
        /// Instantiates Initial File
        /// </summary>
        /// <param name="Path">file path</param>
        public static void CreateFile(string Path)
        {
            using (StreamWriter streamWriterG = new StreamWriter(Answers)) ///using statement ensures method ends after creation of file
            {
                string[] houseArray = { "LANNISTER", "BARATHEON", "GREYJOY", "STARK", "TYRELL", "BOLTON", "TARGARYEN" };
                for (int i = 0; i < houseArray.Length; i++)
                {
                    streamWriterG.WriteLine($"{houseArray[i]}");
                }              
            }
        }
        /// <summary>
        /// Reads File
        /// </summary>
        /// <param name="Path">file path</param>
        /// <returns>an array of words from file</returns>
        public static string[] ReadFileWords(string Path)
        {
            string[] words = File.ReadAllLines(Answers); /// reads file based on path
            string[] answerKeyList = new string[words.Length];

            ///for loop wich places words from file into new answer array for random method
            for (int i = 0; i < words.Length; i++)
            {
                answerKeyList[i] = words[i];
            }
            return answerKeyList;
        }
        /// <summary>
        /// Displays current house array from file
        /// </summary>
        /// <param name="Path">file path</param>
        /// <returns>an array of words from file</returns>
        public static string[] DisplayFileHouses(string Path)
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

        /// <summary>
        /// Asks user for new house to add
        /// </summary>
        /// <returns>user input for addition</returns>
        public static string AskForNewHouse ()
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
        /// <summary>
        /// Adds house taken from users into text file
        /// </summary>
        /// <param name="input">user input for new house</param>
        /// <returns>updated array</returns>
        public static string[] AddHouse(string input)
        {
            using (StreamWriter streamWriter = File.AppendText(Answers))
            {
                streamWriter.WriteLine($"{input}");
            }
            string[] newHouses = DisplayFileHouses(Answers);

            return newHouses;
        }

        /// <summary>
        /// Asks user for input for deletion
        /// </summary>
        /// <returns>user input</returns>
        public static string AskForDeletion ()
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

        /// <summary>
        /// Selects house to delete from file
        /// </summary>
        /// <param name="input">user input for deletion</param>
        /// <param name="Path">file path</param>
        /// <returns>updated array</returns>
        public static string[] DeleteHouse(string input, string Path)
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

        /// <summary>
        /// Creates a new file passed on current array
        /// </summary>
        /// <param name="newList">updated array list</param>
        /// <param name="Path">file path</param>
       public static void CreateUpdatedFile(string[] newList, string Path)
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
        /// <summary>
        /// Deletes file
        /// </summary>
        /// <param name="Path">file path</param>
        public static void DeleteFile(string Path)
        {
            File.Delete(Answers);
        }
    }
}
