using System.Collections.Generic;

namespace HumanTimeParser.English.Extensions
{
    internal static class DictionaryExtensions
    {
        public static void AddKeyRange<TKey, TValue>(this IDictionary<TKey, TValue> dict, IEnumerable<TKey> collection, TValue value)
        {
            foreach (var item in collection)
            {
                dict.Add(item, value);
            }
        }
    }
}