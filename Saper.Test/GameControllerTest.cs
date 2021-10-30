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

        [Test]
        public void CheckNodeNumbers()
        {
            _gameController.FillGameField(gameField);

            Assert.AreEqual(0, gameField[0, 0].X);
            Assert.AreEqual(0, gameField[0, 0].Y);

            Assert.AreEqual(1, gameField[1, 1].X);
            Assert.AreEqual(1, gameField[1, 1].Y);

            Assert.AreEqual(2, gameField[2, 2].X);
            Assert.AreEqual(2, gameField[2, 2].Y);

            Assert.AreEqual(0, gameField[0, 2].X);
            Assert.AreEqual(2, gameField[0, 2].Y);

         
        }

        [Test]
        public void SetNodesStatus()
        {
            _gameController.FillGameField(gameField);
            //_gameController.SetBombsOnField(gameField,2);
            gameField[0, 0].nodeStatus = Blazor.Helpers.NodeStatus.Bomb;
            gameField[2, 2].nodeStatus = Blazor.Helpers.NodeStatus.Bomb;
            _gameController.SetNodesStatus(gameField);

            Assert.AreEqual(Blazor.Helpers.NodeStatus.One, gameField[1, 0].nodeStatus);
        }


        [Test]
        public void GameControllerFillTest()
        {
            _gameController.FillGameField(gameField);
            foreach(var node in gameField)
            {
                Assert.IsNotNull(node);
            }
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

        [Test]
        public void WinLooseCondition1()
        {
            _gameController.NumberOfVisitedNodes = 1;
            _gameController.FillGameField(gameField);
            gameField[0, 0].nodeStatus = Blazor.Helpers.NodeStatus.Empty;
           bool value =_gameController.IsGameOver(gameField, gameField[0, 0], 8);
            Assert.AreEqual(value,true);
        }
        [Test]
        public void WinLooseCondition2()
        {
            _gameController.NumberOfVisitedNodes = 1;
            _gameController.FillGameField(gameField);
            gameField[0, 0].nodeStatus = Blazor.Helpers.NodeStatus.Bomb;
            bool value = _gameController.IsGameOver(gameField, gameField[0, 0],5);
            Assert.AreEqual(value, true);
        }
    }
}