using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSL.Statistics.HelperClasses;

namespace CSL.Statistics
{
    /// <summary>
    /// Histogram class.
    /// </summary>
    public class Hist
    {
        //internal List<HistogramRecord> records;
        uint m_intervalsAmount;
        long m_firstInterval;
        long m_intervalSize;
        
        /// <summary>
        /// List, that contains number of each statistic added.
        /// </summary>
        List<uint> histogram;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="nrOfIntervals">Number of intervals.</param>
        /// <param name="firstInterval">Border of first interval.</param>
        /// <param name="intervalSize">Size of each one interval.</param>
        public Hist(uint nrOfIntervals, long firstInterval, long intervalSize)
        {
            m_intervalsAmount = nrOfIntervals;
            m_firstInterval = firstInterval;
            m_intervalSize = intervalSize;
            
            histogram = new List<uint>();
           
            for (int i = 0; i < m_intervalsAmount; i++)
            {
                histogram.Add(0);
            }
        }

        /// <summary>
        /// Add observation to current histogram.
        /// </summary>
        /// <param name="valueToAdd">Value of observation to add.</param>
        public void Add(long valueToAdd)
        {            
            long currentLeftBorder = 0;
            long currentRightBorder = 0;

            if (valueToAdd == 0)
            {
                histogram[0]++;
            }
            else
            {
                for (int i = 0; i < m_intervalsAmount; i++)
                {
                    // Set current interval borders.
                    if ((i == 0) && (m_firstInterval == 0))
                    {
                        currentRightBorder = m_intervalSize;
                    }
                    else
                    {
                        currentRightBorder = m_firstInterval + (i+1) * m_intervalSize;
                    }

                    currentLeftBorder = currentRightBorder - m_intervalSize;

                    if ((valueToAdd > currentLeftBorder) && (valueToAdd <= currentRightBorder))
                    {
                        histogram[i]++;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Clears all histogram entries.
        /// </summary>
        public void Clear()
        {
            for (int i = 0; i < m_intervalsAmount; i++)
            {
                histogram[i] = 0;
            }
                 
        }

        /// <summary>
        /// Returns vaue of observations for interval.
        /// </summary>
        /// <param name="value">Number of interval.</param>
        /// <returns>Number of observations.</returns>
        public ulong Yield(long value)
        {            
            long currentLeftBorder = 0;
            long currentRightBorder = 0;

            if (value == 0)
            {
                histogram[0]++;
            }
            else
            {
                for (int i = 0; i < m_intervalsAmount; i++)
                {
                    // Set current interval borders.
                    if ((i == 0) && (m_firstInterval == 0))
                    {
                        currentRightBorder = m_intervalSize;
                    }
                    else
                    {
                        currentRightBorder = m_firstInterval + (i + 1) * m_intervalSize;
                    }

                    currentLeftBorder = currentRightBorder - m_intervalSize;

                    if ((value > currentLeftBorder) && (value <= currentRightBorder))
                    {
                        return histogram[i];                        
                    }
                }
            }

            return 0;
        }

        /// <summary>
        /// Returns formatted string output with histogram results.
        /// </summary>
        /// <returns></returns>
        public string Out()
        {
            StringBuilder stringBuilder = new StringBuilder();
            long sumOfAll = CalculateSum();

            for (int i = 0; i < m_intervalsAmount; i++)
            {
                long currentIntervalBegin = i * m_intervalSize + m_firstInterval;
                long currentIntervalEnd = currentIntervalBegin + m_intervalSize;
                double currentPercent = (double)histogram[i] / (double)sumOfAll;

                stringBuilder.AppendLine("( " + currentIntervalBegin + ", " + currentIntervalEnd + ">      " + 
                    histogram[i].ToString() + "        " + currentPercent*100 + "%");
            }

                return stringBuilder.ToString();
        }

        /// <summary>
        /// Calculates sum of all records.
        /// </summary>
        /// <returns></returns>
        long CalculateSum()
        {
            long sum = 0;

            for (int i = 0; i < m_intervalsAmount; i++)
            {
                sum += histogram[i];
            }

            return sum;
        }
    }
}
