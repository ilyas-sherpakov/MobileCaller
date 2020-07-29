using System;
using System.Collections;
using System.IO;

namespace MobileCaller
{
    static class Logger
    {
        private const string LogFolderName = "Logs";

        public static string WorkingDirectory { get; set; }

        public static void Write(string text)
        {
            var logDirectory = Path.Combine(WorkingDirectory, LogFolderName);
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }

            try
            {
                using (var fileStream = new FileStream(Path.Combine(logDirectory, String.Format("Log{0:yyyy_MM_dd}.txt", DateTime.Now)),
                    FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read))
                {
                    using (var streamWriter = new StreamWriter(fileStream))
                    {
                        streamWriter.BaseStream.Seek(0, SeekOrigin.End);
                        streamWriter.WriteLine("{0:HH}:{0:mm}:{0:ss} => {1}", DateTime.Now, text);
                        streamWriter.Flush();
                    }
                }
            }
            catch (Exception)
            { 
                
            }
        }
        public static void Write(IEnumerable textList)
        {
            var logDirectory = Path.Combine(WorkingDirectory, LogFolderName);
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }

            try
            {
                using (var fileStream = new FileStream(Path.Combine(logDirectory, String.Format("Log{0:yyyy_MM_dd}.txt", DateTime.Now)),
                    FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read))
                {
                    using (var streamWriter = new StreamWriter(fileStream))
                    {
                        streamWriter.BaseStream.Seek(0, SeekOrigin.End);
                        foreach (var text in textList)
                        {
                            streamWriter.WriteLine("{0:HH}:{0:mm}:{0:ss} => {1}", DateTime.Now, text);
                        }
                        streamWriter.Flush();
                    }
                }
            }
            catch (Exception)
            { 
            }
        }
    }
}
