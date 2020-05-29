namespace Cloudtoid.Framework.UnitTests
{
    using System.Collections.Generic;
    using System.Linq;
    using Cloudtoid;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using static FluentAssertions.FluentActions;

    [TestClass]
    public sealed class ValueDictionaryTests
    {
        private static readonly string[] SingleStringArray = new[] { "a" };

        [TestMethod]
        public void Empty_WhenChanged_ItIsImmutable()
        {
            var v = ValueDictionary<string, string?>.Empty;
            v.Count.Should().Be(0);
            v.Add("test", "test");
            v.Count.Should().Be(1);

            ValueDictionary<string, string?>.Empty.Should().HaveCount(0);
        }

        [TestMethod]
        public void New_WhenEmpty_ValuesFieldIsEmpty()
        {
            var v = ValueDictionary<string, string?>.Empty;
            v.GetInner().Should().BeNull();
            v.Count.Should().Be(0);
            v.IsReadOnly.Should().BeFalse();
            v.Keys.Should().BeEmpty();
            v.Values.Should().BeEmpty();
            v.TryGetValue("a", out _).Should().BeFalse();
            v.Select(k => k.Value).WhereNotNull().ToList().Should().BeEmpty();
            v.ContainsKey(string.Empty).Should().BeFalse();
            v.CopyTo(new KeyValuePair<string, string?>[0], 0);
            v.Should().BeEmpty();
            v.Remove(string.Empty).Should().BeFalse();
            v.Clear(); // should not throw
            v.Count.Should().Be(0);
            v.GetInner().Should().BeNull();
            v.Add("a", "a");
            v["a"].Should().Be("a");
            v["a"] = "a";
            v.TryGetValue("a", out _).Should().BeTrue();
            v.Keys.Should().BeEquivalentTo(SingleStringArray);

            v = ValueDictionary<string, string?>.Empty;
            Invoking(() => v["a"]).Should().ThrowExactly<KeyNotFoundException>();
            v.Add("a", "a");
            v.Keys.Should().BeEquivalentTo(SingleStringArray);

            v = ValueDictionary<string, string?>.Empty;
            v.Add("a", "a");
            v.GetInner().Should().NotBeNull();
            v.Count.Should().Be(1);
            v.IsReadOnly.Should().BeFalse();
            v.Select(k => k.Value).WhereNotNull().ToList().Should().BeEquivalentTo(SingleStringArray);
            v.ContainsKey(string.Empty).Should().BeFalse();
            v.Remove(string.Empty).Should().BeFalse();
            v.ContainsKey("a").Should().BeTrue();
            v["a"].Should().Be("a");
            v["a"] = "a";
            v.Count.Should().Be(1);
            v.Remove("a").Should().BeTrue();
            v.Count.Should().Be(0);
            v.Clear(); // should not throw
            v.Count.Should().Be(0);
        }

        [TestMethod]
        public void New_WhenDefault_ValuesFieldIsNull()
        {
            var v = default(ValueDictionary<string, string?>);
            v.GetInner().Should().BeNull();
        }

        [TestMethod]
        public void New_WhenHasValue_ValuesFieldSetToThatValue()
        {
            var v = new ValueDictionary<string, string?>(new Dictionary<string, string?>()
            {
                ["a"] = "a",
                ["b"] = "b"
            });
            var inner = v.GetInner();
            inner.Should().NotBeNull();
            inner.Should().BeOfType<Dictionary<string, string?>>();
            v.Count.Should().Be(2);
            v.IsReadOnly.Should().BeFalse();
            v.Keys.Should().BeEquivalentTo(new[] { "a", "b" });
            v.Select(k => k.Value).Should().BeEquivalentTo(new[] { "a", "b" });
            v.Count.Should().Be(2);

            v.ContainsKey(string.Empty).Should().BeFalse();
            v.Remove(string.Empty).Should().BeFalse();
            v.ContainsKey("a").Should().BeTrue();
            v.Remove("b").Should().BeTrue();
            v.Count.Should().Be(1);

            v.Add("test", "test");
            inner = v.GetInner();
            inner.Should().NotBeNull();
            inner!.GetType().Should().Be(typeof(Dictionary<string, string?>));
            v.Count.Should().Be(2);
            v.IsReadOnly.Should().BeFalse();
            v.Values.Should().BeEquivalentTo(new[] { "a", "test" });
        }
    }
}
