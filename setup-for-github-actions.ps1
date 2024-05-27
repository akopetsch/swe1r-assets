# Create a global.json file to specify the required .NET SDK version.
# This file is used by GitHub Actions to install the required .NET SDK version.
# inspired by:
# https://www.meziantou.net/publishing-a-nuget-package-following-best-practices-using-github.htm
dotnet new globaljson --roll-forward feature --force
