using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.Distributions;
using System.Threading;

namespace CSL.Generators.Discrete
{
    /// <summary>
    /// NegExp distribution.
    /// It uses exponential distribution from MathNet library.
    /// </summary>
    public class NegExp
    {
        MathNet.Numerics.Distributions.ExponentialDistribution negExp;
        
        ulong meanValue;

        /// <summary>
        /// Constructor with mean value as parameter.
        /// </summary>
        /// <param name="m">Double mean value.</param>
        public NegExp(ulong mean)
        {
            meanValue = mean;                                     
            Thread.Sleep(20);
            negExp = new ExponentialDistribution();                        
        }

        /// <summary>
        /// Gets next distribution value.
        /// </summary>
        /// <returns>Returns generated value.</returns>
        public long Get()
        {
            long longToReturn = 0;
            longToReturn = (long)(negExp.NextDouble() * meanValue);
            return longToReturn;
        }

        /// <summary>
        /// Initialises new distribution.
        /// </summary>
        /// <param name="mean">Mean value for distribution.</param>
        public void Init(ulong mean)
        {
            meanValue = mean;
        }
    }
}
