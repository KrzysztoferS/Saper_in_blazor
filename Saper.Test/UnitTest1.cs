using NUnit.Framework;
using Saper.Blazor.Services;

namespace Saper.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            GameService gameService = new GameService(5, 5, 5);

            Assert.IsNotNull(gameService);
        }
        [Test]
        public void Test2()
        {
            GameService gameService = new GameService(5, 5, 5);
            //gameService.StartGame();
            
            int counter = 0;
            foreach(var node in gameService.GameField)
            {
                if (node.nodeStatus == Blazor.Helpers.NodeStatus.Bomb) counter++;
            }

            Assert.AreEqual(5,counter);
        }
        
    }
}