using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hms.Models.ViewModels
{
    public class InvoiceRecordsViewModel
    {
        [Required(ErrorMessage ="Please enter the units")]
        public double Units { get; set; }
        public double? Total { get; set; }
        [Required(ErrorMessage ="Please enter the dishId")]
        public int DishId { get; set; }
        public int? InvoiceId { get; set; }
    }
}
