using Saper.Blazor.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saper.Blazor.Contollers
{
    public class TimeController : ITimer
    {
        public float Time { get; set; }
        public bool TimeIsOn { get; set; }

        public TimeController()
        {
            Time = 0;
            TimeIsOn = false;
        }

        public async void StartTime()
        {
            TimeIsOn = true;
            Time = 0;
            while (TimeIsOn)
            {
                for (int i = 0; i < 10; i++)
                {
                    await Task.Delay(100);
                }
                Time++;
                //Tutaj rzucic evetem ze minela sekunda
            }
        }

        public float StopTime()
        {
            TimeIsOn = false;
            return Time;
        }
    }
}

