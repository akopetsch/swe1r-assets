// SPDX-License-Identifier: GPL-2.0-only

namespace SWE1R.Assets.Blocks.Original.SQLite.Tests
{
    public sealed class AssetsDbContextFixture : IDisposable
    {
        public AssetsDbContext AssetsDbContext { get; } = new();

        public void Dispose() => AssetsDbContext.Dispose();
    }
}
