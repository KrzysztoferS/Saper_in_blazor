using Saper.Blazor.Abstraction;
using Saper.Blazor.Objects;
using Saper.Blazor.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saper.Blazor.Contollers
{
    public class NodeFinder : INodeFinder
    {
        private int iteration = 0;

        public List<GameNode> FindNodes(GameNode[,] gameField, GameNode gameNode)
        {
            iteration++;
            List<GameNode> nodesToOpen = new List<GameNode>();
            //lista zawiera wezly ktore nalezy przeszukac
            List<GameNode> nodesToCheck = new List<GameNode>();
            var currentNode = gameNode;
            currentNode.Visited = true;
            if (currentNode.nodeStatus != Helpers.NodeStatus.Empty)
            {
                return nodesToOpen ;
            }
            //przeszukuje sasiadow danego wezla i jesli nie sa otwarte to dodaje do listy
            foreach (var i in currentNode.adjacentNodes)
            {
                if (gameField[i.X,i.Y].Visited == false)
                {
                    nodesToCheck.Add(gameField[i.X,i.Y]);
                    gameField[i.X, i.Y].AnimationDelay = iteration;
                    gameField[i.X,i.Y].Visited = true;
                    //numberOfVisitedNodes++;
                    if (gameField[i.X,i.Y].IsFlagged == false && gameField[i.X,i.Y].nodeStatus == Helpers.NodeStatus.Empty)
                    {
                        FindNodes(gameField, gameField[i.X, i.Y]);
                    }
                }

            }

            return nodesToOpen;
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
            if ((row - 1) >= 0 && (col + 1) < columns)
            {
                adjacentNodes.Add(new Node(row - 1, col + 1));
            }
            return adjacentNodes;

        }
    }
}
