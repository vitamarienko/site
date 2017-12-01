using System;
using System.Configuration;
using System.IO;

namespace site.core
{
    public class FileSystemUtil
    {
        private static string appDataPath;

        public static string AppDataPath
        {
            get
            {
                if (string.IsNullOrEmpty(appDataPath))
                {
                    appDataPath = AppDomain.CurrentDomain.GetData("DataDirectory").ToString();
                }

                return appDataPath;
            }
        }

        public static bool PathExists(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return false;
            }

            return Directory.Exists(path);
        }
    }
}