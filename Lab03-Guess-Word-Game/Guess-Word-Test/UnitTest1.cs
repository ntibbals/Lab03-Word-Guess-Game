using System;
using Xunit;
using Lab03_Guess_Word_Game;

namespace Guess_Word_Test
{
    public class UnitTest1
    {
        [Fact]
        public void TestUpdate()
        {
            string testHouse = "TESTHOUSE";
            string[] expectedResult = { "LANNISTER", "BARATHEON", "GREYJOY", "STARK", "TYRELL", "BOLTON", "TARGARYEN", "TESTHOUSE" };
            Assert.Equal(expectedResult, Program.AddHouse(testHouse));
        }
    }
}
