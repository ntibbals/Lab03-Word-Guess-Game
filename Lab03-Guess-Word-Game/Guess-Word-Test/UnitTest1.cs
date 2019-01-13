using System;
using Xunit;
using Lab03_Guess_Word_Game;

namespace Guess_Word_Test
{
    public class UnitTest1
    {
        [Fact]
        public void TestUpdateWord()
        {
            /// test that given a new word, will add to existing file
            string testHouse = "TESTHOUSE";
            string[] expectedResult = { "LANNISTER", "BARATHEON", "GREYJOY", "STARK", "TYRELL", "BOLTON", "TARGARYEN", "TESTHOUSE" };
            Assert.Equal(expectedResult, Program.AddHouse(testHouse));
        }

        [Fact]
        public void TestDeleteWord()
        {
            /// test that given a word to delete, will delete and return updated file
            string testHouse = "TARGARYEN";
            string testAnswers = "../../../../../GameWords.txt";
            string[] expectedResult = { "LANNISTER", "BARATHEON", "GREYJOY", "STARK", "TYRELL", "BOLTON" };
            Assert.Equal(expectedResult, Program.DeleteHouse(testHouse, testAnswers));
        }
    }
}
