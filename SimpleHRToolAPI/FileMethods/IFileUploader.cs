namespace SimpleHRToolAPI.FileMethods
{
    public interface IFileUploader
    {
        public Task<string> UploadFile();
    }
}
