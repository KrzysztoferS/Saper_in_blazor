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

            //int a = gameService.GetEmptyNodes().Count;
            int counter = 0;
            foreach(var node in gameService.GameField)
            {
                if (node.nodeStatus == Blazor.Helpers.NodeStatus.Bomb) counter++;
            }

            Assert.AreEqual(5,counter);
        }
        [Test]
        public void Test3()
        {
            GameService gameService = new GameService(5, 5, 5);

            int a = gameService.GetEmptyNodes().Count;
          

            Assert.AreEqual(20, a);
        }
    }
}