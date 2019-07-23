using System.Collections.Generic;

namespace Pixburb.DataAccess.Implementation.Log
{
    public sealed class LoggingManager
    {
        public const string DefaultLogger = "General";

        public const string DBLogger = "DBLog";

        public static void WriteDBLog(IDictionary<string, object> dictionary)
        {
            LogAccess logAccess = new LogAccess(DBLogger);
            logAccess.WriteLog(dictionary);
        }
    }
}
