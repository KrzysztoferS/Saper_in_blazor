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

        public GameService(int rows, int columns, int bombs)
        {
            numberOfRows = rows;
            numberOfColumns = columns;
            GameField = new GameNode[numberOfRows, numberOfColumns];
            FillGameField(numberOfRows, numberOfColumns);

            numberOfBombs = bombs;
            SetBombsOnField();

            SetNodesStatus();
        }
        //fills game field with gmanode objects assigning them row and col numbers and status
        private void FillGameField(int rows,int columns)
        {
            for(int row=0; row<rows; row++)
            {
                for(int col=0; col<columns; col++)
                {
                    GameField[row, col] = new GameNode(row, col, GetAdjacentNodes(row, col));
                }
            }
        }

        //returns row and col of all adjacent nodes
        private List<Node> GetAdjacentNodes(int row, int col)
        {
            List<Node> adjacentNodes = new List<Node>();

            if(row-1>0 && col-1 > 0){
                adjacentNodes.Add(new Node(row - 1, col - 1));
            }
            if (row - 1 > 0)
            {
                adjacentNodes.Add(new Node(row - 1, col));
            }
            if(row+1<numberOfRows && col + 1 < numberOfColumns)
            {
                adjacentNodes.Add(new Node(row + 1, col + 1));
            }
            if (col - 1 > 0)
            {
                adjacentNodes.Add(new Node(row, col - 1));
            }
            if (col + 1 < numberOfColumns)
            {
                adjacentNodes.Add(new Node(row, col + 1));
            }
            if(row+1<numberOfRows && col - 1 > 0)
            {
                adjacentNodes.Add(new Node(row + 1, col - 1));
            }
            if (row + 1 < numberOfRows)
            {
                adjacentNodes.Add(new Node(row + 1, col));
            }
            if(row+1<numberOfRows && col + 1 < numberOfColumns)
            {
                adjacentNodes.Add(new Node(row + 1, col + 1));
            }
            return adjacentNodes;
        }

        private void SetBombsOnField()
        {
            List<GameNode> emptyNodes = GetEmptyNodes();

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

        public void SetNodesStatus()
        {
            foreach(var node in GameField)
            {
                if (node.nodeStatus == Helpers.NodeStatus.Empty)
                {
                    int adjacentBombs = 0;
                    foreach(var adjNode in node.adjacentNodes)
                    {
                        if (GameField[adjNode.X, adjNode.Y].nodeStatus == Helpers.NodeStatus.Bomb) adjacentBombs++;
                    }
                    node.nodeStatus = (Helpers.NodeStatus)adjacentBombs;
                }
            }
        }

        public List<GameNode> GetEmptyNodes()
        {
            List<GameNode> emptyNodes = new List<GameNode>();
            
                foreach (var node in GameField)
                {
                    if(node.nodeStatus == Helpers.NodeStatus.Empty)
                    {
                        emptyNodes.Add(node); 
                    }
                }
            
            return emptyNodes;
        }

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
    }
}
