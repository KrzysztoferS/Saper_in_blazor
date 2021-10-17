using Saper.Blazor.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saper.Blazor.Contollers
{

    public class GameController
    {
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
    }

}
