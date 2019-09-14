﻿using System.Threading;
using System.Threading.Tasks;

namespace StdOttStandard
{
    public class AsyncResult<TOut, TIn>
    {
        private readonly SemaphoreSlim sem;
        private TOut result;

        public Task<TOut> Task { get; }

        public TIn Input { get; }

        public TOut Result => Task.Result;

        public AsyncResult(TIn input)
        {
            Input = input;

            sem = new SemaphoreSlim(0);
            Task = GetValue();
        }

        private async Task<TOut> GetValue()
        {
            await sem.WaitAsync();

            return result;
        }

        public void SetValue(TOut value)
        {
            result = value;

            sem.Release();
        }
    }
}