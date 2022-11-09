using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hms.Models.Utility
{
    public class Constants
    {
        public struct InvoiceStatus
        {
            public const string Paid = "Paid";
            public const string Pending = "Pending";
            public const string Disputed = "Disputed";
        }
    }
}
