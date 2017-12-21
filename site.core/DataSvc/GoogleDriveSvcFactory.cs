namespace site.core.DataSvc
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Google.Apis.Auth.OAuth2;
    using Google.Apis.Drive.v3;
    using Google.Apis.Services;
    using System.Text.RegularExpressions;
    using System.Net;

    public class GoogleDriveSvcFactory
    {
        private DriveService _drive;

        public GoogleDriveSvcFactory(GoogleDriveOptions options)
        {
            Options = options;
        }

        public GoogleDriveOptions Options { get; }

        public async Task<DriveService> GetDriveAsync()
        {
            if (_drive != null)
            {
                return _drive;
            }

            GoogleCredential credential;

            var secretFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Options.SecretFileName);

            using (var stream =
                new FileStream(secretFilePath, FileMode.Open, FileAccess.Read))
            {
                credential = await Task.FromResult(GoogleCredential.FromStream(stream).CreateScoped(Options.Scopes));
            }

            _drive = new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = Options.ApplicationName
            });

            return _drive;
        }

        public async Task<List<GoogleDriveImage>> GetImagesAsync(string folderId)
        {
            var drive = await GetDriveAsync();
            var listRequest = drive.Files.List();

            listRequest.Q = $"'{folderId}' in parents and mimeType='image/jpeg'";
            listRequest.Fields = "files(*)";

            var images = await listRequest.ExecuteAsync();

            return images.Files.Select(e => new GoogleDriveImage { Id = e.Id, Url = e.WebContentLink, Portrait = e.ImageMediaMetadata.Width < e.ImageMediaMetadata.Height }).ToList();
        }

        public async Task<List<GoogleDriveFolder>> GetFoldersAsync(string folderId)
        {
            var drive = await GetDriveAsync();
            var listRequest = drive.Files.List();

            listRequest.Q = $"'{folderId}' in parents and mimeType='application/vnd.google-apps.folder'";
            listRequest.Fields = "files(*)";

            var folders = await listRequest.ExecuteAsync();
            var result = new List<GoogleDriveFolder>();

            foreach (var file in folders.Files)
            {
                var folder = new GoogleDriveFolder { Id = file.Id, Name = file.Name };

                folder.ParentId = file.Parents.FirstOrDefault();

                result.Add(folder);

                listRequest.Q = $"'{folderId}' in parents and mimeType='image/jpeg'";
                listRequest.Fields = "files(*)";

                var images = await listRequest.ExecuteAsync();

                if (!images.Files.Any())
                {
                    listRequest.Q = $"'{file.Id}' in parents and mimeType='image/jpeg'";
                    listRequest.Fields = "files(*)";

                    images = await listRequest.ExecuteAsync();

                    if (!images.Files.Any())
                    {
                        continue;
                    }
                }

                var image = images.Files.FirstOrDefault(e => Regex.IsMatch(e.Name, @"^[a-zA-Z]+$")) ?? images.Files.First();

                folder.Url = image.WebContentLink;

                using (var memoryStream = new MemoryStream())
                {
                    await drive.Files.Get(image.Id).DownloadAsync(memoryStream);

                    memoryStream.Position = 0;

                    folder.Base64Img = Convert.ToBase64String(memoryStream.ToArray());
                }
            }

            return result;
        }

        public async Task<List<GoogleDriveFolder>> GetFoldersByCategoryAsync(string folderId)
        {
            var drive = await GetDriveAsync();
            var listRequest = drive.Files.List();

            listRequest.Q = $"'{folderId}' in parents and mimeType='application/vnd.google-apps.folder'";
            listRequest.Fields = "files(*)";

            var folders = await listRequest.ExecuteAsync();
            var result = new List<GoogleDriveFolder>();

            foreach (var file in folders.Files)
            {
                var folder = new GoogleDriveFolder { Id = file.Id, Name = file.Name };

                folder.ParentId = file.Parents.FirstOrDefault();

                result.Add(folder);

                listRequest.Q = $"'{file.Id}' in parents and mimeType='image/jpeg'";
                listRequest.Fields = "files(*)";

                var images = await listRequest.ExecuteAsync();

                if (!images.Files.Any())
                {
                    continue;
                }

                var image = images.Files.FirstOrDefault(e => Regex.IsMatch(e.Name, @"^[a-zA-Z]+$")) ?? images.Files.First();

                //folder.Url = image.WebContentLink;
                HttpWebRequest httpWebRequest = WebRequest.Create(image.WebContentLink) as HttpWebRequest;
                httpWebRequest.Method = "HEAD";
                httpWebRequest.AllowAutoRedirect = false;
                HttpWebResponse httpWebResponse = await httpWebRequest.GetResponseAsync() as HttpWebResponse;
                folder.Url = httpWebResponse.StatusCode == HttpStatusCode.Redirect ? httpWebResponse.GetResponseHeader("Location") : image.WebContentLink;
                
            }

            return result;
        }


        public async Task<string[]> GetFileUrlsAsync(DriveService drive, string folderId)
        {
            var listRequest = drive.Files.List();

            listRequest.Q = $"'{folderId}' in parents";
            listRequest.Fields = "files(*)";

            var files = await listRequest.ExecuteAsync();

            return files.Files.Select(e => e.WebContentLink).ToArray();
        }
    }
}