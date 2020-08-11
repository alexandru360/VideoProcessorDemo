namespace Api.Helpers
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public string AppConn { get; set; }
    }
    public class ConnectionStrings
    {
        public string AppConnSql { get; set; }
        public string AppConnMySql { get; set; }
        public string AppConnSqLite { get; set; }
    }
    public class EmailSettings
    {
        public string PrimaryDomain { get; set; }
        public int PrimaryPort { get; set; }
        public string SecondayDomain { get; set; }
        public int SecondaryPort { get; set; }
        public string UsernameEmail { get; set; }
        public string UsernamePassword { get; set; }
        public string FromEmail { get; set; }
        public string ToEmail { get; set; }
        public string CcEmail { get; set; }
    }

}