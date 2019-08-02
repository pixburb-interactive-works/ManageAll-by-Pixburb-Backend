using Pixburb.Business.Interface;
using Pixburb.CommonModel;
using Pixburb.DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Pixburb.Business.Implementation
{
    public class OrderWriter : IOrderWriter
    {
        private readonly IOrderDataWriter orderDataWriter;

        public OrderWriter(IOrderDataWriter orderDataWriter)
        {
            this.orderDataWriter = orderDataWriter;
        }

        public async Task<OperationOutcome> PlaceOrder(List<PlaceOrder> placeOrder)
        {
            OperationOutcome outcome;
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                outcome = await this.orderDataWriter.PlaceOrder(placeOrder);
                if (outcome.Status == OperationOutcomeStatus.Success)
                {
                    scope.Complete();
                }
            }
            return outcome;
        }
    }
}
