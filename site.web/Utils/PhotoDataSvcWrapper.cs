using site.core.DataSvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Threading.Tasks;
using System.Web;

namespace site.web.Utils
{
    public class PhotoDataSvcWrapper
    {
        public PhotoDataSvcWrapper()
        {
            svc = new PhotoDataSvc(new GoogleDriveSvcFactory(Options));
        }

        private PhotoDataSvc svc;

        private static GoogleDriveOptions Options = new GoogleDriveOptions
        {
            ApplicationName = "vita marienko photography",
            SecretFileName = "client_secret.json"
        };

        public async Task<List<GoogleDriveFolder>> GetByCategoryAsync(string id)
        {
            var categoryKey = $"category:{id}";
            var titles = MemoryCache.Default.Get(categoryKey) as List<GoogleDriveFolder>;

            if (titles == null)
            {
                titles = await svc.GetByCategoryAsync(id);

                MemoryCache.Default.Set(categoryKey, titles, new CacheItemPolicy());
            }

            return titles;
        }
        
        public async Task<List<GoogleDriveFolder>> GetCategoriesAsync()
        {
            return await svc.GetCategoriesAsync();
        }

        public async Task<List<GoogleDriveImage>> GetSessionAsync(string categoryId, string id)
        {
            var sessionKey = $"category:{categoryId}:session:{id}";
            var items = MemoryCache.Default.Get(sessionKey) as List<GoogleDriveImage>;

            if (items == null)
            {
                items = await svc.GetSession(id);

                MemoryCache.Default.Set(sessionKey, items, new CacheItemPolicy());
            }

            return items;
        }
    }
}