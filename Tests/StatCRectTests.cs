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
    public class StatCRectTests
    {
        [Test]
        public void ShouldCalculateProperAverageAndDeviation()
        {
            // arrange
            var stat = new StatCRect();
            stat.Add(1, 0);
            stat.Add(0, 1);
            stat.Add(1, 2);
            stat.Add(0, 3);
            stat.Add(1, 4);

            // act
            double avg = 0;
            double dev = 0;
            stat.GetStat(ref avg, ref dev);
            double avg2 = 0;
            stat.GetStat(ref avg2);
            double avg3 = 0;
            double dev3 = 0;
            stat.GetStat(ref avg3, ref dev3, 4); 
            double avg4 = 0;
            stat.GetStat(ref avg4);

            // assert
            Assert.That(avg, Is.EqualTo(0.5));
            Assert.That(dev, Is.EqualTo(0.44721).Within(0.00001));

            Assert.That(avg2, Is.EqualTo(0.5));

            Assert.That(avg3, Is.EqualTo(0.5));
            Assert.That(dev3, Is.EqualTo(0.5).Within(0.00001));

            Assert.That(avg4, Is.EqualTo(0.5));
        }

        [Test]
        public void ClearingShouldEmptyStat()
        {
            // arrange
            var stat = new StatCRect();

            stat.Add(1, 0);
            stat.Add(0, 1);
            stat.Add(1, 2);
            stat.Add(0, 3);
            stat.Add(1, 4);

            // act
            stat.Clear();

            // assert
            double x = 0;
            Assert.That(() => stat.GetStat(ref x), Throws.TypeOf<ArgumentOutOfRangeException>());
        }
    }
}
