using Saper.Blazor.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saper.Blazor.Contollers
{
    public class TimeController 
    {
        private bool timeIsOn;

        public delegate void TimeChangeHandler(object source, EventArgs args);
        public event TimeChangeHandler TimeChanged;

        protected virtual void OnTimeChanged()
        {
            if (TimeChanged != null)
            {
                TimeChanged(this, EventArgs.Empty);
            }
        }

        async public void StartTimeChange()
        {
            if (timeIsOn) return;
            timeIsOn = true;
            while (timeIsOn)
            {
                for(int i=0; i<10; i++)
                {
                    if (timeIsOn == false) return;
                    await Task.Delay(100);
                }
                OnTimeChanged();
            }
        }

        public void StopTimeChange()
        {
            timeIsOn = false;
        }
    }
}

