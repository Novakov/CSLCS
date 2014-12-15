using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSL.Time
{
    /// <summary>
    /// Static class useg for time simulation.
    /// </summary>
    public static class Time
    {
        /// <summary>
        /// Every timer object with time to measure.
        /// </summary>
        private static List<Timer> timers = new List<Timer>();

        /// <summary>
        /// Add timer to timers list.
        /// </summary>
        /// <param name="timerToAdd">Timer to add.</param>
        internal static void AddTimer(Timer timerToAdd)
        {
            timers.Add(timerToAdd);
        }

        /// <summary>
        /// Simulates time flow.
        /// </summary>
        public static void TimeFlow()
        {
            long smallestTimer = long.MaxValue;
            int currentTimerIndex = -1;
            
            //Check for timer with smallest time value
            foreach (Timer timer in timers)
            {
                if (timer.IsOn() && (timer.T < smallestTimer))
                {
                    smallestTimer = timer.T;
                    currentTimerIndex = timers.IndexOf(timer);
                }
            }

            //Timer found - decrease all other timers (that are turned on)
            if (currentTimerIndex != -1)
            {
                foreach (Timer timer in timers)
                {
                    timer.T -= smallestTimer;
                }
            }            
        }
    }
}
