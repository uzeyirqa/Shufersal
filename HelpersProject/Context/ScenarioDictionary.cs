using HelpersProject.Helpers;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
namespace HelperProject.Context
{
    /// <summary>
    /// Class for imitation the functional of Scenario Context .
    /// </summary>
    public class ScenarioDictionary
    {
        /// <summary>
        /// Dictionary for storage other Dictionaries by the key Id of Thread.
        /// </summary>
        private static readonly Dictionary<int, Dictionary<string, object>> _dictionary
            = new Dictionary<int, Dictionary<string, object>>();

        /// <summary>
        /// Dictionary for the current thread.
        /// </summary>
        public static Dictionary<string, object> Current
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                var currentThreadId = Thread.CurrentThread.ManagedThreadId;

                if (!_dictionary.ContainsKey(currentThreadId))
                    _dictionary[currentThreadId] = new Dictionary<string, object>();

                return _dictionary[currentThreadId];
            }
        }

        /// <summary>
        /// Clears the dictionary of current thread.
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void Clear() =>
            _dictionary[Thread.CurrentThread.ManagedThreadId].Clear();

        /// <summary>
        /// Adds the value to the dictionary of current thread.
        /// If value with key already existed then replace on a new value.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void Add(string key, object value)
        {
            var currentThreadId = Thread.CurrentThread.ManagedThreadId;

            if (!_dictionary.ContainsKey(currentThreadId))
                _dictionary[currentThreadId] = new Dictionary<string, object>();

            _dictionary[currentThreadId][key] = value;
        }

        /// <summary>
        /// Checks is key exist in the dictionary of current thread.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static bool IsKeyExist(string key) =>
            _dictionary.TryGetValue(Thread.CurrentThread.ManagedThreadId, out var dictionaryOfCurrentThread) && dictionaryOfCurrentThread.ContainsKey(key);

        /// <summary>
        /// Gets the value by the key from the dictionary of current thread.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static T Get<T>(string key) =>
            (T)_dictionary[Thread.CurrentThread.ManagedThreadId][key];

        /// <summary>
        /// The method tries to check is key exist in the dictionary of current thread.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value">If key does not exist, then return default value of T.</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static bool TryGetValue<T>(string key, out T value)
        {
            bool result;

            try
            {
                result = _dictionary[Thread.CurrentThread.ManagedThreadId].TryGetValue(key, out var objectValue);
                value = objectValue.IsNull() ? default : (T)objectValue;
            }
            catch
            {
                result = false;
                value = default;
            }

            return result;
        }
    }
}