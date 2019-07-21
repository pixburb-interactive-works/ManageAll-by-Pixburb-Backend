using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixburb.CommonModel
{
    public class OperationOutcome
    {
        public OperationOutcome()
            : this(OperationOutcomeStatus.Success)
        { }

        public OperationOutcome(OperationOutcomeStatus status)
        {
            this.Status = status;
            this.Messages = new List<OperationOutcomeMessage>();
        }

        public OperationOutcomeStatus Status { get; set; }

        public List<OperationOutcomeMessage> Messages { get; set; } = new List<OperationOutcomeMessage>();
    }
}
