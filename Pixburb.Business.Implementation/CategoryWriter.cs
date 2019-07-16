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
    public class CategoryWriter : ICategoryWriter
    {
        private readonly ICategoryDataWriter categoryDataWriter;

        public CategoryWriter(ICategoryDataWriter categoryDataWriter)
        {
            this.categoryDataWriter = categoryDataWriter;
        }

        public async Task<OperationOutcome> Category(Category category)
        {
            OperationOutcome outcome;
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                outcome = await this.categoryDataWriter.Category(category);

                if (outcome.Status == OperationOutcomeStatus.Success)
                {
                    scope.Complete();
                }
            }

            return outcome;
        }
    }
}
