using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saper.Blazor.Abstraction
{
    public interface ITimer
    {
        public float Time { get; set; }
        public bool TimeIsOn { get; set; }

        public void StartTime();
        public float StopTime();
    }
}
