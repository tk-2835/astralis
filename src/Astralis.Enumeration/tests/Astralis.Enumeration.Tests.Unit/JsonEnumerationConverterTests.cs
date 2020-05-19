using System.Linq;
using System.Text.Json;
using Astralis.Enumeration.Serialization;
using Xunit;

namespace Astralis.Enumeration.Tests.Unit
{
	public class JsonEnumerationConverterTests
	{
		public static readonly TheoryData<string, object> Serialize_Should_Return_Expected_TheoryData = new TheoryData<string, object>()
		{
			{ "1", Color.Red },
			{ "2", Color.Green },
			{ "3", Color.Blue },
			{ "\"u\"", Direction.Up },
			{ "\"d\"", Direction.Down },
			{ "\"l\"", Direction.Left },
			{ "\"r\"", Direction.Right },
			{ "{\"Color\":1,\"Direction\":\"u\"}", new Model() { Color = Color.Red, Direction = Direction.Up } },
			{ "{\"Color\":null,\"Direction\":null}", new Model() { Color = null, Direction = null } }
		};

		[Theory]
		[MemberData(nameof(Serialize_Should_Return_Expected_TheoryData))]
		public void Serialize_Should_Return_Expected(string expected, object enumeration)
		{
			var options = new JsonSerializerOptions()
			{
				Converters =
				{
					new JsonEnumerationConverter<Color, int>(),
					new JsonEnumerationConverter<Direction, string>()
				}
			};

			var actual = new string(JsonSerializer.Serialize(enumeration, options).Where(x => !char.IsWhiteSpace(x)).ToArray());

			Assert.Equal(expected, actual);
		}

		public static readonly TheoryData<object, string> Deserialize_Should_Return_Expected_TheoryData = new TheoryData<object, string>()
		{
			{ Color.Red, "1" },
			{ Color.Green, "2" },
			{ Color.Blue, "3" },
			{ Direction.Up, "\"u\"" },
			{ Direction.Down, "\"d\"" },
			{ Direction.Left, "\"l\"" },
			{ Direction.Right, "\"r\"" },
			{ new Model() { Color = Color.Red, Direction = Direction.Up }, "{\"Color\":1,\"Direction\":\"u\"}" },
			{ new Model() { Color = null, Direction = null }, "{\"Color\":null,\"Direction\":null}" }
		};

		[Theory]
		[MemberData(nameof(Deserialize_Should_Return_Expected_TheoryData))]
		public void Deserialize_Should_Return_Expected(object expected, string json)
		{
			var options = new JsonSerializerOptions()
			{
				Converters =
				{
					new JsonEnumerationConverter<Color, int>(),
					new JsonEnumerationConverter<Direction, string>()
				}
			};

			var actual = JsonSerializer.Deserialize(json, expected.GetType(), options);

			Assert.Equal(expected, actual);
		}

		public class Model
		{
			public Color Color { get; set; }
			public Direction Direction { get; set; }

			public override bool Equals(object other)
			{
				return other is Model model &&
					model.Color == Color &&
					model.Direction == Direction;
			}
		}

		public sealed class Color : Enumeration<Color, int>
		{
			public static readonly Color Red = new Color(1);
			public static readonly Color Green = new Color(2);
			public static readonly Color Blue = new Color(3);

			private Color(int value) : base(value)
			{
			}
		}

		public sealed class Direction : Enumeration<Direction, string>
		{
			public static readonly Direction Up = new Direction("u");
			public static readonly Direction Down = new Direction("d");
			public static readonly Direction Left = new Direction("l");
			public static readonly Direction Right = new Direction("r");

			private Direction(string value) : base(value)
			{
			}
		}
	}
}