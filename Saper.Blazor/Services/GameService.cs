using Saper.Blazor.Abstraction;
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
        private int timeLimit;
        private int timeLeft;

        public GameNode[,] GameField { get; private set; }

        private GameController gameController;
        private TimeController timeController;
        public bool gameOver;

        public GameService(int rows, int columns, int bombs, int timeLimit=99)
        {
            gameController = new GameController();
            timeController = new TimeController();
            numberOfRows = rows;
            numberOfColumns = columns;
            GameField = new GameNode[numberOfRows, numberOfColumns];
            this.timeLimit = timeLimit;
            //FillGameField(numberOfRows, numberOfColumns);

            numberOfBombs = bombs;
            //SetBombsOnField();
            timeController.TimeChanged += OnTimeChanged;
            //SetNodesStatus();
            StartGame();
        }
        
        public void StartGame()
        {
            gameController.FillGameField(GameField);
            gameController.SetBombsOnField(GameField, numberOfBombs);
            gameController.SetNodesStatus(GameField);
            gameOver = false;
            timeLeft = timeLimit;
            timeController.StartTimeChange();
            
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
                if (gameOver) timeController.StopTimeChange();
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
            timeLeft = timeLimit;
            timeController.StartTimeChange();
            gameOver = false;
        }

        public void OnTimeChanged(object source, EventArgs e)
        {
            timeLeft--;
            if (timeLeft == 0) GameOver();
            OnParamtersChange();
        }

        public delegate void ParametersChangeHandler(object source, EventArgs args);
        public event ParametersChangeHandler ParametersChanged;

        protected virtual void OnParamtersChange()
        {
            if (ParametersChanged != null)
            {
                ParametersChanged(this, EventArgs.Empty);
            }
        }

        public int GetTime()
        {
            return timeLeft;
        }

        private void GameOver()
        {
            gameOver = true;
            timeController.StopTimeChange();
        }
    }
}
