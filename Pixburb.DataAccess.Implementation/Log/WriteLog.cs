using log4net;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixburb.DataAccess.Implementation.Log
{
    public class WriteLog
    {
        private static readonly log4net.ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Task WriteDbLogAsync(string storedProcedure, SqlParameterCollection parameters)
        {
            try
            {
                Dictionary<string, object> dbParameters = new Dictionary<string, object>();
                dbParameters.Add("DBCommand:", storedProcedure);
                for (int i =0; i < parameters.Count; i++)
                {
                    dbParameters.Add(parameters[i].ParameterName,parameters[i].Value);
                }
                
                WriteDBLog(dbParameters);
                return Task.FromResult<bool>(true);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static void WriteDBLog(IDictionary<string, object> dictionary)
        {
            if (logger.IsDebugEnabled)
            {
                logger.Debug(dictionary);
            }
        }
    }
}
