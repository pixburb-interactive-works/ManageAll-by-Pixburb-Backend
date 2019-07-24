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
        public Task WriteDbLogAsync(string storedProcedure, SqlParameterCollection parameters)
        {
            try
            {
                Dictionary<string, object> dbParameters = new Dictionary<string, object>();
                dbParameters.Add("DBCommand : ", storedProcedure);
                for (int i =0; i < parameters.Count; i++)
                {
                    dbParameters.Add(parameters[i].ParameterName,parameters[i].Value);
                }
                
                LoggingManager.WriteDBLog(dbParameters);
                return Task.FromResult<bool>(true);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
