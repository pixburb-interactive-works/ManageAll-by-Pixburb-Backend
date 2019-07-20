using Pixburb.CommonModel;
using Pixburb.DataAccess.Implementation.Helpers;
using Pixburb.DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixburb.DataAccess.Implementation
{
    public class InventoryDataWriter : IInventoryDataWriter
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["PixBurb"].ConnectionString;

        public Task<OperationOutcome> SaveInventory(List<Inventory> inventory)
        {
            OperationOutcome outcome = new OperationOutcome();
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Put_Inventory";

            foreach (var item in inventory)
            {
                //var productId = cmd.Parameters.Add(new SqlParameter(parameterName: "@Id", dbType: SqlDbType.VarChar, size: 50, direction: ParameterDirection.Output, isNullable: true, precision: 2, scale: 2, sourceColumn: "", sourceVersion: DataRowVersion.Current, value: CommonHelper.RemoveDefaultValue(item.Id)));
                var productId = cmd.Parameters.Add(new SqlParameter("@Id", CommonHelper.RemoveDefaultValue(item.Id)));
                cmd.Parameters.Add(new SqlParameter("@CategoryId", CommonHelper.RemoveDefaultValue(item.Category)));
                cmd.Parameters.Add(new SqlParameter("@Name", CommonHelper.RemoveDefaultValue(item.Name)));
                cmd.Parameters.Add(new SqlParameter("@Description", CommonHelper.RemoveDefaultValue(item.Description)));
                cmd.Parameters.Add(new SqlParameter("@Vendor", CommonHelper.RemoveDefaultValue(item.Vendor)));
                cmd.Parameters.Add(new SqlParameter("@Location", CommonHelper.RemoveDefaultValue(item.Location)));
                cmd.Parameters.Add(new SqlParameter("@BatchCode", CommonHelper.RemoveDefaultValue(item.BatchCode)));
                cmd.Parameters.Add(new SqlParameter("@Price", CommonHelper.RemoveDefaultValue(item.Price)));
                cmd.Parameters.Add(new SqlParameter("@Featured", CommonHelper.RemoveDefaultValue(item.Featured)));
                cmd.Parameters.Add(new SqlParameter("@Tags", CommonHelper.RemoveDefaultValue(item.Tags)));
                cmd.Parameters.Add(new SqlParameter("@Stock", CommonHelper.RemoveDefaultValue(item.Stock)));
                cmd.Parameters.Add(new SqlParameter("@LimitPerUser", CommonHelper.RemoveDefaultValue(item.LimitPerUser)));
                cmd.Parameters.Add(new SqlParameter("@FileName", CommonHelper.RemoveDefaultValue(item.File.FileName)));
                cmd.Parameters.Add(new SqlParameter("@FileType", CommonHelper.RemoveDefaultValue(item.File.FileType)));
                cmd.Parameters.Add(new SqlParameter("@FileContent", CommonHelper.RemoveDefaultValue(item.File.FileContent)));
                cmd.Parameters.Add(new SqlParameter("@Status", CommonHelper.RemoveDefaultValue(item.Status)));
                cmd.Parameters.Add(new SqlParameter("@RecordStatus", CommonHelper.RemoveDefaultValue(item.RecordStatus)));
                var success = cmd.Parameters.Add(new SqlParameter(parameterName: "@IsSuccess", dbType: SqlDbType.VarChar, size: 50, direction: ParameterDirection.Output, isNullable: true, precision: 2, scale: 2, sourceColumn: "", sourceVersion: DataRowVersion.Current, value: ""));
                var message = cmd.Parameters.Add(new SqlParameter(parameterName: "@Message", dbType: SqlDbType.VarChar, size: 50, direction: ParameterDirection.Output, isNullable: true, precision: 2, scale: 2, sourceColumn: "", sourceVersion: DataRowVersion.Current, value: ""));
                cmd.ExecuteNonQuery();

                if (Convert.ToInt32(success.Value) != 1)
                {
                    outcome = new OperationOutcome(OperationOutcomeStatus.Failure);
                    outcome.Messages.Add(new OperationOutcomeMessage { Message = Convert.ToString(message.Value) });
                }
                else
                {
                    var IdentityValue = Convert.ToInt32(productId.Value);

                    SqlCommand attachmetcmd = conn.CreateCommand();
                    attachmetcmd.CommandType = CommandType.StoredProcedure;
                    attachmetcmd.CommandText = "PUT_Attachment";

                    foreach (var attachment in item.Files)
                    {
                        cmd.Parameters.Add(new SqlParameter("@Id", CommonHelper.RemoveDefaultValue(attachment.Id)));
                        cmd.Parameters.Add(new SqlParameter("@ProductId", CommonHelper.RemoveDefaultValue(IdentityValue)));
                        cmd.Parameters.Add(new SqlParameter("@FileName", CommonHelper.RemoveDefaultValue(attachment.FileName)));
                        cmd.Parameters.Add(new SqlParameter("@FileType", CommonHelper.RemoveDefaultValue(attachment.FileType)));
                        cmd.Parameters.Add(new SqlParameter("@FileContent", CommonHelper.RemoveDefaultValue(attachment.FileContent)));
                        cmd.Parameters.Add(new SqlParameter("@RecordStatus", CommonHelper.RemoveDefaultValue(attachment.RecordStatus)));
                        var attachmentSuccess = cmd.Parameters.Add(new SqlParameter(parameterName: "@IsSuccess", dbType: SqlDbType.VarChar, size: 50, direction: ParameterDirection.Output, isNullable: true, precision: 2, scale: 2, sourceColumn: "", sourceVersion: DataRowVersion.Current, value: ""));
                        var attachmentMessage = cmd.Parameters.Add(new SqlParameter(parameterName: "@Message", dbType: SqlDbType.VarChar, size: 50, direction: ParameterDirection.Output, isNullable: true, precision: 2, scale: 2, sourceColumn: "", sourceVersion: DataRowVersion.Current, value: ""));

                        attachmetcmd.ExecuteNonQuery();

                        if (Convert.ToInt32(success.Value) == 1)
                        {
                            outcome = new OperationOutcome(OperationOutcomeStatus.Success);
                            outcome.Messages.Add(new OperationOutcomeMessage { Message = Convert.ToString(message.Value) });
                        }
                        else
                        {
                            outcome = new OperationOutcome(OperationOutcomeStatus.Failure);
                            outcome.Messages.Add(new OperationOutcomeMessage { Message = Convert.ToString(message.Value) });
                        }
                    }
                } 
            }
            conn.Close();

            return Task.FromResult(outcome);
        }
    }
}
