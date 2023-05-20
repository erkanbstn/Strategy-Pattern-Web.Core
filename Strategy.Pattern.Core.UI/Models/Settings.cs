namespace Strategy.Pattern.Core.UI.Models
{
    public class Settings
    {
        public static string claimDatabaseType = "databasetype";
        public EDatabaseTypes DatabaseType { get; set; }
        public EDatabaseTypes GetDefault => EDatabaseTypes.SqlServer;
    }
}
