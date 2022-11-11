using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hms.Models.ViewModels
{
    public class InvoiceViewModel
    {
        public DateTime InvoiceDate { get; set; }
        public string? Status { get; set; }
        [Required(ErrorMessage ="Please enter the TableId")]
        public int? TableId { get; set; }
        [Required(ErrorMessage ="You cannot submit a empty invoice")]
        public List<InvoiceRecordsViewModel> InvoiceRecords { get; set; }
    }
}
