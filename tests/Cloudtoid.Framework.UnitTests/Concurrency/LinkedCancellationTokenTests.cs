using System.Collections.Generic;
using System.Threading;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cloudtoid.Framework.UnitTests
{
    [TestClass]
    public sealed class LinkedCancellationTokenTests
    {
        [TestMethod]
        public void SuccessfulLinkedTokenCreation()
        {
            using var source1 = new CancellationTokenSource();
            using var source2 = new CancellationTokenSource();
            using var linked = new LinkedCancellationToken(source1.Token, source2.Token);
            linked.Token.Should().NotBe(default(LinkedCancellationToken));
        }

        [TestMethod]
        public void DisposeDefaultToken()
        {
            var linked = default(LinkedCancellationToken);
            linked.GetHashCode().Should().Be(0);
            linked.Token.Should().Be(default(CancellationToken));
            linked.Dispose();
        }

        [TestMethod]
        public void DisposeMoreThanOnce()
        {
            using var source1 = new CancellationTokenSource();
            using var source2 = new CancellationTokenSource();
            var linked = new LinkedCancellationToken(source1.Token, source2.Token);
            linked.Dispose();
            linked.Dispose();
        }

        [TestMethod]
        public void CheckEquality()
        {
            using var source1 = new CancellationTokenSource();
            using var source2 = new CancellationTokenSource();
            using var linked = new LinkedCancellationToken(source1.Token, source2.Token);
            linked.Equals(linked).Should().BeTrue();
#pragma warning disable CS1718 // Comparison made to same variable
            (linked == linked).Should().BeTrue();
            (linked != linked).Should().BeFalse();
#pragma warning restore CS1718 // Comparison made to same variable
        }

        [TestMethod]
        public void ExhaustPool()
        {
            using var source1 = new CancellationTokenSource();
            using var source2 = new CancellationTokenSource();
            var count = LinkedCancellationToken.Count;
            var set = new HashSet<LinkedCancellationToken>();

            for (int i = 0; i < count + 10; i++)
                set.Add(new LinkedCancellationToken(source1.Token, source2.Token)).Should().BeTrue();

            foreach (var linked in set)
                linked.Dispose();

            for (int i = 0; i < count; i++)
                set.Add(new LinkedCancellationToken(source1.Token, source2.Token)).Should().BeFalse();

            for (int i = 0; i < 10; i++)
                set.Add(new LinkedCancellationToken(source1.Token, source2.Token)).Should().BeTrue();

            set.Should().HaveCount(count + 20);

            foreach (var linked in set)
                linked.Dispose();
        }

        [TestMethod]
        public void CancelledNotReturned()
        {
            using var source1 = new CancellationTokenSource();
            using var source2 = new CancellationTokenSource();
            using var source3 = new CancellationTokenSource();
            var count = LinkedCancellationToken.Count;
            var set = new HashSet<LinkedCancellationToken>();

            var cancelledLinked = new LinkedCancellationToken(source3.Token, source2.Token);
            source3.Cancel();
            cancelledLinked.Token.IsCancellationRequested.Should().BeTrue();
            cancelledLinked.Dispose();

            for (int i = 0; i < count; i++)
            {
                var linked = new LinkedCancellationToken(source1.Token, source2.Token);
                set.Add(linked).Should().BeTrue();
                cancelledLinked.Equals(linked).Should().BeFalse();
            }

            foreach (var linked in set)
                linked.Dispose();
        }
    }
}
