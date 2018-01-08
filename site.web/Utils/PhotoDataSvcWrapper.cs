using Kvn.Translit;
using NLog;
using site.core.DataSvc;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Runtime.Caching;
using System.Threading.Tasks;
using System.Web;

namespace site.web.Utils
{
    public class PhotoDataSvcWrapper
    {
        Logger logger;

        public PhotoDataSvcWrapper()
        {
            svc = new PhotoDataSvc(new GoogleDriveSvcFactory(Options));
            logger = LogManager.GetCurrentClassLogger();
        }

        public void ResetCache()
        {
            var cacheKeys = MemoryCache.Default.Select(kvp => kvp.Key).ToList();
            foreach (string cacheKey in cacheKeys)
            {
                MemoryCache.Default.Remove(cacheKey);
            }
        }

        public async Task<List<GoogleDriveFolder>> SeedCacheAsync(bool force = false)
        {
            var categories = MemoryCache.Default.Get("categories") as List<GoogleDriveFolder>;

            if (force || categories == null)
            {
                logger.Info("seeding cache");

                var dataSvc = new PhotoDataSvcWrapper();
                categories = await Task.Run(async () => await dataSvc.GetCategoriesAsync());

                var translitParser = new RuEngParser();
                var separators = new[] { ' ', ',','_','.' };

                Func<string, string> aliasFactory = (e) =>
                {
                    return string.Join("-", 
                        e.Split(separators, StringSplitOptions.RemoveEmptyEntries)
                        .Select(ee=> translitParser.Transliterate(ee)
                        .Replace("`", "")));
                };

                categories.ForEach(e =>
                {
                    e.Alias = aliasFactory(e.Name);
                });

                foreach (var category in categories)
                {
                    var byCategory = await Task.Run(async () => await dataSvc.GetByCategoryAsync(category.Id));

                    byCategory.ForEach(e =>
                    {
                        e.Alias = aliasFactory(e.Name);
                        e.ParentAlias = category.Alias;
                    });

                    category.Children = byCategory;
                }

                MemoryCache.Default.Remove("categories");
                MemoryCache.Default.Set("categories", categories, new CacheItemPolicy());
            }

            return categories;
        }

        private PhotoDataSvc svc;

        private static GoogleDriveOptions Options = new GoogleDriveOptions
        {
            ApplicationName = "vita marienko photography",
            SecretFileName = "client_secret.json"
        };

        public async Task<bool> TryGetAnyAsync()
        {
            var initCategoryTitles = await GetByInitialCategory();

            if (initCategoryTitles?.Any() != true)
            {
                return false;
            }

            try
            {
                var firstTitle = initCategoryTitles.First();
                var httpWebRequest = WebRequest.Create(firstTitle.Url) as HttpWebRequest;
                httpWebRequest.Method = "GET";
                HttpWebResponse httpWebResponse = await httpWebRequest.GetResponseAsync() as HttpWebResponse;

                return httpWebResponse.StatusCode == HttpStatusCode.OK;
            }
            catch (Exception exception)
            {
                logger.Error(exception);

                return false;
            }
        }

        public async Task<List<GoogleDriveFolder>> GetByInitialCategory()
        {
            var initialCategoryId = ConfigurationManager.AppSettings["initialcategoryid"];
            var categoryKey = $"category:{initialCategoryId}";
            var titles = MemoryCache.Default.Get(categoryKey) as List<GoogleDriveFolder>;

            if (titles == null)
            {
                titles = await svc.GetByCategoryAsync(initialCategoryId);

                MemoryCache.Default.Remove(categoryKey);
                MemoryCache.Default.Set(categoryKey, titles, new CacheItemPolicy());
            }

            return titles;
        }

        public async Task<List<GoogleDriveFolder>> GetByCategoryAsync(string id)
        {
            var categoryKey = $"category:{id}";
            var titles = MemoryCache.Default.Get(categoryKey) as List<GoogleDriveFolder>;

            if (titles == null)
            {
                titles = await svc.GetByCategoryAsync(id);
                
                MemoryCache.Default.Remove(categoryKey);
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

                MemoryCache.Default.Remove(sessionKey);
                MemoryCache.Default.Set(sessionKey, items, new CacheItemPolicy());
            }

            return items;
        }
    }
}