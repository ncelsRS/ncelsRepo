using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Teme.Contract.Infrastructure
{
    public class TaskCompletionService
    {
        private static readonly ConcurrentDictionary<string, TaskCompletionSource<string>> _source = new ConcurrentDictionary<string, TaskCompletionSource<string>>();

        public static Task<string> AddTask(string key)
        {
            var tcs = new TaskCompletionSource<string>();
            if (!_source.TryAdd(key, tcs))
                throw new Exception($"TaskCompletionService: key {key} already exists");
            return tcs.Task;
        }

        public static void ReleaseTask(string key, string result = null)
        {
            if (!_source.TryRemove(key, out TaskCompletionSource<string> tcs))
                throw new Exception($"TaskCompletionService: key {key} not exists");
            tcs.SetResult(result);
        }

        public static bool TryReleaseTask(string key, string result = null)
        {
            if (!_source.TryRemove(key, out TaskCompletionSource<string> tcs))
                return false;
            tcs.SetResult(result);
            return true;
        }
    }
}
