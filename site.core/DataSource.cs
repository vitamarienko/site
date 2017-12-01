using site.core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace site.core
{
    public class DataSource
    {
        private static string appDataDir;

        public DataSource()
        {
            if (string.IsNullOrEmpty(appDataDir))
            {
                appDataDir = FileSystemUtil.AppDataPath;
            }
        }

        private string GetSetPath(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            var setPath = Path.Combine(appDataDir, id);

            return setPath;
        }

        private ImgSet ReadSetData(string pathToFiles)
        {
            var pathToData = Path.Combine(pathToFiles, Constants.DataFileName);
            var dataContents = File.ReadAllText(pathToData);

            return dataContents.FromJson<ImgSet>();
        }

        private IEnumerable<Img> ReadSetImages(string pathToFiles)
        {
            var filePaths = Directory.GetFiles(pathToFiles);
            var itemPaths = filePaths.Where(e => !e.EndsWith(Constants.DataFileName));

            foreach (var path in itemPaths)
            {
                yield return new Img
                {
                    Id = Path.GetFileName(path),
                    Base64Data = Convert.ToBase64String(File.ReadAllBytes(path))
                };
            }
        }

        public ImgSet GetSet(string id)
        {
            var pathToFiles = GetSetPath(id);

            if (!string.IsNullOrEmpty(pathToFiles) && !FileSystemUtil.PathExists(pathToFiles))
            {
                return ImgSet.Empty;
            }

            var set = ReadSetData(pathToFiles);
            var images = ReadSetImages(pathToFiles);

            set.Images = images.ToList();

            return set;
        }
    }
}