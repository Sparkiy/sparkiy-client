using System;
using System.Collections.Generic;

namespace SparkiyClient.Common.Helpers
{
	public static class DictionaryExtensions
	{
		/// <summary>
		/// Adds the range of key/value pairs to the collection.
		/// </summary>
		/// <typeparam name="TKey">The type of the key.</typeparam>
		/// <typeparam name="TValue">The type of the value.</typeparam>
		/// <param name="self">The dictionary.</param>
		/// <param name="values">The values.</param>
		/// <exception cref="System.ArgumentNullException">values; one or more keys are null</exception>
		/// <exception cref="System.ArgumentException">An element with the same key already exists in the dictionary.</exception>
		public static void AddRange<TKey, TValue>(
			this Dictionary<TKey, TValue> self,
			IEnumerable<KeyValuePair<TKey, TValue>> values)
		{
			foreach (var kvp in values)
				if (kvp.Key == null)
					throw new ArgumentNullException("values", "one or more keys are null");
				else if (self.ContainsKey(kvp.Key))
					throw new ArgumentException("An element with the same key already exists in the dictionary.");
				else self[kvp.Key] = kvp.Value;
		}
	}
}