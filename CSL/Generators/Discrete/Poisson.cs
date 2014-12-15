using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.Distributions;
using System.Threading;

namespace CSL.Generators.Discrete
{
    /// <summary>
    /// Poisson distribution.
    /// It uses poisson distribution from MathNet library.
    /// </summary>
    public class Poisson
    {
        internal MathNet.Numerics.Distributions.PoissonDistribution poisson;

        /// <summary>
        /// Constructor with parameter.
        /// </summary>
        /// <param name="m">Lambda double parameter.</param>
        Poisson(double m)
        {
            Thread.Sleep(20);
            poisson.SetDistributionParameters(m);            
        }

        /// <summary>
        /// Gets another random number.
        /// </summary>
        /// <returns>Random integer number.</returns>
        public int Get()
        {
            return poisson.NextInt32();
        }

        /// <summary>
        /// Initialises new distribution.
        /// </summary>
        /// <param name="m"></param>
        public void Init(double m)
        {
            poisson.SetDistributionParameters(m);
        }
    }
}
