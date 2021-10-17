using NUnit.Framework;
using Saper.Blazor.Contollers;
using Saper.Blazor.Objects;
using Saper.Blazor.Services;
using System.Diagnostics;

namespace Saper.Test
{
    public class GameControllerTest
    {

        GameController _gameController = new GameController();
        GameNode[,] gameField = new GameNode[3, 3];

        [SetUp]
        public void Setup()
        {
            
        }

        //Get adjacent nodes numbers
        [Test]
        public void Test1()
        {
            var list=_gameController.GetAdjacentNodes(gameField, 0, 0);
            

            Assert.AreEqual(3, list.Count);
        }
        [Test]
        public void Test1_1()
        {
            var list = _gameController.GetAdjacentNodes(gameField, 0, 2);
            
            Assert.AreEqual(3, list.Count);
        }
        [Test]
        public void Test1_2()
        {
            var list = _gameController.GetAdjacentNodes(gameField, 2, 0);

            Assert.AreEqual(3, list.Count);
        }

        [Test]
        public void Test1_3()
        {
            var list = _gameController.GetAdjacentNodes(gameField, 2, 2);

            Assert.AreEqual(3, list.Count);
        }
        [Test]
        public void Test3()
        {
            var list = _gameController.GetAdjacentNodes(gameField, 1, 0);

            Assert.AreEqual(5, list.Count);
        }
        [Test]
        public void Test3_1()
        {
            var list = _gameController.GetAdjacentNodes(gameField, 1, 2);

            Assert.AreEqual(5, list.Count);
        }
        [Test]
        public void Test3_2()
        {
            var list = _gameController.GetAdjacentNodes(gameField, 1, 2);

            Assert.AreEqual(5, list.Count);
        }
        [Test]
        public void Test4()
        {
            var list = _gameController.GetAdjacentNodes(gameField, 1, 1);

            Assert.AreEqual(8, list.Count);
        }

        [Test]
        public void Test5()
        {
            var list = _gameController.GetAdjacentNodes(gameField, 1, 1);

            Assert.AreEqual(8, list.Count);
        }

    }
}