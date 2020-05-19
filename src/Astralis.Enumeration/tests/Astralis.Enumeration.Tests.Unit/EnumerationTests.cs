using System;
using Xunit;

namespace Astralis.Enumeration.Tests.Unit
{
	public class EnumerationTests
	{
		public static readonly TheoryData<First, int> Parse_Should_Return_Enumeration_TheoryData = new TheoryData<First, int>()
		{
			{ First.One, 1 },
			{ First.Two, 2 },
			{ First.Three, 3 }
		};

		[Theory]
		[MemberData(nameof(Parse_Should_Return_Enumeration_TheoryData))]
		public void Parse_Should_Return_Expected(First expected, int value)
		{
			Assert.Equal(expected, First.Parse(value));
		}

		[Fact]
		public void Parse_Should_Throw_ArgumentExcpetion()
		{
			Assert.Throws<ArgumentException>(() => First.Parse(0));
		}

		public static readonly TheoryData<bool, First, int> TryParse_Should_Return_Expected_TheoryData = new TheoryData<bool, First, int>()
		{
			{ false, null, 0 },
			{ true, First.One, 1 },
			{ true, First.Two, 2 },
			{ true, First.Three, 3 }
		};

		[Theory]
		[MemberData(nameof(TryParse_Should_Return_Expected_TheoryData))]
		public void TryParse_Should_Return_Expected(bool expected, First expectedEnumeration, int value)
		{
			Assert.Equal(expected, First.TryParse(value, out var enumeration));
			Assert.Equal(expectedEnumeration, enumeration);
		}

		[Fact]
		public void GetAll_Should_Return_Expected()
		{
			Assert.Equal(
				new First[] { First.One, First.Two, First.Three },
				First.GetAll());
		}

		public static readonly TheoryData<bool, object, object> Equals_Should_Return_Expected_TheoryData = new TheoryData<bool, object, object>()
		{
			{ true, First.One, First.One },
			{ false, First.One, null },
			{ false, First.One, 1 },
			{ false, First.One, "lorem ipsum" },
			{ false, First.One, Guid.NewGuid() },
			{ false, First.One, First.Two },
			{ false, First.One, Second.One }
		};

		[Theory]
		[MemberData(nameof(Equals_Should_Return_Expected_TheoryData))]
		public void Equals_Should_Return_Expected(bool expected, object left, object right)
		{
			Assert.Equal(expected, left?.Equals(right));
		}

		public static readonly TheoryData<int, Second, Second> CompareTo_Shoud_Return_Expected_TheoryData = new TheoryData<int, Second, Second>()
		{
			{ -1, Second.Two, Second.Three },
			{ 0, Second.Two, Second.Two },
			{ 1, Second.Two, Second.One }
		};

		[Theory]
		[MemberData(nameof(CompareTo_Shoud_Return_Expected_TheoryData))]
		public void CompareTo_Shoud_Return_Expected(int expected, Second left, Second right)
		{
			Assert.Equal(expected, left.CompareTo(right));
		}

		public static readonly TheoryData<bool, object, object> Equal_Operator_Should_Return_Expected_TheoryData = new TheoryData<bool, object, object>()
		{
			{ true, First.One, First.One },
			{ false, First.One, null },
			{ false, First.One, 1 },
			{ false, First.One, "lorem ipsum" },
			{ false, First.One, Guid.NewGuid() },
			{ false, First.One, First.Two },
			{ false, First.One, Second.One }
		};

		[Theory]
		[MemberData(nameof(Equal_Operator_Should_Return_Expected_TheoryData))]
		public void Equal_Operator_Should_Return_Expected(bool expected, object left, object right)
		{
			Assert.Equal(expected, left as First == right as First);
		}

		public static readonly TheoryData<bool, object, object> Not_Equal_Operator_Should_Return_Expected_TheoryData = new TheoryData<bool, object, object>()
		{
			{ false, First.One, First.One },
			{ true, First.One, null },
			{ true, First.One, 1 },
			{ true, First.One, "lorem ipsum" },
			{ true, First.One, Guid.NewGuid() },
			{ true, First.One, First.Two },
			{ true, First.One, Second.One }
		};

		[Theory]
		[MemberData(nameof(Not_Equal_Operator_Should_Return_Expected_TheoryData))]
		public void Not_Equal_Operator_Should_Return_Expected(bool expected, object left, object right)
		{
			Assert.Equal(expected, left as First != right as First);
		}

		public static readonly TheoryData<bool, First, First> Greater_Than_Operator_Should_Return_Expected_TheoryData = new TheoryData<bool, First, First>()
		{
			{ true, First.Two, First.One },
			{ false, First.Two, First.Two },
			{ false, First.Two, First.Three }
		};

		[Theory]
		[MemberData(nameof(Greater_Than_Operator_Should_Return_Expected_TheoryData))]
		public void Greater_Than_Operator_Should_Return_Expected(bool expected, First left, First right)
		{
			Assert.Equal(expected, left > right);
		}

		public static readonly TheoryData<bool, First, First> Greater_Than_Or_Equal_Operator_Should_Return_Expected_TheoryData = new TheoryData<bool, First, First>()
		{
			{ true, First.Two, First.One },
			{ true, First.Two, First.Two },
			{ false, First.Two, First.Three }
		};

		[Theory]
		[MemberData(nameof(Greater_Than_Or_Equal_Operator_Should_Return_Expected_TheoryData))]
		public void Greater_Than_Or_Equal_Operator_Should_Return_Expected(bool expected, First left, First right)
		{
			Assert.Equal(expected, left >= right);
		}

		public static readonly TheoryData<bool, First, First> Less_Than_Operator_Should_Return_Expected_TheoryData = new TheoryData<bool, First, First>()
		{
			{ false, First.Two, First.One },
			{ false, First.Two, First.Two },
			{ true, First.Two, First.Three }
		};

		[Theory]
		[MemberData(nameof(Less_Than_Operator_Should_Return_Expected_TheoryData))]
		public void Less_Than_Operator_Should_Return_Expected(bool expected, First left, First right)
		{
			Assert.Equal(expected, left < right);
		}

		public static readonly TheoryData<bool, First, First> Less_Than_Or_Equal_Operator_Should_Return_Expected_TheoryData = new TheoryData<bool, First, First>()
		{
			{ false, First.Two, First.One },
			{ true, First.Two, First.Two },
			{ true, First.Two, First.Three }
		};

		[Theory]
		[MemberData(nameof(Less_Than_Or_Equal_Operator_Should_Return_Expected_TheoryData))]
		public void Less_Than_Or_Equal_Operator_Should_Return_Expected(bool expected, First left, First right)
		{
			Assert.Equal(expected, left <= right);
		}

		public static readonly TheoryData<int, First> Implicit_Operator_Should_Return_Exptected_TheoryData = new TheoryData<int, First>()
		{
			{ 1, First.One },
			{ 2, First.Two },
			{ 3, First.Three }
		};

		[Theory]
		[MemberData(nameof(Implicit_Operator_Should_Return_Exptected_TheoryData))]
		public void Implicit_Operator_Should_Return_Exptected(int expected, First enumeration)
		{
			Assert.Equal(expected, enumeration);
		}

		public static readonly TheoryData<First, int> Explicit_Operator_Should_Return_Exptected_TheoryData = new TheoryData<First, int>()
		{
			{ First.One, 1 },
			{ First.Two, 2 },
			{ First.Three, 3 }
		};

		[Theory]
		[MemberData(nameof(Explicit_Operator_Should_Return_Exptected_TheoryData))]
		public void Explicit_Operator_Should_Return_Exptected(First expected, int value)
		{
			Assert.Equal(expected, (First)value);
		}

		[Fact]
		public void Explicit_Operator_Should_Throw_InvalidCastException()
		{
			Assert.Throws<InvalidCastException>(() => (First)0);
		}

		public sealed class First : Enumeration<First, int>
		{
			public static readonly First One = new First(1);
			public static readonly First Two = new First(2);
			public static readonly First Three = new First(3);

			private First(int value) : base(value)
			{
			}
		}

		public sealed class Second : Enumeration<Second, int>
		{
			public static readonly Second One = new Second(1);
			public static readonly Second Two = new Second(2);
			public static readonly Second Three = new Second(3);

			private Second(int value) : base(value)
			{
			}
		}
	}
}