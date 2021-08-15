using HelpersProject.Helpers;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

namespace HelperProject.Context
{
    /// <summary>
    /// Class for imitation the functional of Feature Context.
    /// </summary>
    public class FeatureDictionary
    {
        /// <summary>
        /// Dictionaries for all threads.
        /// </summary>
        public static Dictionary<int, Dictionary<string, object>> All
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get;
        } = new Dictionary<int, Dictionary<string, object>>();

        /// <summary>
        /// Dictionary for the current thread.
        /// </summary>
        public static Dictionary<string, object> Current
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                var currentThreadId = Thread.CurrentThread.ManagedThreadId;

                if (!All.ContainsKey(currentThreadId))
                    All[currentThreadId] = new Dictionary<string, object>();

                return All[currentThreadId];
            }
        }

        /// <summary>
        /// Clears the dictionary of current thread.
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void Clear() =>
            All[Thread.CurrentThread.ManagedThreadId].Clear();

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

            if (!All.ContainsKey(currentThreadId))
                All[currentThreadId] = new Dictionary<string, object>();

            All[currentThreadId][key] = value;
        }

        /// <summary>
        /// Checks is key exist in the dictionary of current thread.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static bool IsKeyExist(string key) =>
            All.TryGetValue(Thread.CurrentThread.ManagedThreadId, out var dictionaryOfCurrentThread) && dictionaryOfCurrentThread.ContainsKey(key);

        /// <summary>
        /// Gets the value by the key from the dictionary of current thread.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static T Get<T>(string key) =>
            (T)All[Thread.CurrentThread.ManagedThreadId][key];


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
                result = All[Thread.CurrentThread.ManagedThreadId].TryGetValue(key, out var objectValue);
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
