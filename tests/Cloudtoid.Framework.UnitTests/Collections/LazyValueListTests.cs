namespace Cloudtoid.Framework.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Cloudtoid;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using static FluentAssertions.FluentActions;

    [TestClass]
    public sealed class LazyValueListTests
    {
        [TestMethod]
        public void New_WhenEmpty_ValuesFieldIsEmpty()
        {
            var v = LazyValueList<string?>.Empty;
            v.GetInner().Should().BeNull();
            v.Count.Should().Be(0);
            v.IsReadOnly.Should().BeFalse();
            v.ToArray().Should().BeSameAs(Array.Empty<string>());
            v.WhereNotNull().ToList().Should().BeEmpty();
            v.IndexOf(string.Empty).Should().Be(-1);
            v.Contains(string.Empty).Should().BeFalse();
            v.Remove(string.Empty).Should().BeFalse();
            v.Clear(); // should not throw
            v.Count.Should().Be(0);

            Invoking(() => v[0]).Should().ThrowExactly<IndexOutOfRangeException>();
            Invoking(() => v.RemoveAt(0)).Should().ThrowExactly<ArgumentOutOfRangeException>();

            v.Add("test");
            v.GetInner().Should().NotBeNull();
            v.Count.Should().Be(1);
            v.IsReadOnly.Should().BeFalse();
            v.ToArray().Should().BeEquivalentTo(new[] { "test" });
            v.WhereNotNull().ToList().Should().BeEquivalentTo(new[] { "test" });
            v[0].Should().Be("test");
            v.IndexOf(string.Empty).Should().Be(-1);
            v.Contains(string.Empty).Should().BeFalse();
            v.Remove(string.Empty).Should().BeFalse();
            v.Contains("test").Should().BeTrue();
            v.IndexOf("test").Should().Be(0);
            v.Remove("test").Should().BeTrue();
            v.Clear(); // should not throw
            v.Count.Should().Be(0);

            v.Add("test");
            v.Count.Should().Be(1);
            v.RemoveAt(0);
            v.Count.Should().Be(0);

            v.Add("test");
            v.Count.Should().Be(1);
            v.Clear(); // should not throw
            v.Count.Should().Be(0);
        }

        [TestMethod]
        public void New_WhenDefault_ValuesFieldIsNull()
        {
            var v = default(LazyValueList<string?>);
            v.GetInner().Should().BeNull();
        }

        [TestMethod]
        public void New_WhenNullValue_ValuesFieldIsNull()
        {
            var v = new LazyValueList<string?>(default(string));
            v.GetInner().Should().BeNull();
        }

        [TestMethod]
        public void New_WhenOneValue_ValuesFieldSetToThatValue()
        {
            var v = new LazyValueList<string?>("a");
            var inner = v.GetInner();
            inner.Should().NotBeNull();
            inner.Should().BeOfType<ReadOnlyValueList<string?>>();
            v.Count.Should().Be(1);
            v.IsReadOnly.Should().BeFalse();
            v.ToArray().Should().BeEquivalentTo(new[] { "a" });
            v.WhereNotNull().ToList().Should().BeEquivalentTo(new[] { "a" });
            v[0].Should().Be("a");
            v.IndexOf(string.Empty).Should().Be(-1);
            v.Contains(string.Empty).Should().BeFalse();
            v.Remove(string.Empty).Should().BeFalse();
            v.Contains("a").Should().BeTrue();
            v.IndexOf("a").Should().Be(0);

            v.Add("test");
            inner = v.GetInner();
            inner.Should().NotBeNull();
            inner!.GetType().Should().Be(typeof(List<string?>));
            v.Count.Should().Be(2);
            v.IsReadOnly.Should().BeFalse();
            v.ToArray().Should().BeEquivalentTo(new[] { "a", "test" });
            v.WhereNotNull().ToList().Should().BeEquivalentTo(new[] { "a", "test" });
            v[0].Should().Be("a");
            v[1].Should().Be("test");
            v.IndexOf(string.Empty).Should().Be(-1);
            v.Contains(string.Empty).Should().BeFalse();
            v.Remove(string.Empty).Should().BeFalse();
            v.Contains("test").Should().BeTrue();
            v.IndexOf("test").Should().Be(1);
            v.Remove("test").Should().BeTrue();
            v.Clear(); // should not throw
            v.Count.Should().Be(0);

            v = new LazyValueList<string?>("a");
            v.Remove("a").Should().BeTrue();
            v.Clear(); // should not throw
            v.Count.Should().Be(0);
        }

        [TestMethod]
        public void New_WhenOneEnumerableValue_ValuesFieldSetToThatValue()
        {
            var v = new LazyValueList<string?>(Enumerable.Repeat("a", 1));
            var inner = v.GetInner();
            inner.Should().NotBeNull();
            inner.Should().BeOfType<ReadOnlyValueList<string?>>();
            ((ReadOnlyValueList<string?>)inner!).items.Should().Be("a");
        }

        [TestMethod]
        public void New_WhenMultipleEnumerableValues_ValuesFieldSetToListOfValues()
        {
            var e = Enumerable.Range(1, 1000).Select(i => i.ToStringInvariant());
            var v = new LazyValueList<string?>(e);
            var inner = v.GetInner();
            inner.Should().NotBeNull();
            inner.Should().BeOfType<ReadOnlyValueList<string?>>();
            ((ReadOnlyValueList<string?>)inner!).items.Should().BeEquivalentTo(e);
            v.Count.Should().Be(1000);
            v.IsReadOnly.Should().BeFalse();
            v.ToArray().Should().BeEquivalentTo(e);
            v.WhereNotNull().ToList().Should().BeEquivalentTo(e);
            v[0].Should().Be("1");
            v[1].Should().Be("2");
            v.IndexOf(string.Empty).Should().Be(-1);
            v.Contains(string.Empty).Should().BeFalse();
            v.Remove(string.Empty).Should().BeFalse();
            v.Contains("10").Should().BeTrue();
            v.IndexOf("10").Should().Be(9);

            v.Add("test");
            inner = v.GetInner();
            inner.Should().NotBeNull();
            inner!.GetType().Should().Be(typeof(List<string?>));
            v.Count.Should().Be(1001);
            v[1000].Should().Be("test");
        }

        [TestMethod]
        public void New_WhenValueArray_ValuesFieldSetToThatArray()
        {
            var e = Enumerable.Range(1, 1000).Select(i => i.ToStringInvariant()).ToArray();
            var v = new LazyValueList<string?>(e);
            var inner = v.GetInner();
            inner.Should().NotBeNull();
            inner.Should().BeOfType<ReadOnlyValueList<string?>>();
            ((ReadOnlyValueList<string?>)inner!).items.Should().BeEquivalentTo(e);
            v.Count.Should().Be(1000);
            v.IsReadOnly.Should().BeFalse();
            v.ToArray().Should().BeEquivalentTo(e);
            v.WhereNotNull().ToList().Should().BeEquivalentTo(e);
            v[0].Should().Be("1");
            v[1].Should().Be("2");
            v.IndexOf(string.Empty).Should().Be(-1);
            v.Contains(string.Empty).Should().BeFalse();
            v.Remove(string.Empty).Should().BeFalse();
            v.Contains("10").Should().BeTrue();
            v.IndexOf("10").Should().Be(9);

            v.Add("test");
            inner = v.GetInner();
            inner.Should().NotBeNull();
            inner!.GetType().Should().Be(typeof(List<string?>));
            v.Count.Should().Be(1001);
            v[1000].Should().Be("test");
        }
    }
}
