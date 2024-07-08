
namespace SimpleHRToolAPI.FileMethods
{
    public class UploadFormFile : IFileUploader
    {
        private readonly string _bucketPath;
        private readonly IFormFile _file;

        public UploadFormFile(string BucketList, IFormFile File)
        {
            _bucketPath = BucketList;
            _file = File; 
        }
        public async Task<string> UploadFile()
        {
            string filePath = Path.Combine(_bucketPath, _file.Name);
            DeleteFileIfExist(filePath);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await _file.CopyToAsync(stream);
            }
            return filePath;
        }


        private static void DeleteFileIfExist(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            else
            {
                return;
            }
        }


    }
}
