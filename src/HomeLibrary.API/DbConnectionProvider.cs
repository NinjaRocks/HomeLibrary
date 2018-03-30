using MicroFx.Data;
using Microsoft.WindowsAzure;

namespace HomeLibrary.API
{
    public class DbConnectionProvider : IDbConnectionProvider
    {
        public string GetConnectionString()
        {
            return CloudConfigurationManager.GetSetting("SQL.Database.ConnectionString");
        }
    }
}