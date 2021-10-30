using Saper.Blazor.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saper.Blazor.Objects
{
    public class GameNode:Node 
    {
        public bool Visited { get; set; } = false;
        public bool IsFlagged { get; set; }
        public List<Node> adjacentNodes;
        public NodeStatus nodeStatus;
        public float AnimationDelay { get; set; }

        public GameNode(int x, int y, List<Node> nodes, NodeStatus _nodeStatus=NodeStatus.Empty, bool visited=false):base( x, y)
        {
            adjacentNodes = nodes;
            nodeStatus = _nodeStatus;
            Visited = visited;
            AnimationDelay = 0;
        }

    }
}
