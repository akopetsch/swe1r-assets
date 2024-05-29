// SPDX-License-Identifier: GPL-2.0-only

namespace SWE1R.Assets.Blocks.Original
{
    public class OriginalBlocksProviderFixture : IDisposable
    {
        public OriginalBlocksProvider Provider { get; } = new();

        public OriginalBlocksProviderFixture() =>
            Provider.Load();

        public void Dispose() { }
    }
}
