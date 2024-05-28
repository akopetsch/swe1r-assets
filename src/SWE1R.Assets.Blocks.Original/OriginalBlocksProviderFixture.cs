// Copyright 2024 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

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
