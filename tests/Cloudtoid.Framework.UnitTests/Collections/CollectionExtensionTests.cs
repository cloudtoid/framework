namespace Cloudtoid.Framework.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public sealed class CollectionExtensionTests
    {
        [TestMethod]
        public void IndexOf_WhenDifferentConditions_ReturnsIndex()
        {
            var list = new string?[] { "a", "b", null, "c", "d" };
            EnumerableExtensions.IndexOf(list, "a").Should().Be(0);
            EnumerableExtensions.IndexOf(list, "A").Should().Be(-1);
            EnumerableExtensions.IndexOf(list, "A", StringComparer.OrdinalIgnoreCase).Should().Be(0);
            EnumerableExtensions.IndexOf(list, null).Should().Be(2);
        }

        [TestMethod]
        public void WhereNotNull_ListOfTuples_DropsNullItem()
        {
            IList<(string Item1, string Item2)?> list = new List<(string Item1, string Item2)?>
            {
                ("a", "a"), ("b", "b"), null, ("c", "c")
            };

            list.WhereNotNull().Should().HaveCount(3);
        }

        [TestMethod]
        public void IsEmpty_EmptyIList_ReturnsTrue()
        {
            IList<string> list = new List<string>();
            list.IsEmpty().Should().BeTrue();
        }

        [TestMethod]
        public void IsEmpty_EmptyArray_ReturnsTrue()
        {
            var list = Array.Empty<string>();
            list.IsEmpty().Should().BeTrue();
        }

        [TestMethod]
        public void IsEmpty_EmptyICollection_ReturnsTrue()
        {
            ICollection<string> list = Array.Empty<string>();
            list.IsEmpty().Should().BeTrue();
        }

        [TestMethod]
        public void IsEmpty_EmptyReadOnlyList_ReturnsTrue()
        {
            IReadOnlyList<string> list = Array.Empty<string>();
            list.IsEmpty().Should().BeTrue();
        }

        [TestMethod]
        public void AsMutableList_EmptyReadOnlyList_ReturnsMutableList()
        {
            IReadOnlyList<string> list = Array.Empty<string>();
            var mutable = list.AsMutableList();
            mutable.Should().NotBeSameAs(list);
            mutable.IsReadOnly.Should().BeFalse();
            mutable.Add("z");
            mutable.Should().HaveCount(1);
        }

        [TestMethod]
        public void AsMutableList_Array_ReturnsMutableList()
        {
            var list = new string[] { "a", "b" };
            var mutable = list.AsMutableList();
            mutable.Should().NotBeSameAs(list);
            mutable.IsReadOnly.Should().BeFalse();
            mutable.Add("z");
            mutable.Should().HaveCount(3);
        }

        [TestMethod]
        public void AsMutableList_Enumerable_ReturnsMutableList()
        {
            var list = Enumerable.Repeat("a", 2);
            var mutable = list.AsMutableList();
            mutable.Should().NotBeSameAs(list);
            mutable.IsReadOnly.Should().BeFalse();
            mutable.Add("z");
            mutable.Should().HaveCount(3);
        }
    }
}
