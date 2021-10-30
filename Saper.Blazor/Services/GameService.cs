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
        readonly int numberOfRows;
        readonly int numberOfColumns;
        readonly int numberOfBombs;

        public GameNode[,] GameField { get; private set; }

        private GameController gameController;
        private bool gameOver;

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
                if (GameField[row, col].nodeStatus == Helpers.NodeStatus.Bomb)
                {
                    Loose();
                }
            }
        }

        public void OnRightClick(int row, int col)
        {
            gameController.SetFlag(GameField[row, col]);
        }

        public bool IsGameInProgress()
        {
            if(((numberOfColumns*numberOfRows)-numberOfBombs)==gameController.NumberOfVisitedNodes)
            {
                Win();
                return false;
            }
            return false;
        }

        public void Win()
        {
            //TODO
            gameOver = true;
        }

        public void Loose()
        {
            //TODO
            gameOver = true;
        }
    }
}
