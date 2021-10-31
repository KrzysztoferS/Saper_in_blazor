using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saper.Blazor.Models
{
    public class GameLevel
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        public int Bombs { get; set; }
    }
}
