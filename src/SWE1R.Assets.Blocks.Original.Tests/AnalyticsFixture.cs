// SPDX-License-Identifier: MIT

using System.Diagnostics;

namespace SWE1R.Assets.Blocks.Original.Tests
{
    public class AnalyticsFixture : IDisposable
    {
        public Dictionary<object, int> CounterByObject { get; }
            = new Dictionary<object, int>();

        public AnalyticsFixture()
        {

        }

        public int GetCounter(object key) =>
            CounterByObject.GetValueOrDefault(key);

        public void SetCounter(object key, int counter) =>
            CounterByObject[key] = counter;

        public void IncreaseCounter(object key)
        {
            if (!CounterByObject.ContainsKey(key))
                CounterByObject.Add(key, default);
            CounterByObject[key]++;
        }

        public void SetCounterMax(object key, int max)
        {
            int existingMax = GetCounter(key);
            if (max > existingMax)
                SetCounter(key, max);
        }

        public void Dispose()
        {
            Debug.WriteLine(nameof(AnalyticsFixture));
            List<object> keys = CounterByObject.Keys.OrderBy(x => x).ToList();
            foreach (object key in keys)
            {
                int counter = CounterByObject[key];
                Debug.WriteLine($"{key} | {counter}");
            }
        }
    }
}
