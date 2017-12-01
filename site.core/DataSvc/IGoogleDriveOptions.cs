namespace site.core.DataSvc
{
    public interface IGoogleDriveOptions
    {
        string[] Scopes { get; }

        string ApplicationName { get; set; }
    }
}