using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MigrationWithMySql.SettingManager
{
    public static class SettingsManager
    {
        public static DatabaseSettingsModel DatabaseSettings { get; } = new DatabaseSettingsModel();
    }
}
