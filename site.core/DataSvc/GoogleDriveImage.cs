using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace site.core.DataSvc
{
    public class GoogleDriveImage
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public bool Portrait { get; internal set; }
    }
}
