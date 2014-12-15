using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSL.Statistics
{
    /// <summary>
    /// Represents discrete statistic.
    /// </summary>
    public class StatD
    {
        /// <summary>
        /// List of all statistic records.
        /// </summary>
        internal List<long> records;

        /// <summary>
        /// Constructor that initialises new list of records.
        /// </summary>
        public StatD()
        {
            records = new List<long>();
        }

        /// <summary>
        /// Adds long record to statistic.
        /// </summary>
        /// <param name="recordToAdd">Record to add.</param>
        public void Add(long recordToAdd)
        {
            records.Add(recordToAdd);
        }

        /// <summary>
        /// Adds int record to statistic.
        /// </summary>
        /// <param name="recordToAdd">Record to add.</param>
        public void Add(int recordToAdd)
        {
            records.Add((long)recordToAdd);
        }
        
        /// <summary>
        /// Gets double average value of all records.
        /// </summary>
        /// <param name="average">Referenced average value.</param>
        public void GetStat(ref double average)
        {
            double avg = 0;
            avg = records.Average();
            average = avg;
        }

        /// <summary>
        /// Gets average value and standard deviation of all records.
        /// </summary>
        /// <param name="average">Referenced average value.</param>
        /// <param name="deviation">Referenced standard deviation value.</param>
        public void GetStat(ref double average, ref double deviation)
        {
            double avg = 0;
            avg = records.Average();
            average = avg;

            deviation = GetDeviation(avg);
        }

        /// <summary>
        /// Count deviation from average for current statistic.
        /// </summary>
        /// <param name="average">Average value.</param>
        /// <returns>Deviation value.</returns>
        double GetDeviation(double average)
        {
            double sum = 0;
            double current = 0;

            foreach (long value in records)
            {
                current = Math.Pow(value - average, 2);
                sum += current;
            }

            return Math.Sqrt(sum / records.Count);
        }

        /// <summary>
        /// REmoves all records from statistic.
        /// </summary>
        public void Clear()
        {
            records.Clear();
        }
    }
}
