// SPDX-License-Identifier: MIT

using System.Linq;

namespace SWE1R.Assets.Blocks.TestApp.ItemListers
{
    public class MarkdownTableHelper
    {
        #region Fields

        private const string columnSeparator = " | ";

        #endregion

        #region Properties

        public string[] ColumnNames { get; }
        private int[] ColumnWidths { get; }
        public string HeaderRow { get; }
        public string SeparatorRow { get; }

        #endregion

        #region Constructor

        public MarkdownTableHelper(params string[] columnNames)
        {
            ColumnNames = columnNames;

            ColumnWidths = columnNames.Select(x => x.Length).ToArray();
            HeaderRow = GetRowString(ColumnNames);
            SeparatorRow = GetRowString(ColumnWidths.Select(w => new string('-', w)).ToArray());
        }

        #endregion

        #region Methods

        public string GetRowString(string[] row)
        {
            var padded = new string[row.Length];
            for (int i = 0; i < row.Length; i++)
                padded[i] = row[i].PadRight(ColumnWidths[i]);
            return $"{columnSeparator}{string.Join(columnSeparator, padded)}{columnSeparator}";
        }

        #endregion
    }
}
