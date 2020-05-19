using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Astralis.Enumeration
{
	public abstract class Enumeration<TEnumeration, TValue> :
		IEquatable<Enumeration<TEnumeration, TValue>>,
		IComparable<Enumeration<TEnumeration, TValue>>
		where TEnumeration : Enumeration<TEnumeration, TValue>
		where TValue : IComparable<TValue>, IEquatable<TValue>
	{
		public TValue Value { get; private set; }

		protected Enumeration(TValue value)
		{
			Value = value ?? throw new ArgumentNullException(nameof(value));
		}

		public override int GetHashCode()
		{
			return Value.GetHashCode();
		}

		public override bool Equals(object other)
		{
			return Equals(other as Enumeration<TEnumeration, TValue>);
		}

		public bool Equals(Enumeration<TEnumeration, TValue> other)
		{
			return other != null && Value.Equals(other.Value);
		}

		public override string ToString()
		{
			return Value.ToString();
		}

		public int CompareTo(Enumeration<TEnumeration, TValue> other)
		{
			return Value.CompareTo(other.Value);
		}

		private static readonly Lazy<IEnumerable<TEnumeration>> Instances = new Lazy<IEnumerable<TEnumeration>>(() =>
		{
			return typeof(TEnumeration)
				.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
				.Where(x => typeof(TEnumeration).IsAssignableFrom(x.FieldType) &&
					x.GetValue(null) != null)
				.Select(x => x.GetValue(null))
				.Cast<TEnumeration>()
				.OrderBy(x => x.Value);
		});

		public static TEnumeration[] GetAll()
		{
			return Instances.Value.ToArray();
		}

		public static TEnumeration Parse(TValue value)
		{
			if (!TryParse(value, out var enumeration))
			{
				throw new ArgumentException($"'{value}' is not a valid value of type '{typeof(TEnumeration).Name}'.", nameof(value));
			}

			return enumeration;
		}

		public static bool TryParse(TValue id, out TEnumeration enumeration)
		{
			enumeration = Instances.Value.FirstOrDefault(x => x.Value.Equals(id));
			return enumeration != null;
		}

		public static bool operator ==(Enumeration<TEnumeration, TValue> left, Enumeration<TEnumeration, TValue> right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(Enumeration<TEnumeration, TValue> left, Enumeration<TEnumeration, TValue> right)
		{
			return !(left == right);
		}

		public static bool operator <(Enumeration<TEnumeration, TValue> left, Enumeration<TEnumeration, TValue> right)
		{
			return left.CompareTo(right) < 0;
		}

		public static bool operator >(Enumeration<TEnumeration, TValue> left, Enumeration<TEnumeration, TValue> right)
		{
			return left.CompareTo(right) > 0;
		}

		public static bool operator <=(Enumeration<TEnumeration, TValue> left, Enumeration<TEnumeration, TValue> right)
		{
			return left.CompareTo(right) <= 0;
		}

		public static bool operator >=(Enumeration<TEnumeration, TValue> left, Enumeration<TEnumeration, TValue> right)
		{
			return left.CompareTo(right) >= 0;
		}

		public static implicit operator TValue(Enumeration<TEnumeration, TValue> enumeration)
		{
			return enumeration.Value;
		}

		public static explicit operator Enumeration<TEnumeration, TValue>(TValue value)
		{
			if (!TryParse(value, out var enumeration))
			{
				throw new InvalidCastException($"Unable to cast object of type '{typeof(TValue)}' to type '{typeof(Enumeration<TEnumeration, TValue>)}'");
			}

			return enumeration;
		}
	}
}