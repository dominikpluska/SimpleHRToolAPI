
namespace SimpleHRToolAPI.FileMethods
{
    public class FileDownloader : IFileDownloader
    {
        private readonly string _filePath;

        public FileDownloader(string filePath)
        {
            _filePath = filePath;
        }

        public Task<byte[]> GetFileBytes()
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(_filePath);
            return Task.FromResult(fileBytes);
        }
    }
}
