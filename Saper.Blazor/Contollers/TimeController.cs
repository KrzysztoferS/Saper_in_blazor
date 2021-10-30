using Saper.Blazor.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saper.Blazor.Contollers
{
    public class TimeController : ITimer
    {
        public float Time { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool TimeIsOn { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void StartTime()
        {
            throw new NotImplementedException();
        }

        public float StopTime()
        {
            throw new NotImplementedException();
        }
    }
}
