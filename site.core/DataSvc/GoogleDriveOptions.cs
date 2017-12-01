namespace site.core.DataSvc
{
    using Google.Apis.Drive.v3;

    public class GoogleDriveOptions
    {
        public virtual string[] Scopes => new[] { DriveService.Scope.DriveReadonly };

        public string ApplicationName { get; set; }

        public string SecretFileName { get; set; }
    }
}