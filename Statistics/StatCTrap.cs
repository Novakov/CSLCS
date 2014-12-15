using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSL.Statistics
{
    /// <summary>
    /// Class that represents statistic calculated with trapez method.
    /// </summary>
    public class StatCTrap : StatC
    {
        /// <summary>
        /// Gets average value for current statistic.
        /// </summary>
        /// <param name="average">Referenced average value.</param>
        public override void GetStat(ref double average)
        {        
            average = GetAverage(long.MaxValue);
        }

        /// <summary>
        /// Gets average value for current statistic.
        /// </summary>
        /// <param name="average">Referenced average value.</param>
        public override void GetStat(ref double average, long time)
        {
            average = GetAverage(time);
        }

        /// <summary>
        /// Counts average value.
        /// </summary>
        /// <returns>Calculated average value.</returns>
        double GetAverage(long time)
        {
            int count = records.Count;
            long sum = 0;
            base.actualCount = 0;

            // Dla każdego przedziału obliczamy pole trapezu.
            for (int i = 0; i < count - 1; i++)
            {
                if (records[i].Item2 <= time)
                {
                    long current = ((records[i].Item1 + records[i + 1].Item1) / 2) * (records[i + 1].Item2 - records[i].Item2);
                    sum += current;
                    actualCount++;
                }
                else
                {
                    break;
                }                
            }

            double average = (double)sum / (double)records[actualCount].Item2;
            return average;
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
    }
}
