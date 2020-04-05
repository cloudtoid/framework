namespace Cloudtoid.Framework.UnitTests
{
    using System;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using static Contract;
    using static FluentAssertions.FluentActions;

    [TestClass]
    public sealed class ContractTests
    {
        [TestMethod]
        public void CheckValue_NonNullString_Success()
        {
            string? nonNullString = "some-string";
            CheckValue<object>(nonNullString, "testParam")
                .Should()
                .Be(nonNullString);

            CheckValue<object>(nonNullString, "testParam", "SomeMessage")
                .Should()
                .Be(nonNullString);

            CheckValue<object, string>(nonNullString, "testParam", "SomeMessage-{0}", "arg0")
                .Should()
                .Be(nonNullString);

            CheckValue<object, string, string>(nonNullString, "testParam", "SomeMessage-{0}{1}", "arg0", "arg1")
                .Should()
                .Be(nonNullString);

            CheckValue<object, string, string, string>(nonNullString, "testParam", "SomeMessage-{0}{1}{2}", "arg0", "arg1", "arg2")
                .Should()
                .Be(nonNullString);

            CheckValue<object>(nonNullString, "testParam", "SomeMessage-{0}{1}{2}{3}", "arg0", "arg1", "arg2", "arg3")
                .Should()
                .Be(nonNullString);

            CheckValue<object, object[]>(nonNullString, "testParam", "SomeMessage-{0}", new object[] { "arg0" })
                .Should()
                .Be(nonNullString);
        }

        [TestMethod]
        public void CheckValue_NullString_Throws()
        {
            string? nullString = null;
            Invoking(() => CheckValue<object>(nullString, "testParam"))
                .Should()
                .ThrowExactly<ArgumentNullException>()
                .WithMessage("*testParam*");

            Invoking(() => CheckValue<object>(nullString, "testParam", "SomeMessage"))
                .Should()
                .ThrowExactly<ArgumentNullException>()
                .WithMessage("*SomeMessage*")
                .WithMessage("*testParam*");

            Invoking(() => CheckValue<object, string>(nullString, "testParam", "SomeMessage-{0}", "arg0"))
                .Should()
                .ThrowExactly<ArgumentNullException>()
                .WithMessage("*SomeMessage*")
                .WithMessage("*testParam*")
                .WithMessage("*arg0*");

            Invoking(() => CheckValue<object, string, string>(nullString, "testParam", "SomeMessage-{0}{1}", "arg0", "arg1"))
                .Should()
                .ThrowExactly<ArgumentNullException>()
                .WithMessage("*SomeMessage*")
                .WithMessage("*testParam*")
                .WithMessage("*arg0*")
                .WithMessage("*arg1*");

            Invoking(() => CheckValue<object, string, string, string>(nullString, "testParam", "SomeMessage-{0}{1}{2}", "arg0", "arg1", "arg2"))
                .Should()
                .ThrowExactly<ArgumentNullException>()
                .WithMessage("*SomeMessage*")
                .WithMessage("*testParam*")
                .WithMessage("*arg0*")
                .WithMessage("*arg1*")
                .WithMessage("*arg2*");

            Invoking(() => CheckValue<object>(nullString, "testParam", "SomeMessage-{0}{1}{2}{3}", "arg0", "arg1", "arg2", "arg3"))
                .Should()
                .ThrowExactly<ArgumentNullException>()
                .WithMessage("*SomeMessage*")
                .WithMessage("*testParam*")
                .WithMessage("*arg0*")
                .WithMessage("*arg1*")
                .WithMessage("*arg2*")
                .WithMessage("*arg3*");

            Invoking(() => CheckValue<object, object[]>(nullString, "testParam", "SomeMessage-{0}", new object[] { "arg0" }))
                .Should()
                .ThrowExactly<ArgumentNullException>()
                .WithMessage("*SomeMessage*")
                .WithMessage("*testParam*")
                .WithMessage("*" + nameof(Object) + "[]*");
        }

        [TestMethod]
        public void CheckRange_InBound_Success()
        {
            CheckRange(2, 2, 2, "testParam").Should().Be(2);
            CheckRange(2, 1, 3, "testParam").Should().Be(2);
            CheckRange(2, 1, 2, "testParam").Should().Be(2);
            CheckRange(2, 2, 3, "testParam").Should().Be(2);
        }

        [TestMethod]
        public void CheckRange_OutOfUpperBound_Throws()
        {
            Invoking(() => CheckRange(3, 1, 2, "testParam"))
                .Should()
                .ThrowExactly<ArgumentOutOfRangeException>()
                .WithMessage("*[1,2]*");
        }

        [TestMethod]
        public void CheckRange_OutOfLowerBound_Throws()
        {
            Invoking(() => CheckRange(0, 1, 2, "testParam"))
                .Should()
                .ThrowExactly<ArgumentOutOfRangeException>()
                .WithMessage("*[1,2]*");
        }

        [TestMethod]
        public void CheckLessThanOrEqual_InBound_Success()
        {
            CheckLessThanOrEqual(2, 2, "testParam").Should().Be(2);
            CheckLessThanOrEqual(1, 2, "testParam").Should().Be(1);
        }

        [TestMethod]
        public void CheckLessThanOrEqual_OutOfUpperBound_Throws()
        {
            Invoking(() => CheckLessThanOrEqual(3, 2, "testParam"))
                .Should()
                .ThrowExactly<ArgumentOutOfRangeException>()
                .WithMessage("*less than or equal to 2*");
        }

        [TestMethod]
        public void CheckGreaterThanOrEqual_InBound_Success()
        {
            CheckGreaterThanOrEqual(2, 2, "testParam").Should().Be(2);
            CheckGreaterThanOrEqual(3, 2, "testParam").Should().Be(3);
        }

        [TestMethod]
        public void CheckGreaterThanOrEqual_OutOfUpperBound_Throws()
        {
            Invoking(() => CheckGreaterThanOrEqual(1, 2, "testParam"))
                .Should()
                .ThrowExactly<ArgumentOutOfRangeException>()
                .WithMessage("*greater than or equal to 2*");
        }

        [TestMethod]
        public void CheckNonNegative_Positive_Success()
        {
            CheckNonNegative(0, "testParam").Should().Be(0);
            CheckNonNegative(1, "testParam").Should().Be(1);
        }

        [TestMethod]
        public void CheckNonNegative_Negative_Throws()
        {
            Invoking(() => CheckNonNegative(-1, "testParam"))
                .Should()
                .ThrowExactly<ArgumentOutOfRangeException>()
                .WithMessage("*greater than or equal to 0*");
        }

        [TestMethod]
        public void CheckValue_NullTask_Throws()
        {
            Task? nullTask = null;
            Invoking(() => CheckValue(nullTask, "testParam"))
                .Should()
                .ThrowExactly<ArgumentNullException>()
                .WithMessage("*testParam*");

            Invoking(() => CheckValue(nullTask, "testParam", "SomeMessage"))
                .Should()
                .ThrowExactly<ArgumentNullException>()
                .WithMessage("*SomeMessage*")
                .WithMessage("*testParam*");

            Invoking(() => CheckValue(nullTask, "testParam", "SomeMessage-{0}", "arg0"))
                .Should()
                .ThrowExactly<ArgumentNullException>()
                .WithMessage("*SomeMessage*")
                .WithMessage("*testParam*")
                .WithMessage("*arg0*");

            Invoking(() => CheckValue(nullTask, "testParam", "SomeMessage-{0}{1}", "arg0", "arg1"))
                .Should()
                .ThrowExactly<ArgumentNullException>()
                .WithMessage("*SomeMessage*")
                .WithMessage("*testParam*")
                .WithMessage("*arg0*")
                .WithMessage("*arg1*");

            Invoking(() => CheckValue(nullTask, "testParam", "SomeMessage-{0}{1}{2}", "arg0", "arg1", "arg2"))
                .Should()
                .ThrowExactly<ArgumentNullException>()
                .WithMessage("*SomeMessage*")
                .WithMessage("*testParam*")
                .WithMessage("*arg0*")
                .WithMessage("*arg1*")
                .WithMessage("*arg2*");

            Invoking(() => CheckValue(nullTask, "testParam", "SomeMessage-{0}{1}{2}{3}", "arg0", "arg1", "arg2", "arg3"))
                .Should()
                .ThrowExactly<ArgumentNullException>()
                .WithMessage("*SomeMessage*")
                .WithMessage("*testParam*")
                .WithMessage("*arg0*")
                .WithMessage("*arg1*")
                .WithMessage("*arg2*")
                .WithMessage("*arg3*");

            Invoking(() => CheckValue<object[]>(nullTask, "testParam", "SomeMessage-{0}", new object[] { "arg0" }))
                .Should()
                .ThrowExactly<ArgumentNullException>()
                .WithMessage("*SomeMessage*")
                .WithMessage("*testParam*")
                .WithMessage("*" + nameof(Object) + "[]*");
        }

        [TestMethod]
        public void CheckValue_NullTaskOfT_Throws()
        {
            Task<string>? nullTask = null;
            Invoking(() => CheckValue(nullTask, "testParam"))
                .Should()
                .ThrowExactly<ArgumentNullException>()
                .WithMessage("*testParam*");

            Invoking(() => CheckValue(task: nullTask, "testParam", "SomeMessage"))
                .Should()
                .ThrowExactly<ArgumentNullException>()
                .WithMessage("*SomeMessage*")
                .WithMessage("*testParam*");

            Invoking(() => CheckValue(task: nullTask, "testParam", "SomeMessage-{0}", "arg0"))
                .Should()
                .ThrowExactly<ArgumentNullException>()
                .WithMessage("*SomeMessage*")
                .WithMessage("*testParam*")
                .WithMessage("*arg0*");

            Invoking(() => CheckValue(task: nullTask, "testParam", "SomeMessage-{0}{1}", "arg0", "arg1"))
                .Should()
                .ThrowExactly<ArgumentNullException>()
                .WithMessage("*SomeMessage*")
                .WithMessage("*testParam*")
                .WithMessage("*arg0*")
                .WithMessage("*arg1*");

            Invoking(() => CheckValue(task: nullTask, "testParam", "SomeMessage-{0}{1}{2}", "arg0", "arg1", "arg2"))
                .Should()
                .ThrowExactly<ArgumentNullException>()
                .WithMessage("*SomeMessage*")
                .WithMessage("*testParam*")
                .WithMessage("*arg0*")
                .WithMessage("*arg1*")
                .WithMessage("*arg2*");

            Invoking(() => CheckValue(task: nullTask, "testParam", "SomeMessage-{0}{1}{2}{3}", "arg0", "arg1", "arg2", "arg3"))
                .Should()
                .ThrowExactly<ArgumentNullException>()
                .WithMessage("*SomeMessage*")
                .WithMessage("*testParam*")
                .WithMessage("*arg0*")
                .WithMessage("*arg1*")
                .WithMessage("*arg2*")
                .WithMessage("*arg3*");

            Invoking(() => CheckValue<string, object[]>(task: nullTask, "testParam", "SomeMessage-{0}", new object[] { "arg0" }))
                .Should()
                .ThrowExactly<ArgumentNullException>()
                .WithMessage("*SomeMessage*")
                .WithMessage("*testParam*")
                .WithMessage("*" + nameof(Object) + "[]*");
        }

        [TestMethod]
        public void CheckValue_WhenIncorrectMessageFormat_DoesNotThrowFormatException()
        {
            Invoking(() => CheckValue(null, "testParam", "{0}{1}{2}", "arg1"))
                .Should()
                .ThrowExactly<ArgumentNullException>();

            Invoking(() => CheckValue(null, "testParam", "{0}", "arg1", "arg2"))
                .Should()
                .ThrowExactly<ArgumentNullException>();
        }

        [TestMethod]
        public void CheckEqual_WhenEqualValueType_Success()
        {
            Invoking(() => CheckEqual(1, 1, "testParam")).Should().NotThrow();
        }

        [TestMethod]
        public void CheckEqual_WhenEqualValueType_ThrowArgumentException()
        {
            Invoking(() => CheckEqual(1, 2, "testParam"))
                .Should()
                .ThrowExactly<ArgumentException>()
                .WithMessage("*testParam does not have the expected value*");
        }

        [TestMethod]
        public void CheckEqual_WhenEqualRefType_Success()
        {
            Invoking(() => CheckEqual("a", "a", "testParam")).Should().NotThrow();
        }

        [TestMethod]
        public void CheckNotEqual_WhenNotEqualValueType_Success()
        {
            Invoking(() => CheckNotEqual(1, 2, "testParam")).Should().NotThrow();
        }

        [TestMethod]
        public void CheckNotEqual_WhenNotEqualValueType_ThrowArgumentException()
        {
            Invoking(() => CheckNotEqual(1, 1, "testParam"))
                .Should()
                .ThrowExactly<ArgumentException>()
                .WithMessage("*NOT to be '1'*");
        }

        [TestMethod]
        public void CheckNotEqual_WhenNotEqualRefType_Success()
        {
            Invoking(() => CheckEqual("a", "a", "testParam")).Should().NotThrow();
        }

        [TestMethod]
        public void CheckEqual_WhenRegValueType_ThrowArgumentException()
        {
            Invoking(() => CheckEqual("a", "b", "testParam"))
                .Should()
                .ThrowExactly<ArgumentException>()
                .WithMessage("*testParam does not have the expected value*");
        }
    }
}
