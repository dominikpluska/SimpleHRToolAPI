namespace SimpleHRToolAPI.FileMethods
{
    public interface IFileDownloader
    {
        public Task<byte[]> GetFileBytes();
    }
}
