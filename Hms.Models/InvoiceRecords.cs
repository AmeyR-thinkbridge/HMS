using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hms.Models
{
    public class InvoiceRecords
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InvoiceRecordId { get; set; }
        public double Units { get; set; }
        public double? Total { get; set; }
        [ForeignKey("Dish")]
        public int? DishId { get; set; }
        public virtual Dish? Dish { get; set; }

    }
}
