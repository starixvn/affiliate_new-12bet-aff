namespace Fanex.Affiliate.DataService.CheckSum.Utilities
{
    using System.IO;

    public class FileSystem : IFileSystem
    {
        public string ReadFile(string path)
        {
            using (var reader = new StreamReader(path))
            {
                return reader.ReadToEnd();
            }
        }
    }
}