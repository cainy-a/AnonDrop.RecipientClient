using System;
using System.Collections.Generic;
using System.Linq;

namespace AnonDrop.RecipientClient
{
	public static class ExtensionMethods
	{
		public static IEnumerable<IEnumerable<string>> Shuffle(this IEnumerable<IEnumerable<string>> enumerable,
		                                                       Random                                rand)
		{
			var items = enumerable as IEnumerable<string>[] ?? enumerable.ToArray();

			var working = new IEnumerable<string>[items.Length];
			foreach (var item in items)
			{
				var randomInt = -1;
				while (!(working.ElementAtOrDefault(randomInt) ?? Array.Empty<string>()).Any())
					randomInt = rand.Next(items.Length);

				working[randomInt] = item;
			}

			return working;
		}
	}
}