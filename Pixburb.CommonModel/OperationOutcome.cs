using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixburb.CommonModel
{
    public class OperationOutcome
    {
        public OperationOutcomeStatus Status { get; set; }
        public string IdentityValue { get; set; }
        public List<OperationOutcomeMessage> Messages { get; set; } = new List<OperationOutcomeMessage>();
    }
}
