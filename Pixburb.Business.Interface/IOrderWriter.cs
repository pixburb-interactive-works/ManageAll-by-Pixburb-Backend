﻿using Pixburb.CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixburb.Business.Interface
{
    public interface IOrderWriter
    {
        Task<OperationOutcome> PlaceOrder(List<PlaceOrder> placeOrder);
    }
}
