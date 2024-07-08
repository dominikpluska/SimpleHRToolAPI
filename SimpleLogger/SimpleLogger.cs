using System.IO;

namespace SimpleLogger
{
    public class SimpleLogger : ILogger
    {

        private readonly string _date = DateTime.Now.ToString("HH:mm");
        private readonly string _path;


        public SimpleLogger(string path)
        {
            _path = path;
        }

        public void Log(string logContent, string fileName)
        {
            if (File.Exists(_path))
            {
                File.AppendAllText(_path, _date + " " + logContent + Environment.NewLine);
            }
            else if (!File.Exists(_path))
            {
                File.Create($@"{_path}\{fileName}").Dispose();
                File.AppendAllText(_path, _date + " " +  logContent + Environment.NewLine);
            }

        }
    }
}
