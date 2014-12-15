using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSL.Statistics;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class StatDTests
    {
        [Test]
        public void ShouldCalculateProperAverageAndDeviation()
        {
            // arrange
            var stat = new StatD();
            stat.Add(1);
            stat.Add(2);
            stat.Add(3);
            stat.Add(2);
            stat.Add((long)1);

            // act
            double avg = 0;
            double dev = 0;
            stat.GetStat(ref avg, ref dev);
            double avg2 = 0;
            stat.GetStat(ref avg2);

            // assert
            Assert.That(avg, Is.EqualTo(1.8));
            Assert.That(dev, Is.EqualTo(0.74833).Within(0.00001));

            Assert.That(avg2, Is.EqualTo(1.8));
        }

        [Test]
        public void ClearingShouldEmptyStat()
        {
            // arrange
            var stat = new StatD();

            stat.Add(1);

            // act
            stat.Clear();

            // assert
            double x = 0;
            Assert.That(()=>stat.GetStat(ref x), Throws.InvalidOperationException);
        }
    }
}
