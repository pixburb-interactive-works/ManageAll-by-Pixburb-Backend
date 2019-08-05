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
    public class OrderDataWriter : IOrderDataWriter
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["PixBurb"].ConnectionString;

        public Task<OperationOutcome> PlaceOrder(List<PlaceOrder> placeOrder)
        {
            OperationOutcome outcome = new OperationOutcome();
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Put_Order";

            foreach (var item in placeOrder)
            {
                var orderId = cmd.Parameters.Add(new SqlParameter("@Id", CommonHelper.RemoveDefaultValue(item.Id)));
                cmd.Parameters.Add(new SqlParameter("@UserId", CommonHelper.RemoveDefaultValue(item.CustomerId)));
                cmd.Parameters.Add(new SqlParameter("@UserName", CommonHelper.RemoveDefaultValue(item.CustomerName)));
                cmd.Parameters.Add(new SqlParameter("@AddressId", CommonHelper.RemoveDefaultValue(item.AddressId)));
                cmd.Parameters.Add(new SqlParameter("@Address", CommonHelper.RemoveDefaultValue(item.Address)));
                cmd.Parameters.Add(new SqlParameter("@City", CommonHelper.RemoveDefaultValue(item.City)));
                cmd.Parameters.Add(new SqlParameter("@State", CommonHelper.RemoveDefaultValue(item.State)));
                cmd.Parameters.Add(new SqlParameter("@PinCode", CommonHelper.RemoveDefaultValue(item.PinCode)));
                cmd.Parameters.Add(new SqlParameter("@PhoneNo", CommonHelper.RemoveDefaultValue(item.PhoneNumber)));
                cmd.Parameters.Add(new SqlParameter("@Email", CommonHelper.RemoveDefaultValue(item.Email)));
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
                    var IdentityValue = Convert.ToInt32(orderId.Value);

                    SqlCommand attachmetcmd = conn.CreateCommand();
                    attachmetcmd.CommandType = CommandType.StoredProcedure;
                    attachmetcmd.CommandText = "Put_OrderItems";

                    foreach (var orderitem in item.OrderItems)
                    {
                        cmd.Parameters.Add(new SqlParameter("@Id", CommonHelper.RemoveDefaultValue(orderitem.Id)));
                        cmd.Parameters.Add(new SqlParameter("@OrderId", CommonHelper.RemoveDefaultValue(IdentityValue)));
                        cmd.Parameters.Add(new SqlParameter("@ProductId", CommonHelper.RemoveDefaultValue(orderitem.ProductId)));
                        cmd.Parameters.Add(new SqlParameter("@Quantity", CommonHelper.RemoveDefaultValue(orderitem.Quantity)));
                        cmd.Parameters.Add(new SqlParameter("@Price", CommonHelper.RemoveDefaultValue(orderitem.Price)));
                        cmd.Parameters.Add(new SqlParameter("@RecordStatus", CommonHelper.RemoveDefaultValue(orderitem.RecordStatus)));
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

            return Task.FromResult<OperationOutcome>(outcome);
        }
    }
}
