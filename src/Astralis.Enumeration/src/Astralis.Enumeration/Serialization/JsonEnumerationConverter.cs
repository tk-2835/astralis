using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Astralis.Enumeration.Serialization
{
	public class JsonEnumerationConverter<TEnumeration, TValue> : JsonConverter<TEnumeration>
		where TEnumeration : Enumeration<TEnumeration, TValue>
		where TValue : IComparable<TValue>, IEquatable<TValue>
	{
		public override TEnumeration Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var converter = options.GetConverter(typeof(TValue)) as JsonConverter<TValue>;

			if (converter == null)
			{
				throw new JsonException();
			}

			var value = converter.Read(ref reader, typeof(TValue), options);

			return Enumeration<TEnumeration, TValue>.Parse(value);
		}

		public override void Write(Utf8JsonWriter writer, TEnumeration value, JsonSerializerOptions options)
		{
			var converter = options.GetConverter(typeof(TValue)) as JsonConverter<TValue>;

			if (converter == null)
			{
				throw new JsonException();
			}

			converter.Write(writer, value.Value, options);
		}
	}
}