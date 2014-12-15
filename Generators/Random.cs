using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.Distributions;
using System.Threading;

namespace CSL.Generators
{
    /// <summary>
    /// Random generator class.
    /// It uses random distribution from MathNet library.
    /// </summary>
    public class RandomGenerator
    {
        MathNet.Numerics.Distributions.NormalDistribution random;
        
        /// <summary>
        /// Mean random distribution value.
        /// </summary>
        double mean;

        /// <summary>
        /// Deviation value.
        /// </summary>
        double sigma;

        /// <summary>
        /// Constructor with mean value.
        /// </summary>
        /// <param name="i">Input mean value parameter.</param>
        public RandomGenerator(ulong i)
        {
            mean = (double)i;
            sigma = mean - 1;
            Thread.Sleep(20);
            random = new NormalDistribution(mean, sigma);            
        }

        /// <summary>
        /// Gets next distribution value.
        /// </summary>
        /// <returns>Returns distribution value.</returns>
        public long Get()
        {
            long valueToReturn = 0;
            double nextDouble = random.NextDouble();
            valueToReturn = (long)nextDouble;
            return valueToReturn;
        }

        /// <summary>
        /// Initialises new distribution.
        /// </summary>
        /// <param name="i">Input mean value parameter.</param>
        public void Init(ulong i)
        {
            mean = (double)i;
            sigma = mean - 1;
        }
    }
}
