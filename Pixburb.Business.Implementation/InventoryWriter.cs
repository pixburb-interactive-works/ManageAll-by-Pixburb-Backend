using Pixburb.Business.Interface;
using Pixburb.CommonModel;
using Pixburb.DataAccess.Implementation;
using Pixburb.DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Pixburb.Business.Implementation
{
    public class InventoryWriter : IInventoryWriter
    {
        private readonly IInventoryDataWriter inventoryDataWriter;

        public InventoryWriter(IInventoryDataWriter inventoryDataWriter)
        {
            this.inventoryDataWriter = inventoryDataWriter;
        }

        public async Task<OperationOutcome> SaveInventory(List<Inventory> inventory)
        {
            OperationOutcome outcome;
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                outcome = await this.inventoryDataWriter.SaveInventory(inventory);

                if (outcome.Status == OperationOutcomeStatus.Success)
                {
                    scope.Complete();
                }
            }

            return outcome;
        }
    }
}
