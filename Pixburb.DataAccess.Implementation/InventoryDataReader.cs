using Pixburb.CommonModel;
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

        public Task<List<InventoryValue>> GetInventory(int? userId)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Get_Inventory";
            var reader = cmd.ExecuteReader();

            List<InventoryValue> inventoryValues = new List<InventoryValue>();
            while (reader.Read())
            {
                InventoryValue inventory = new InventoryValue();
                inventory.Id = Convert.ToInt32(reader["ProductId"]);
                inventory.Name = Convert.ToString(reader["Name"]);
                inventory.Description = Convert.ToString(reader["Description"]);
                inventory.Vendor = Convert.ToString(reader["Vendor"]);
                inventory.Location = Convert.ToString(reader["Location"]);
                inventory.BatchCode = Convert.ToString(reader["BatchCode"]);
                inventory.Price = Convert.ToDecimal(reader["Price"]);
                inventory.Featured = Convert.ToBoolean(reader["Featured"]);
                inventory.Tags = Convert.ToString(reader["Tags"]);
                inventory.Stock = Convert.ToDecimal(reader["Stock"]);
                inventory.LimitPerUser = Convert.ToDouble(reader["LimitPerUser"]);
                inventory.Category = new Category { Id = Convert.ToInt32(reader["CategoryId"]), ParentId = Convert.ToInt32(reader["ParentId"]), CategoryName = Convert.ToString(reader["Category"]) };
                inventory.File = new FileAttachment { Id = Convert.ToInt32(reader["FileId"]), FileName = Convert.ToString(reader["FileName"]), FileType = Convert.ToString(reader["FileType"]), FileContent = reader["FileContent"] as byte[] };
                inventory.StockDate = Convert.ToDateTime(reader["StockDate"]);
                inventory.Status = Convert.ToString(reader["Status"]);
                inventoryValues.Add(inventory);
            }

            conn.Close();

            return Task.FromResult(inventoryValues);
        }
    }
}
