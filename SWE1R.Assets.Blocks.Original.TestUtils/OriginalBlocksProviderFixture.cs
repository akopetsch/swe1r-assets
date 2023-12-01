// Copyright 2023 SWE1R.Assets Maintainers
// Licensed under GPLv2 or any later version
// Refer to the included LICENSE.txt file.

namespace SWE1R.Assets.Blocks.Original.TestUtils
{
    public class OriginalBlocksProviderFixture : IDisposable
    {
        public OriginalBlocksProvider OriginalBlocksProvider { get; } = new();

        public OriginalBlocksProviderFixture()
        {
            OriginalBlocksProvider.Init();
        }

        public void Dispose() { }
    }
}
