namespace Cloudtoid.Framework.UnitTests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using FluentAssertions;
    using Microsoft.Extensions.Primitives;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public sealed class StringValuesExtensionsTests
    {
        private static readonly FieldInfo ValuesField = typeof(StringValues).GetField("_values", BindingFlags.Instance | BindingFlags.NonPublic)!;

        [TestMethod]
        public void AsStringValues_WhenNull_ReturnsDefault()
        {
            IEnumerable<string>? items = null;
            items.AsStringValues().Should().BeEmpty();
        }

        [TestMethod]
        public void AsStringValues_WhenOneValue_ReturnsOneValue()
        {
            var items = Enumerable.Repeat("a", 1);
            var values = items.AsStringValues();
            values.Should().BeEquivalentTo(new StringValues("a"));
            var internals = ValuesField.GetValue(values);
            internals.Should().NotBeNull();
            internals!.GetType().Should().Be(typeof(string));
        }

        [TestMethod]
        public void AsStringValues_WhenTwoValues_ReturnsTwoValue()
        {
            var items = Enumerable.Repeat("a", 2);
            var values = items.AsStringValues();
            values.Should().BeEquivalentTo(new StringValues(new[] { "a", "a" }));
        }

        [TestMethod]
        public void AsStringValues_WhenThreeValues_ReturnsThreeValue()
        {
            var items = Enumerable.Repeat("a", 3);
            var values = items.AsStringValues();
            values.Should().BeEquivalentTo(new StringValues(new[] { "a", "a", "a" }));
        }

        [TestMethod]
        public void AsStringValues_WhenFiveValues_ReturnsFiveValue()
        {
            var items = Enumerable.Repeat("a", 5);
            var values = items.AsStringValues();
            values.Should().BeEquivalentTo(new StringValues(new[] { "a", "a", "a", "a", "a" }));
        }

        [TestMethod]
        public void AsStringValues_WhenArrayIn_ReturnsExactSameArrayInstance()
        {
            var items = new[] { "a", "b" };
            var values = items.AsStringValues();
            values.ToArray().Should().BeSameAs(items);
        }

        [TestMethod]
        public void AsStringValues_WhenAlreadyStringValue_ReturnsStringValue()
        {
            var items = new StringValues("a");
            var values = items.AsStringValues();
            values.Should().BeEquivalentTo(items);
            ValuesField.GetValue(values).Should().NotBeNull().And.BeSameAs(ValuesField.GetValue(items));
        }
    }
}
