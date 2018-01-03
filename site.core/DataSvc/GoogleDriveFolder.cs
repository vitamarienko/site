using System.Collections.Generic;

namespace site.core.DataSvc
{
    public class GoogleDriveFolder
    {
        public string Name { get; internal set; }
        public string Id { get; internal set; }
        public string ParentId { get; internal set; }
        public string Url { get; internal set; }
        public string Base64Img { get; internal set; }
        public string Alias { get; set; }
        public string ParentAlias { get; set; }
        public List<GoogleDriveFolder> Children { get; set; }
    }
}
