﻿namespace Cloudtoid.Framework.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Cloudtoid;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using static FluentAssertions.FluentActions;

    [TestClass]
    public sealed class ValueListTests
    {
        [TestMethod]
        public void New_WhenEmptyy_ValuesFieldIsEmpty()
        {
            var v = ValueList<string>.Empty;
            v.values.Should().BeOfType<string[]>().And.BeSameAs(Array.Empty<string>());
            v.Count.Should().Be(0);
            v.ToArray().Should().BeSameAs(Array.Empty<string>());
            v.WhereNotNull().ToList().Should().BeEmpty();

            var l = (IList<string>)v;
            l.IndexOf("a").Should().Be(-1);

            var c = (ICollection<string>)v;
            c.Contains("a").Should().BeFalse();
            c.WhereNotNull().ToList().Should().BeEmpty();

            var a = Array.Empty<string>();
            c.CopyTo(a, 0);
        }

        [TestMethod]
        public void New_WhenDefault_ValuesFieldIsNull()
        {
            var v = default(ValueList<string>);
            v.values.Should().BeNull();
            v.Count.Should().Be(0);
            v.ToArray().Should().BeSameAs(Array.Empty<string>());
            v.WhereNotNull().ToList().Should().BeEmpty();

            var l = (IList<string>)v;
            l.IndexOf("a").Should().Be(-1);

            var c = (ICollection<string>)v;
            c.Contains("a").Should().BeFalse();

            var a = Array.Empty<string>();
            c.CopyTo(a, 0);
        }

        [TestMethod]
        public void New_WhenNullValue_ValuesFieldIsNull()
        {
            var v = new ValueList<string>(default(string));
            v.values.Should().BeNull();
            v.Count.Should().Be(0);
            v.ToArray().Should().BeSameAs(Array.Empty<string>());
            v.WhereNotNull().ToList().Should().BeEmpty();

            var l = (IList<string>)v;
            l.IndexOf("a").Should().Be(-1);

            var c = (ICollection<string>)v;
            c.Contains("a").Should().BeFalse();

            var a = Array.Empty<string>();
            c.CopyTo(a, 0);
        }

        [TestMethod]
        public void New_WhenOneValue_ValuesFieldSetToThatValue()
        {
            var v = new ValueList<string>("a");
            v.values.Should().Be("a");
            v.Count.Should().Be(1);
            v[0].Should().Be("a");
            v.ToArray().Should().BeEquivalentTo(new[] { "a" });
            v.WhereNotNull().ToList().Should().BeEquivalentTo(new[] { "a" });

            var l = (IList<string>)v;
            l.IndexOf("a").Should().Be(0);
            l.IndexOf("b").Should().Be(-1);
            l[0].Should().Be("a");

            var c = (ICollection<string>)v;
            c.Contains("a").Should().BeTrue();
            c.Contains("b").Should().BeFalse();

            var a = new string[1];
            c.CopyTo(a, 0);
            a.Should().BeEquivalentTo(new[] { "a" });
        }

        [TestMethod]
        public void New_WhenValueArray_ValuesFieldSetToThatArray()
        {
            var array = new[] { "a", "b" };
            var v = new ValueList<string>(array);
            v.values.Should().BeSameAs(array);
            v.Count.Should().Be(2);
            v[0].Should().Be("a");
            v[1].Should().Be("b");
            v.ToArray().Should().BeSameAs(array);
            v.WhereNotNull().ToList().Should().BeEquivalentTo(new[] { "a", "b" });

            var l = (IList<string>)v;
            l.IndexOf("a").Should().Be(0);
            l.IndexOf("b").Should().Be(1);
            l.IndexOf("c").Should().Be(-1);

            var c = (ICollection<string>)v;
            c.Contains("a").Should().BeTrue();
            c.Contains("b").Should().BeTrue();
            c.Contains("c").Should().BeFalse();

            var a = new string[array.Length];
            c.CopyTo(a, 0);
            a.Should().BeEquivalentTo(array);
        }

        [TestMethod]
        public void Indexer_WhenNullValue_Throws()
        {
            var v = new ValueList<string>(default(string));
            Invoking(() => v[0]).Should().ThrowExactly<IndexOutOfRangeException>();
        }

        [TestMethod]
        public void Indexer_WhenOneValueAccessingOutOfBounds_Throws()
        {
            var v = new ValueList<string>("a");
            Invoking(() => v[-1]).Should().ThrowExactly<IndexOutOfRangeException>();
            Invoking(() => v[1]).Should().ThrowExactly<IndexOutOfRangeException>();
        }

        [TestMethod]
        public void CopyTo_WhenBadArgs_Throws()
        {
            var array = new[] { "a", "b" };
            var v = new ValueList<string>(array);
            var c = (ICollection<string>)v;

            var a = new string[array.Length];
            c.CopyTo(a, 0);
            a.Should().BeEquivalentTo(array);

            Invoking(() => c.CopyTo(a, 1)).Should().ThrowExactly<ArgumentException>();
            Invoking(() => c.CopyTo(a, -1)).Should().ThrowExactly<ArgumentOutOfRangeException>();
            Invoking(() => c.CopyTo(null!, 0)).Should().ThrowExactly<ArgumentNullException>();

            a = new string[1];
            Invoking(() => c.CopyTo(a, 2)).Should().ThrowExactly<ArgumentException>("*is not long enough to copy all the items*");
        }

        [TestMethod]
        public void Equals_Mix()
        {
            var array = new[] { "a", "b" };
            var v2 = new ValueList<string>(array);

            (v2 == array).Should().BeTrue();
            (v2 != array).Should().BeFalse();
            (v2 == (object)array).Should().BeTrue();
            (v2 != (object)array).Should().BeFalse();
            (v2 == new ValueList<string>(array)).Should().BeTrue();
            (v2 != new ValueList<string>(array)).Should().BeFalse();

            (array == v2).Should().BeTrue();
            (array != v2).Should().BeFalse();
            (((object)array) == v2).Should().BeTrue();
            (((object)array) != v2).Should().BeFalse();
            (new ValueList<string>(array) == v2).Should().BeTrue();
            (new ValueList<string>(array) != v2).Should().BeFalse();

            v2.Equals(array).Should().BeTrue();
            v2.Equals(v2).Should().BeTrue();
            v2.Equals((object?)v2).Should().BeTrue();
            v2.Equals((object)array).Should().BeTrue();
            v2.Equals(new ValueList<string>(array)).Should().BeTrue();
            v2.Equals(new ValueList<string>(new[] { "a", "b" })).Should().BeTrue();
            v2.Equals(new ValueList<string>(new[] { "a", "c" })).Should().BeFalse();
            v2.Equals(new ValueList<string>(default(string))).Should().BeFalse();
            v2.Equals(default(string)).Should().BeFalse();
            v2.Equals(new ValueList<string>("a")).Should().BeFalse();

            var s = "a";
            var v1 = new ValueList<string>(s);

            (v1 == s).Should().BeTrue();
            (v1 != s).Should().BeFalse();
            (s == v1).Should().BeTrue();
            (s != v1).Should().BeFalse();
            v1.Equals(v1).Should().BeTrue();
            v1.Equals(s).Should().BeTrue();
            v1.Equals((object)s).Should().BeTrue();
            v1.Equals(new ValueList<string>(s)).Should().BeTrue();
            v1.Equals(new ValueList<string>(default(string))).Should().BeFalse();
            v1.Equals(default(string)).Should().BeFalse();
            v1.Equals(new ValueList<string>(array)).Should().BeFalse();

            var v0 = new ValueList<string>(default(string));

            v0.Equals((object?)null).Should().BeTrue();
            v0.Equals(v0).Should().BeTrue();
            v0.Equals(default(string)).Should().BeTrue();
            v0.Equals(new ValueList<string>(default(string))).Should().BeTrue();
            v0.Equals(default(string)).Should().BeTrue();
            v0.Equals(ValueList<string>.Empty).Should().BeTrue();
            v0.Equals(new ValueList<string>(array)).Should().BeFalse();
            v0.Equals("a").Should().BeFalse();
            v0.Equals(10).Should().BeFalse();

            ValueList<string>.Equals(v2, default(string)).Should().BeFalse();
            ValueList<string>.Equals(v0, default(string)).Should().BeTrue();
            ValueList<string>.Equals(v2, array).Should().BeTrue();
            ValueList<string>.Equals(default(string), v2).Should().BeFalse();
            ValueList<string>.Equals(default(string), v0).Should().BeTrue();
            ValueList<string>.Equals(array, v2).Should().BeTrue();
        }

        [TestMethod]
        public void ReadOnlyTests()
        {
            var array = new[] { "a", "b" };
            var v = new ValueList<string>(array);
            var c = (ICollection<string?>)v;
            var l = (IList<string?>)v;

            c.IsReadOnly.Should().BeTrue();
            Invoking(() => c.Remove("a")).Should().ThrowExactly<NotSupportedException>();
            Invoking(() => c.Add("a")).Should().ThrowExactly<NotSupportedException>();
            Invoking(() => c.Clear()).Should().ThrowExactly<NotSupportedException>();
            Invoking(() => l[0] = "a").Should().ThrowExactly<NotSupportedException>();
            Invoking(() => l.Insert(0, "a")).Should().ThrowExactly<NotSupportedException>();
            Invoking(() => l.RemoveAt(1)).Should().ThrowExactly<NotSupportedException>();
        }

        [TestMethod]
        public void GetHashCodeTests()
        {
            var array = new[] { "a", "b" };
            var v = new ValueList<string>(array);
            v.GetHashCode().Should().NotBe(HashUtil.NullHashCode);

            v = new ValueList<string>("a");
#pragma warning disable RS0030 // Do not used banned APIs
            v.GetHashCode().Should().Be("a".GetHashCode());
#pragma warning restore RS0030 // Do not used banned APIs

            ValueList<string>.Empty.GetHashCode().Should().Be(0);

            new ValueList<string>(default(string)).GetHashCode().Should().Be(HashUtil.NullHashCode);
        }
    }
}
