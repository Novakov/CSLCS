using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSL.Statistics.HelperClasses;

namespace CSL.Statistics
{
    /// <summary>
    /// Base class for cintinuous statistics.
    /// </summary>
    public class StatC
    {
        //List<StatCRecord> records;
        protected List<Tuple<long, long>> records;
        protected int actualCount;

        public StatC()
        {
            //records = new List<StatCRecord>();
            records = new List<Tuple<long, long>>();
            actualCount = -1;
        }

        /// <summary>
        /// Add observation to current statistic,
        /// </summary>
        /// <param name="x">Value of observation.</param>
        /// <param name="t">Time of observation.</param>
        public void Add(long x, long t)
        {
            //StatCRecord recordToAdd = new StatCRecord(x, t);
            var recordToAdd = Tuple.Create(x, t);
            records.Add(recordToAdd);
        }

        /// <summary>
        /// Gets average for current statistic.
        /// </summary>
        /// <param name="average">Reference average value.</param>
        public virtual void GetStat(ref double average) {}

        /// <summary>
        /// Gets average for statistic in specified time range.
        /// </summary>
        /// <param name="average"></param>
        /// <param name="time"></param>
        public virtual void GetStat(ref double average, long time) {}

        /// <summary>
        /// Gets average and deviation for current statistic.
        /// </summary>
        /// <param name="average">Reference average value.</param>
        /// <param name="deviation">Reference deviation value.</param>
        public virtual void GetStat(ref double average, ref double deviation) { }

        /// <summary>
        /// Gets average and deviation for current statistic.
        /// </summary>
        /// <param name="average">Reference average value.</param>
        /// <param name="deviation">Reference deviation value.</param>
        public virtual void GetStat(ref double average, ref double deviation, long time) { }   

        /// <summary>
        /// Removes all observations.
        /// </summary>
        public void Clear()
        {
            records.Clear();
        }
        
        /// <summary>
        /// Returns deviation from average value.
        /// </summary>
        /// <param name="avg">Average value.</param>
        /// <returns>Deviation value.</returns>
        protected double GetDeviation(double avg, int count)
        {
            double sum = 0;
            double current = 0;

            for (int i = 0; i < records.Count - 1; i++)
            {
                if (i <= count)
                {
                    current = Math.Pow(records[i].Item1 - avg, 2);
                    sum += current;
                }
                else
                {
                    break;
                }                
            }

            if (count < records.Count)
            {
                return Math.Sqrt(sum / count);
            }
            else
            {
                return Math.Sqrt(sum / records.Count);
            }            
        }        
    }
}
