using log4net;
using System;
using System.Collections.Generic;

namespace Pixburb.DataAccess.Implementation.Log
{
    internal class LogAccess
    {
        private readonly ILog log;

        public LogAccess(string exceptionType)
        {
            this.log = LogManager.GetLogger(exceptionType);
            if (this.log == null && this.log.Logger.Name == null)
            {
                throw new ArgumentNullException("exceptionType", "Logger not available!");
            }
        }

        public void WriteLog(IDictionary<string, object> dictionary)
        {
            //if (this.log.IsDebugEnabled)
            //{
                this.log.Debug(dictionary);
            //}
        }
    }
}
