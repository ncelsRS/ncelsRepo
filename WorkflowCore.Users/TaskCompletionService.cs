using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WorkflowCore.Users
{
    public class TaskCompletionService
    {
        private class Awaiter : TaskCompletionSource<string>
        {
            public int Count { get; set; }

            public Awaiter() : base()
            {
                Count = 0;
            }
        }

        private static readonly Dictionary<string, Awaiter> _source = new Dictionary<string, Awaiter>();

        public static Task<string> AddTask(string key)
        {
            lock (_source)
            {
                var hasKey = _source.ContainsKey(key);
                if (hasKey)
                {
                    _source[key].Count++;
                    return _source[key].Task;
                }
                else
                {
                    var awaiter = new Awaiter();
                    _source.Add(key, awaiter);
                    return awaiter.Task;
                }
            }
        }

        public static void TryReleaseTask(string key, string result = null)
        {
            if (result == null) result = key;
            lock (_source)
            {
                var hasKey = _source.ContainsKey(key);
                if (!hasKey) return;
                var awaiter = _source[key];
                awaiter.Count--;
                if (awaiter.Count > 0) return;
                _source.Remove(key);
                awaiter.SetResult(result);
            }
        }

        public static void ReleaseAll(string key, string result = null)
        {
            if (result == null) result = key;
            lock (_source)
            {
                var hasKey = _source.ContainsKey(key);
                if (!hasKey) return;
                var awaiter = _source[key];
                awaiter.Count--;
                _source.Remove(key);
                awaiter.SetResult(result);
            }
        }
    }
}