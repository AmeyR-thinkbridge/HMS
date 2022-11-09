using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hms.Models
{
    public class Invoice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InvoiceId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string? Status { get; set; }
        [ForeignKey("Table")]
        public int? TableId { get; set; }
        public virtual Table? Table { get; set; }
        [ForeignKey("User")]
        public string? UserId { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<InvoiceRecords>? InvoiceRecords { get; set; }
    }
}
