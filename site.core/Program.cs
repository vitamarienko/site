using site.core.DataSvc;
using System.Threading.Tasks;

namespace site.core
{
    public class Program
    {
        public static void Main()
        {
            var options = new GoogleDriveOptions
            {
                ApplicationName = "vita marienko photography",
                SecretFileName = "client_secret.json"
            };

            var svc = new PhotoDataSvc(new GoogleDriveSvcFactory(options));

            var result = svc.GetByCategoryAsync("1XzuXHS7Dg_jJYaMmxAypeW3lN4rPxFru").Result;
        }
    }
}
