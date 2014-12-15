using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CSL.Generators.Discrete
{
    /// <summary>
    /// Class that represents Deviate discrete distribution.
    /// Uses NormalDistribution from MathNet library.
    /// </summary>
    public class Deviate
    {
        private MathNet.Numerics.Distributions.NormalDistribution deviate;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="mean">Mean value for generated distrubution.</param>
        /// <param name="sigma">Deviation from mean value.</param>
        public Deviate(double mean, double sigma)
        {
            Thread.Sleep(20);
            this.deviate = new MathNet.Numerics.Distributions.NormalDistribution(mean, sigma);
        }

        /// <summary>
        /// Gets next generated value of distribution.
        /// </summary>
        /// <returns>Returns distribution value.</returns>
        public long Get()
        {
            return (long)deviate.NextDouble();
        }

        /// <summary>
        /// Initialises new distribution.
        /// </summary>
        /// <param name="mean">Mean value for generated distrubution.</param>
        /// <param name="sigma">Deviation from mean value.</param>
        public void Init(double mean, double sigma)
        {
            this.deviate.SetDistributionParameters(mean, sigma);
        }
    }
}
