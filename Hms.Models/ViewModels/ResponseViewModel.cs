using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hms.Models.ViewModels
{
    public class ResponseViewModel
    {
        public string? ErrorCode { get; set; }
        public string? ErrorDescription { get; set; }
        public bool HasError { get; set; }
        public string? SuccessMessage { get; set; }
    }
}
