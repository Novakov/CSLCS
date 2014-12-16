using System.Collections.Generic;
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
            var result = group.All(x => x % 2 == 0);

            // assert
            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void AllShouldReturnZeroWhenNotAllElementsMeetCondition()
        {
            // arrange
            var group = new QueueGroup(5);
            group.To(2);
            group.To(4);
            group.To(6);
            group.To(8);
            group.To(9);

            // act
            var result = group.All(x => x % 2 == 0);

            // assert
            Assert.That(result, Is.EqualTo(0));
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

        [Test]
        public void ExistsShouldReturnOneIfThereIsEnoughElementsThatMeetCondition()
        {
            // arrange
            var group = new QueueGroup(5);
            group.To(2);
            group.To(4);
            group.To(6);
            group.To(8);

            // act
            var result = group.Exists(2, x => x > 4);

            // assert
            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void ExistsShouldReturnZeroIfThereIsNotEnoughElementsThatMeetCondition()
        {
            // arrange
            var group = new QueueGroup(5);
            group.To(2);
            group.To(4);
            group.To(6);
            group.To(8);

            // act
            var result = group.Exists(3, x => x > 4);

            // assert
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void UniqueShouldReturnOneIfExactNumberOfElementMeetCondition()
        {
            // arrange
            var group = new QueueGroup(5);
            group.To(2);
            group.To(4);
            group.To(6);
            group.To(8);

            // act
            var result = group.Unique(2, x => x > 4);

            // assert
            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void UniqueShouldReturnZeroIfMoreNumberOfElementMeetCondition()
        {
            // arrange
            var group = new QueueGroup(5);
            group.To(2);
            group.To(4);
            group.To(6);
            group.To(8);

            // act
            var result = group.Unique(1, x => x > 4);

            // assert
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void UniqueShouldReturnZeroIfLessNumberOfElementMeetCondition()
        {
            // arrange
            var group = new QueueGroup(5);
            group.To(2);
            group.To(4);
            group.To(6);
            group.To(8);

            // act
            var result = group.Unique(3, x => x > 4);

            // assert
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void SumShouldReturnSumOfElementsTransformedByFunction()
        {
            // arrange
            var group = new QueueGroup(5);
            group.To(2);
            group.To(4);
            group.To(6);
            group.To(8);

            // act
            var result = group.Sum(x => x + 1);

            // assert
            Assert.That(result, Is.EqualTo(24));
        }

        [Test]
        public void ForShouldInvokeActionForEachElement()
        {
            // arrange
            var group = new QueueGroup(5);
            group.To(2);
            group.To(4);
            group.To(6);
            group.To(8);

            int invocationCount = 0;

            // act
            var result = group.For(x =>
            {
                invocationCount++;
                return true;
            });

            // assert
            Assert.That(invocationCount, Is.EqualTo(4));
        }

        [Test]
        public void EmptyShoudReturnOneForEmptyQueue()
        {
            // arrange
            var group = new QueueGroup(5);

            // act
            var result = group.Empty();

            // assert
            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void EmptyShoudReturnZeroForNonEmptyQueue()
        {
            // arrange
            var group = new QueueGroup(5);
            group.To(4);

            // act
            var result = group.Empty();

            // assert
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void InShouldReturnOneIfElementBelongsToGroup()
        {
            // arrange
            var group = new QueueGroup(5);
            group.To(2);
            group.To(4);
            group.To(6);
            group.To(8);

            // act
            var result = group.In(4);

            // assert
            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void InShouldReturnZeroIfElementDoesNotBelongToGroup()
        {
            // arrange
            var group = new QueueGroup(5);
            group.To(2);
            group.To(4);
            group.To(6);
            group.To(8);

            // act
            var result = group.In(5);

            // assert
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void NotInShouldReturnZeroIfElementBelongsToGroup()
        {
            // arrange
            var group = new QueueGroup(5);
            group.To(2);
            group.To(4);
            group.To(6);
            group.To(8);

            // act
            var result = group.NotIn(4);

            // assert
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void NotInShouldReturnOneIfElementDoesNotBelongToGroup()
        {
            // arrange
            var group = new QueueGroup(5);
            group.To(2);
            group.To(4);
            group.To(6);
            group.To(8);

            // act
            var result = group.NotIn(5);

            // assert
            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void ZeroShouldClearQueue()
        {
            // arrange
            var group = new QueueGroup(5);
            group.To(4);

            // act
            group.Zero();

            // assert
            Assert.That(group.Empty(), Is.EqualTo(1));
        }

        [Test]
        public void FromShouldRemoveElementFromQueue()
        {
            // arrange
            var group = new QueueGroup(5);
            group.To(2);
            group.To(4);
            group.To(6);
            group.To(8);

            // act
            group.From(6);

            // assert
            Assert.That(group.In(6), Is.EqualTo(0));
        }

        [Test]
        public void FindFirstShouldReturnFirstElement()
        {
            // arrange
            var group = new QueueGroup(5);
            group.To(2);
            group.To(4);
            group.To(6);
            group.To(8);

            // act
            uint element = 0;
            var result = group.Find(ref element, FindParameters.FIRST);

            // assert
            Assert.That(result, Is.True);
            Assert.That(element, Is.EqualTo(2));
        }

        [Test]
        public void FindShouldReturnZeroWhenQueueIsEmpty()
        {
            // arrange
            var group = new QueueGroup(5);          

            // act
            uint element = 0;
            var result = group.Find(ref element, FindParameters.FIRST);

            // assert
            Assert.That(result, Is.False);            
        }

        [Test]
        public void FindShouldReturnZeroWhenNoElementMatchesCondition()
        {
            // arrange
            var group = new QueueGroup(5);
            group.To(2);
            group.To(4);
            group.To(6);
            group.To(8);

            // act
            uint element = 0;
            var result = group.Find(ref element, FindParameters.FIRST, x => x > 10);

            // assert
            Assert.That(result, Is.False);
        }
    }
}