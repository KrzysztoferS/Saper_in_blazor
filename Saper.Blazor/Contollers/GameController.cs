using Saper.Blazor.Abstraction;
using Saper.Blazor.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saper.Blazor.Contollers
{

    public class GameController
    {
        public int NumberOfVisitedNodes { get; set; }
        public int NumberOfClicks { get; set; }
        public bool PlayerWin { get; set; }

        private INodeFinder nodeFinder;


        public GameController()
        {
            nodeFinder = new NodeFinder();
            StartGame();
        }

        public void StartGame()
        {
            NumberOfClicks = 0;
            NumberOfVisitedNodes = 0;
        }

        public GameNode[,] FillGameField(GameNode[,] gameField)
        {
            int rows = gameField.GetLength(0);
            int columns = gameField.GetLength(1);

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < columns; col++)
                {
                    gameField[row, col] = new GameNode(row, col, GetAdjacentNodes(gameField,row, col));
                }
            }
            return gameField;
        }

        public List<Node> GetAdjacentNodes(GameNode[,] gameField, int row, int col)
        {
            List<Node> adjacentNodes = new List<Node>();
            int rows = gameField.GetLength(0);
            int columns = gameField.GetLength(1);

            if ((row - 1) >= 0 && (col - 1) >= 0)
            {
                adjacentNodes.Add(new Node((row - 1), (col - 1)));
            }
            if ((row - 1) >= 0)
            {
                adjacentNodes.Add(new Node((row - 1), col));
            }
            if ((row + 1) < rows && (col + 1) < columns)
            {
                adjacentNodes.Add(new Node(row + 1, col + 1));
            }
            if ((col - 1) >= 0)
            {
                adjacentNodes.Add(new Node(row, col - 1));
            }
            if ((col + 1) < columns)
            {
                adjacentNodes.Add(new Node(row, col + 1));
            }
            if ((row + 1) < rows && (col - 1) >= 0)
            {
                adjacentNodes.Add(new Node(row + 1, col - 1));
            }
            if ((row + 1) < rows)
            {
                adjacentNodes.Add(new Node(row + 1, col));
            }
            if ((row - 1) >=0 && (col + 1) < columns)
            {
                adjacentNodes.Add(new Node(row - 1, col + 1));
            }
            return adjacentNodes;
        
        }

        public List<GameNode> GetEmptyNodes(GameNode[,] gameField)
        {
            List<GameNode> emptyNodes = new List<GameNode>();

            foreach (var node in gameField)
            {
                if (node.nodeStatus == Helpers.NodeStatus.Empty)
                {
                    emptyNodes.Add(node);
                }
            }

            return emptyNodes;
        }

        public void SetBombsOnField(GameNode[,] gameField, int numberOfBombs)
        {
            List<GameNode> emptyNodes = GetEmptyNodes(gameField);

            if (emptyNodes != null && emptyNodes.Count > numberOfBombs)
            {
                Random rnd = new Random();
                for (int i = 0; i < numberOfBombs; i++)
                {
                    int node = rnd.Next(0, emptyNodes.Count);
                    emptyNodes[node].nodeStatus = Helpers.NodeStatus.Bomb;
                    emptyNodes.RemoveAt(node);
                }
            }
            else return;
        }

       
        public void SetNodesStatus(GameNode[,] gameField)
        {
            foreach(var node in GetEmptyNodes(gameField))
            {
                int adjacentBombs = 0;
                foreach(var adjacentNode in node.adjacentNodes)
                {
                    if (gameField[adjacentNode.X, adjacentNode.Y].nodeStatus == Helpers.NodeStatus.Bomb) adjacentBombs++;
                }
                node.nodeStatus = (Helpers.NodeStatus)adjacentBombs;
            }
        }

        public void SetNodeAsVisited(GameNode gameNode)
        {
            if (!gameNode.Visited)
            {
                gameNode.Visited = true;
                NumberOfVisitedNodes+=1;
            }
        }

        public void OpenNodes(GameNode[,] gameField, GameNode gameNode)
        {
            List<GameNode> nodesToOpen = nodeFinder.FindNodes(gameField, gameNode);

            foreach(var node in nodesToOpen)
            {
                SetNodeAsVisited(node);
                NumberOfVisitedNodes++;
            }
        }

        public void SetFlag(GameNode gameNode)
        {
            if (gameNode.IsFlagged == false)
            {
                gameNode.IsFlagged = true;
            }
            else gameNode.IsFlagged = false;
        }

        public bool IsGameOver(GameNode[,] gameField, GameNode gameNode, int numberOfBombs)
        {
            int requiredNumberOfVisitedNodesToWin = gameField.Length-numberOfBombs;
            int number = GetNumberOfVisitedNodes(gameField);

            if (gameNode.nodeStatus == Helpers.NodeStatus.Bomb)
            {
                PlayerWin = false;
                return true;
            } else if(number == requiredNumberOfVisitedNodesToWin)
            {
                PlayerWin = true;
                return true;
            }
            else return false;
            
        }

        public void Win(GameNode[,] gameField)
        {
            foreach (var node in gameField)
            {
                if (node.Visited) node.nodeStatus = Helpers.NodeStatus.Win;
            }
        }

        public void Loose(GameNode[,] gameField)
        {
            foreach (var node in gameField)
            {
                if (node.Visited) node.nodeStatus = Helpers.NodeStatus.Loose;
            }
        }

        public int GetNumberOfVisitedNodes(GameNode[,] gameField)
        {
            int number = 0;
            foreach(var node in gameField)
            {
                if (node.Visited) number++;
            }
            NumberOfVisitedNodes = number;
            return number;
        }

        public void Restart(GameNode[,] gameField)
        {
            foreach(var node in gameField)
            {
                node.Visited = false;
                NumberOfVisitedNodes = 0;
            }
        }

    }

}
