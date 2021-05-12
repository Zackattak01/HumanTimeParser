using System.Collections.Generic;

namespace HumanTimeParser.Core.Extensions
{
    /// <summary>
    /// Useful dictionary extensions
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Adds all the specified key with the specified value.
        /// </summary>
        /// <param name="dict">The dictionary to perform the operation on.</param>
        /// <param name="collection">The collection of keys</param>
        /// <param name="value">The value for all the key</param>
        /// <typeparam name="TKey">The type of the key</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        public static void AddKeyRange<TKey, TValue>(this IDictionary<TKey, TValue> dict, IEnumerable<TKey> collection, TValue value)
        {
            foreach (var item in collection)
            {
                dict.Add(item, value);
            }
        }
    }
}