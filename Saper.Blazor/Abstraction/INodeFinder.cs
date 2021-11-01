using Saper.Blazor.Objects;
using Saper.Blazor.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saper.Blazor.Abstraction
{
    public interface INodeFinder
    {
        public List<GameNode> FindNodes(GameNode[,] gameField, GameNode gameNode);
    }
}
