using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MigrationWithMySql.SettingManager
{
    public class DatabaseSettingsModel
    {
        public string Server { get; set; }

        public string User { get; set; }

        public string Password { get; set; }

        public string DatabaseName { get; set; }

        public bool Pooling { get; set; }

        public long MinimumPoolSize { get; set; }

        public long MaximumPoolSize { get; set; }

        public string GetConnectionString()
        {
            return string.Format(
                "server={0};database={1};user={2};password={3};Pooling={4};MinimumPoolSize={5};maximumpoolsize={6};",
                Server, DatabaseName, User, Password, Pooling, MinimumPoolSize, MaximumPoolSize);
        }

    }
}
