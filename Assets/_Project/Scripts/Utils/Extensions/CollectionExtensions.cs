using System;
using System.Collections.Generic;
using System.Linq;

namespace Utils
{
	public static class CollectionExtensions
	{
		public static T LastValue<T>(this IList<T> list)
		{
			return list.Count > 0 ? list[^1] : default;
		}

		public static T FirstValue<T>(this IList<T> list)
		{
			return list.Count > 0 ? list[0] : default;
		}

		public static IList<T> Inverted<T>(this IList<T> list)
		{
			var inverted = new List<T>(list.Count);

			for (int i = list.Count - 1; i >= 0; i--)
			{
				inverted.Add(list[i]);
			}

			return inverted;
		}

		public static T PopFirst<T>(this IList<T> list)
		{
			if (list.Count == 0) return default(T);
			var item = list[0];
			list.RemoveAt(0);
			return item;
		}

		public static T LastValue<T>(this T[] list)
		{
			return list.Length > 0 ? list[^1] : default;
		}

		public static T[] Copy<T>(this T[] sourceArray, int sourceIndex, int length)
		{
			var copy = new T[length];
			Array.Copy(sourceArray, sourceIndex, copy, 0, length);
			return copy;
		}

		public static IList<T> Copy<T>(this IEnumerable<T> list)
		{
			return new List<T>(list);
		}
		
		public static IList<T> Shuffle<T>(this IList<T> list)  
		{  
			var rng = new System.Random();  
			var n = list.Count;
			while (n > 1)
			{
				n--;
				var k = rng.Next(n + 1);
				(list[k], list[n]) = (list[n], list[k]);
			}

			return list;
		}
		
		public static T GetRandom<T>(this IEnumerable<T> list)
		{
			var random = new System.Random();
			var enumerable = list.ToList();
			var index = random.Next(enumerable.Count);
			return enumerable[index];
		}
	}
}