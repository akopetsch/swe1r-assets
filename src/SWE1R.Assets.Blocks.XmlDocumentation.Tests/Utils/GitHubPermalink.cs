// SPDX-License-Identifier: MIT

using Octokit;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace SWE1R.Assets.Blocks.Tests.XmlDocumentation.Utils
{
    public partial class GitHubPermalink
    {
        #region Fields

        private const char PathSeparator = '/';

        private static readonly GitHubClient GitHubClient = 
            new(new ProductHeaderValue(nameof(GitHubPermalink)));

        [GeneratedRegex(@"#L(\d+)")]
        private static partial Regex LineNumberRegex();

        #endregion

        #region Properties

        public string UriString { get; }

        public Uri Uri { get; }
        public string Host { get; }
        public string AccountName { get; }
        public string RepositoryName { get; }
        public string Ref { get; }
        public string[] FilePathSegments { get; }
        public int? LineNumber { get; }

        public string FilePath =>
            string.Join(PathSeparator, FilePathSegments);
        public string FileName =>
            FilePathSegments.Last();

        #endregion

        #region Constructor

        public GitHubPermalink(string uriString)
        {
            UriString = uriString;

            Uri = new Uri(uriString);
            Host = Uri.Host;
            string[] pathSegments = Uri.AbsolutePath.Split(
                PathSeparator, StringSplitOptions.RemoveEmptyEntries);
            AccountName = pathSegments[0];
            RepositoryName = pathSegments[1];
            Debug.Assert(pathSegments[2].Equals("blob"));
            Ref = pathSegments[3];
            Debug.Assert(pathSegments.Length >= 5);
            FilePathSegments = pathSegments.Skip(4).ToArray();

            if (Uri.Fragment != null)
                LineNumber = int.Parse(LineNumberRegex().Match(Uri.Fragment).Groups[1].Value);
        }

        #endregion

        #region Methods

        public string DownloadRawFile()
        {
            
            byte[] rawFileBytes = GitHubClient.Repository.Content.GetRawContentByRef(
                AccountName, RepositoryName, FilePath, Ref).Result;
            string rawFileString = Encoding.UTF8.GetString(rawFileBytes);
            return rawFileString;
        }

        public override string ToString() =>
            UriString;

        #endregion
    }
}
