using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSL.Statistics
{
    /// <summary>
    /// Histogram class.
    /// </summary>
    class Histogram
    {
        /// <summary>
        /// List of histogram partitions.
        /// </summary>
        List<HistogramEntry> partitions;

        /// <summary>
        /// Constructor with count and partition ranges.
        /// </summary>
        /// <param name="count"></param>
        /// <param name="partitionsList"></param>
        public Histogram(long count, params long[] partitionsList)
        {
            partitions = new List<HistogramEntry>();

            for (int i = 0; i < count; i++)
            {
                HistogramEntry entry = new HistogramEntry(partitionsList[i], 0);
                partitions.Add(entry);
            }
            // Add last partition from last border to unfinity (or last possible value).
            HistogramEntry lastEntry = new HistogramEntry(long.MaxValue, 0);
            partitions.Add(lastEntry);
        }

        /// <summary>
        /// Adds record to histogram.
        /// </summary>
        /// <param name="valueToAdd"></param>
        public void Add(long valueToAdd)
        {
            for (int i=0; i< partitions.Count; i++)
            {
                if ((valueToAdd >= partitions[i].Border) && (valueToAdd < partitions[i+1].Border))
                {
                    partitions[i].amount++;
                    break;                
                }
            }
        }

        /// <summary>
        /// Removes all records from histogram.
        /// </summary>
        public void Clear()
        {
            for (int i = 0; i < partitions.Count; i++ )
            {
                partitions[i].amount = 0;
            }
        }

        /// <summary>
        /// Returns amount of records in particular partition.
        /// </summary>
        /// <param name="value">Value that specifies partition.</param>
        /// <returns>Number of records.</returns>
        public ulong Yield(ulong value)
        {
            long valueToMatch = (long)value;
            for (int i = 0; i < partitions.Count; i++)
            {
                if ((valueToMatch >= partitions[i].Border) && (valueToMatch < partitions[i + 1].Border))
                {
                    return partitions[i].amount;                    
                }
            }

            return 0;            
        }

        /// <summary>
        /// HistogramEntry inner class.
        /// </summary>
        class HistogramEntry
        {
            /// <summary>
            /// Current partition border.
            /// </summary>
            long border;

            /// <summary>
            /// Readonly property that gets current partition border.
            /// </summary>
            public long Border
            {
                get 
                { 
                    return border; 
                }                
            }

            /// <summary>
            /// Amount of entries.
            /// </summary>
            public uint amount;

            /// <summary>
            /// HistogramEntry constructor.
            /// </summary>
            /// <param name="_border">Border of current entry.</param>
            /// <param name="_amount">Amount of records in current partition.</param>
            public HistogramEntry(long _border, uint _amount)
            {
                border = _border;
                amount = _amount;
            }
        }
    }


}
