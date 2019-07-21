using Pixburb.Business.Interface;
using Pixburb.CommonModel;
using Pixburb.DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixburb.Business.Implementation
{
    public class InventoryReader : IInventoryReader
    {
        private readonly IInventoryDataReader inventoryDataReader;

        public InventoryReader(IInventoryDataReader inventoryDataReader)
        {
            this.inventoryDataReader = inventoryDataReader;
        }

        public Task<List<InventoryValue>> GetInventory(int? userId)
        {
            return this.inventoryDataReader.GetInventory(userId);
        }
    }
}
