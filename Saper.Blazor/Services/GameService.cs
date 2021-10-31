using Saper.Blazor.Contollers;
using Saper.Blazor.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saper.Blazor.Services
{
    public class GameService
    {
        private int numberOfRows;
        private int numberOfColumns;
        private int numberOfBombs;

        public GameNode[,] GameField { get; private set; }

        private GameController gameController;
        public bool gameOver;

        public GameService(int rows, int columns, int bombs)
        {
            gameController = new GameController();
            numberOfRows = rows;
            numberOfColumns = columns;
            GameField = new GameNode[numberOfRows, numberOfColumns];
            //FillGameField(numberOfRows, numberOfColumns);

            numberOfBombs = bombs;
            //SetBombsOnField();

            //SetNodesStatus();
            StartGame();
        }
        
        public void StartGame()
        {
            gameController.FillGameField(GameField);
            gameController.SetBombsOnField(GameField, numberOfBombs);
            gameController.SetNodesStatus(GameField);
            gameOver = false;
        }
       

        //returns row and col of all adjacent nodes
        
        public Helpers.NodeStatus[,] GetGameField()
        {
            Helpers.NodeStatus[,] nodesStatus = new Helpers.NodeStatus[numberOfRows, numberOfColumns];

            for(int row=0; row < numberOfRows; row++)
            {
                for(int col=0; col < numberOfColumns; col++)
                {
                    nodesStatus[row,col]=GameField[row,col].nodeStatus;
                }
            }

            return nodesStatus;
        }

        public void OnClick(int row, int col)
        {
            //gameController.SetNodeAsVisited(GameField[row, col]);
            if (!gameOver)
            {
                gameController.OpenNodes(GameField, GameField[row, col]);
                gameOver = gameController.IsGameOver(GameField, GameField[row, col], numberOfBombs);
            }
        }

        public void OnRightClick(int row, int col)
        {
            gameController.SetFlag(GameField[row, col]);
        }

        public bool GameResult()
        {
            return gameController.PlayerWin;
        }

        public int VistedNodes()
        {
            return gameController.NumberOfVisitedNodes;
        }

        public void Restart()
        {
            gameController.Restart(GameField);
            gameOver = false;
        }
    }
}
