using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSL
{
    /// <summary>
    /// Timer class.
    /// </summary>
    public class Timer
    {
        /// <summary>
        /// Current time value for timer.
        /// </summary>
        public long t;

        /// <summary>
        /// Defines if timer is turned on or off.
        /// </summary>
        private bool turnedOn;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Timer()
        {
            turnedOn = false;
            // Add current timer to timers list in Time class.
            Time.Time.AddTimer(this);
        }

        /// <summary>
        /// Overloaded constructor that takes time for argument.
        /// </summary>
        /// <param name="time">Initial value of time.</param>
        public Timer(long time)
        {
            t = time;
            turnedOn = false;
            Time.Time.AddTimer(this);
        }

        /// <summary>
        /// Setter for field t.
        /// </summary>
        public long T
        {
            get
            {
                return t;
            }
            set
            {
                t = value;
            }
        }

        /// <summary>
        /// Sets timer on.
        /// </summary>
        public void SetOn()
        {
            turnedOn = true;
        }

        /// <summary>
        /// Sets timer off.
        /// </summary>
        public void SetOff()
        {
            turnedOn = false;
        }


        /// <summary>
        /// Checks if current timer is running.
        /// </summary>
        /// <returns>True if yes.</returns>
        internal bool IsOn()
        {
            return turnedOn;
        }

        /// <summary>
        /// Checks if timer is set on and its value is zero (now).
        /// </summary>
        /// <returns>True if event occurs.</returns>
        public bool Now()
        {
            if ((t == 0) && (turnedOn))
                return true;
            else
                return false;
        }
    }
}
