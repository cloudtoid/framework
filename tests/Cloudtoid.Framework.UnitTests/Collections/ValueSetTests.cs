namespace Cloudtoid.Framework.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Cloudtoid;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public sealed class ValueSetTests
    {
        private static readonly string[] SingleStringArray = new[] { "a" };

        [TestMethod]
        public void Empty_WhenChanged_ItIsImmutable()
        {
            var v = ValueSet<string?>.Empty;
            v.Count.Should().Be(0);
            v.Add("test");
            v.Count.Should().Be(1);

            ValueSet<string?>.Empty.Should().HaveCount(0);
        }

        [TestMethod]
        public void New_WhenEmpty_ValuesFieldIsEmpty()
        {
            var v = ValueSet<string?>.Empty;
            v.GetInner().Should().BeNull();
            v.Count.Should().Be(0);
            v.IsReadOnly.Should().BeFalse();
            v.WhereNotNull().ToList().Should().BeEmpty();
            v.Contains(string.Empty).Should().BeFalse();
            v.CopyTo(new string[0], 0);
            v.ExceptWith(SingleStringArray);
            v.IntersectWith(SingleStringArray);
            v.IsProperSubsetOf(SingleStringArray).Should().BeTrue();
            v.IsProperSupersetOf(SingleStringArray).Should().BeFalse();
            v.IsSubsetOf(SingleStringArray).Should().BeTrue();
            v.IsSupersetOf(SingleStringArray).Should().BeFalse();
            v.IsSupersetOf(Array.Empty<string>()).Should().BeTrue();
            v.Overlaps(SingleStringArray).Should().BeFalse();
            v.Overlaps(Array.Empty<string>()).Should().BeFalse();
            v.SetEquals(Array.Empty<string>()).Should().BeTrue();
            v.Should().BeEmpty();
            v.Remove(string.Empty).Should().BeFalse();
            v.Clear(); // should not throw
            v.Count.Should().Be(0);
            v.GetInner().Should().BeNull();
            v.SymmetricExceptWith(SingleStringArray);
            v.Should().BeEquivalentTo(SingleStringArray);

            v = ValueSet<string?>.Empty;
            v.UnionWith(SingleStringArray);
            v.Should().BeEquivalentTo(SingleStringArray);

            v = ValueSet<string?>.Empty;
            v.Add("a");
            v.GetInner().Should().NotBeNull();
            v.Count.Should().Be(1);
            v.IsReadOnly.Should().BeFalse();
            v.WhereNotNull().ToList().Should().BeEquivalentTo(SingleStringArray);
            v.IndexOf(string.Empty).Should().Be(-1);
            v.Contains(string.Empty).Should().BeFalse();
            v.Remove(string.Empty).Should().BeFalse();
            v.Contains("a").Should().BeTrue();
            v.IndexOf("a").Should().Be(0);
            v.Add("a");
            v.Count.Should().Be(1);
            v.Remove("a").Should().BeTrue();
            v.Count.Should().Be(0);
            v.Clear(); // should not throw
            v.Count.Should().Be(0);
        }

        [TestMethod]
        public void New_WhenDefault_ValuesFieldIsNull()
        {
            var v = default(ValueSet<string?>);
            v.GetInner().Should().BeNull();
        }

        [TestMethod]
        public void TestICollctionOverrides()
        {
            var v = (ICollection<string?>)default(ValueSet<string?>);
            v.Add("a");
            v.Should().BeEquivalentTo(SingleStringArray);
        }

        [TestMethod]
        public void New_WhenHasValue_ValuesFieldSetToThatValue()
        {
            var v = new ValueSet<string?>(new HashSet<string?>(new[] { "a", "b" }));
            var inner = v.GetInner();
            inner.Should().NotBeNull();
            inner.Should().BeOfType<HashSet<string?>>();
            v.Count.Should().Be(2);
            v.IsReadOnly.Should().BeFalse();
            v.ToArray().Should().BeEquivalentTo(new[] { "a", "b" });
            v.WhereNotNull().ToList().Should().BeEquivalentTo(new[] { "a", "b" });
            v.Count.Should().Be(2);
            v.IsProperSubsetOf(SingleStringArray).Should().BeFalse();
            v.IsProperSupersetOf(SingleStringArray).Should().BeTrue();
            v.IsSubsetOf(SingleStringArray).Should().BeFalse();
            v.IsSupersetOf(SingleStringArray).Should().BeTrue();
            v.IsSupersetOf(Array.Empty<string>()).Should().BeTrue();
            v.Overlaps(SingleStringArray).Should().BeTrue();
            v.Overlaps(Array.Empty<string>()).Should().BeFalse();
            v.SetEquals(Array.Empty<string>()).Should().BeFalse();

            v.Contains(string.Empty).Should().BeFalse();
            v.Remove(string.Empty).Should().BeFalse();
            v.Contains("a").Should().BeTrue();
            v.Remove("b").Should().BeTrue();
            v.Count.Should().Be(1);

            v.Add("test");
            inner = v.GetInner();
            inner.Should().NotBeNull();
            inner!.GetType().Should().Be(typeof(HashSet<string?>));
            v.Count.Should().Be(2);
            v.IsReadOnly.Should().BeFalse();
            v.ToArray().Should().BeEquivalentTo(new[] { "a", "test" });
            v.IntersectWith(SingleStringArray);
            v.ToArray().Should().BeEquivalentTo(SingleStringArray);
            v.UnionWith(new[] { "test" });
            v.ToArray().Should().BeEquivalentTo(new[] { "a", "test" });
            v.ExceptWith(new[] { "test" });
            v.ToArray().Should().BeEquivalentTo(SingleStringArray);
        }
    }
}
