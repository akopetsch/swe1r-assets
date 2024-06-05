// SPDX-License-Identifier: MIT

using SWE1R.Assets.Blocks.Tests.XmlDocumentation.Utils;
using SWE1R.Assets.Blocks.XmlDocumentation.Tests.Utils;
using System.Xml.Linq;

namespace SWE1R.Assets.Blocks.XmlDocumentation.Tests.ElementValidation.Links
{
    public class GitHubPermalinkElementValidator : LinkElementValidator
    {
        public GitHubPermalink Permalink { get; }

        public GitHubPermalinkElementValidator(XElement seeElement) :
            base(seeElement)
        {
            Permalink = new GitHubPermalink(Href);
        }

        public override void Validate()
        {
            ValidatePermalink();
            ValidateTextSplit();
            //ValidateSymbolName();
        }

        private void ValidatePermalink()
        {
            Assert.Equal("github.com", Permalink.Host);
            Assert.Equal("akopetsch", Permalink.AccountName);
            var allowedRepositoryNames = new string[] { "SW_RACER_RE", "Sw_Racer" };
            Assert.True(allowedRepositoryNames.Contains(Permalink.RepositoryName),
                nameof(Permalink.RepositoryName));
        }

        private void ValidateTextSplit()
        {
            Assert.Equal(4, TextSplit.Length);
            Assert.Equal(Permalink.Host, TextSplit[0]);
            Assert.Equal($"{Permalink.AccountName}/{Permalink.RepositoryName}", TextSplit[1]);
            Assert.Equal(Permalink.FilePathSegments.Last(), TextSplit[2]);
        }

        private void ValidateSymbolName()
        {
            string rawFileContent = Permalink.DownloadRawFile();
            string line = TextHelper.GetLineByNumber(rawFileContent, Permalink.LineNumber.Value);
            string symbolName = TextSplit.Last();
            Assert.Contains(symbolName, line);
        }
    }
}
