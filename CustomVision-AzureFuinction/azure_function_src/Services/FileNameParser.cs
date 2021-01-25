using System.Text.RegularExpressions;
using CustomVisionLibraryApproach.Interfaces;

namespace CustomVisionLibraryApproach.Services
{
    public class FileNameParser : IFileNameParser
    {
        public string ParseFileName(string fileName)
        {
            Regex exFileName = new Regex(@"^(?<tagName>.+)\s", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Multiline);
            Match mFileName = exFileName.Match(fileName);
            return mFileName.Groups["tagName"].Value;
        }
    }
}