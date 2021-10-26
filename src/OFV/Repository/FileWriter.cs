using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackSugar.Repository
{
    internal class Encode
    {
        public const int ENCODE_SHIFT_JIS = 0;
    }

    public interface IFileWriter
    {
        string GetFile();
        string GetLogFile();

        void Write(string path, string value);
        void Write(string path, Exception ex);
    }

    public class FileWriter : IFileWriter
    {
        private const string FILE_VALUE_TIMESTAMP = "$$timeStamp$$";

        private const string FILE_NAME = "OpenedFoler_$$timeStamp$$.json";

        private const string FILE_LOG = "err_$$timeStamp$$.log";

        public string GetFile()
        {
            return FILE_NAME.Replace(FILE_VALUE_TIMESTAMP, DateTime.UtcNow.ToString("yyyyMMddhhmmssfff"));
        }

        public void Write(string path, string value)
        {
            using (var writer = new StreamWriter(path, false, Encoding.GetEncoding(Encode.ENCODE_SHIFT_JIS)))
            {
                writer.Write(value);
                writer.Close();
            }
        }

        public string GetLogFile()
        {
            return FILE_LOG.Replace(FILE_VALUE_TIMESTAMP, DateTime.UtcNow.ToString("yyyyMMddhhmmssfff"));
        }

        public void Write(string path, Exception ex)
        {
            Write(path, ex.Message + "\n" + ex.StackTrace);
        }
    }
}
