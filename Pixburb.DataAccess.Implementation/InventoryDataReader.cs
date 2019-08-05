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
    public class InventoryDataReader : IInventoryDataReader
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["PixBurb"].ConnectionString;

        public Task<List<Inventory>> GetInventory(int? userId)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Get_Inventory";
            cmd.Parameters.Add(new SqlParameter("@UserId", CommonHelper.RemoveDefaultValue(userId)));
            var reader = cmd.ExecuteReader();

            List<Inventory> inventoryValues = new List<Inventory>();
            while (reader.Read())
            {
                Inventory inventory = new Inventory();
                inventory.Id = Convert.ToInt32(reader["ProductId"]);
                inventory.Name = Convert.ToString(reader["Name"]);
                inventory.Description = reader["Description"] == DBNull.Value ? null : Convert.ToString(reader["Description"]);
                inventory.Vendor = reader["Vendor"] == DBNull.Value ? null : Convert.ToString(reader["Vendor"]);
                inventory.Location = reader["Location"] == DBNull.Value ? null : Convert.ToString(reader["Location"]);
                inventory.BatchCode = reader["BatchCode"] == DBNull.Value ? null : Convert.ToString(reader["BatchCode"]);
                inventory.Price = reader["Price"] == DBNull.Value ? default(decimal) : Convert.ToDecimal(reader["Price"]);
                inventory.Featured = reader["Featured"] == DBNull.Value ? false : Convert.ToBoolean(reader["Featured"]);
                inventory.Tags = reader["Tags"] == DBNull.Value ? null : Convert.ToString(reader["Tags"]);
                inventory.Stock = reader["Stock"] == DBNull.Value ? default(decimal) : Convert.ToDecimal(reader["Stock"]);
                inventory.LimitPerUser = reader["LimitPerUser"] == DBNull.Value ? default(double) : Convert.ToDouble(reader["LimitPerUser"]);
                inventory.Category = new CategoryBase { Id = Convert.ToInt32(reader["CategoryId"]), CategoryName = Convert.ToString(reader["Category"]) };
                inventory.File = new FileAttachment { Id = Convert.ToInt32(reader["FileId"]), FileName = Convert.ToString(reader["FileName"]), FileType = Convert.ToString(reader["FileType"]), FileContent = reader["FileContent"] as byte[] };
                inventory.StockDate = reader["StockDate"] == DBNull.Value ? default(DateTime) : Convert.ToDateTime(reader["StockDate"]);
                inventory.Status = reader["Status"] == DBNull.Value ? null : Convert.ToString(reader["Status"]);
                inventoryValues.Add(inventory);
            }

            conn.Close();

            return Task.FromResult(inventoryValues);
        }
    }
}
