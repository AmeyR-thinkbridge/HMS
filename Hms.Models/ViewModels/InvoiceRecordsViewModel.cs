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
        public int? DishId { get; set; }
    }
}
