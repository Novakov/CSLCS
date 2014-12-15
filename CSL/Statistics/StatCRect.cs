using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSL.Statistics
{
    /// <summary>
    /// Class that represents statistic calculated with rectangle method.
    /// </summary>
    public class StatCRect : StatC
    {
        /// <summary>
        /// Gets average of current statistic.
        /// </summary>
        /// <param name="average">Reference average parameter.</param>
        public override void GetStat(ref double average)
        {
            average = GetAverage(long.MaxValue);
        }

        /// <summary>
        /// Gets average for specified time.
        /// </summary>
        /// <param name="average">Referenced average parameter.</param>
        /// <param name="time">Time range.</param>
        public override void GetStat(ref double average, long time)
        {
            average = GetAverage(time);
        }

        /// <summary>
        /// gets average and deviation value of current statistic.
        /// </summary>
        /// <param name="average">Reference average parameter.</param>
        /// <param name="deviation">Reference deviation parameter.</param>
        public override void GetStat(ref double average, ref double deviation)
        {
            average = GetAverage(long.MaxValue);
            deviation = base.GetDeviation(average, int.MaxValue);
        }

        /// <summary>
        /// gets average and deviation value of current statistic.
        /// </summary>
        /// <param name="average">Reference average parameter.</param>
        /// <param name="deviation">Reference deviation parameter.</param>
        public override void GetStat(ref double average, ref double deviation, long time)
        {
            average = GetAverage(time);
            deviation = base.GetDeviation(average, base.actualCount);
        }

        /// <summary>
        /// Gets average value of statistic.
        /// </summary>
        /// <returns></returns>
        double GetAverage(long time)
        {
            int count = records.Count;
            long sum = 0;
            base.actualCount = 0;

            // Dla każdego przedziału obliczamy pole prostokąta.
            for (int i = 0; i < count - 1; i++)
            {
                if (records[i].Item2 <= time)
                {
                    long current = records[i].Item1 * (records[i + 1].Item2 - records[i].Item2);
                    sum += current;
                    actualCount++;
                }
                else
                {
                    break;
                }                
            }

            //double average = (double)sum / (double)records[records.Count - 1].Item2;                                    
            double average = (double)sum / (double)records[actualCount].Item2;
            return average;
        }
    }
}
