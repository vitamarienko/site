namespace site.core.DataSvc
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class PhotoDataSvc
    {
        private readonly GoogleDriveSvcFactory googleDriveSvcFactory;

        public PhotoDataSvc(
            GoogleDriveSvcFactory googleDriveSvcFactory)
        {
            this.googleDriveSvcFactory = googleDriveSvcFactory;
        }

        public async Task<List<GoogleDriveFolder>> GetByCategoryAsync(string categoryId)
        {
            return await googleDriveSvcFactory.GetFoldersByCategoryAsync(categoryId);
        }

        public async Task<List<GoogleDriveFolder>> GetCategoriesAsync()
        {
            return await googleDriveSvcFactory.GetFoldersAsync("1XaO5XSKCLXPuszMb07p40CPce8j9VA48");
        }

        public async Task<List<GoogleDriveImage>> GetSession(string id)
        {
            return await googleDriveSvcFactory.GetImagesAsync(id);
        }
    }
}