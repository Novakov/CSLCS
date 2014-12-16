using CSL.Groups;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class QueueTests
    {
        [Test]
        public void AllShouldReturnOneWhenAllElementsMeetCondition()
        {
            // arrange
            var group = new QueueGroup(5);
            group.To(2);
            group.To(4);
            group.To(6);
            group.To(8);

            // act
            var result = group.All(x => x%2 == 0);

            // assert
            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void CountShouldReturnCountOfElementsThatMeetCondition()
        {
            // arrange
            var group = new QueueGroup(5);
            group.To(2);
            group.To(4);
            group.To(6);
            group.To(8);

            // act
            var result = group.Count(x => x > 4);

            // assert
            Assert.That(result, Is.EqualTo(2));
        }
    }
}