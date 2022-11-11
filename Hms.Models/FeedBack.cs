using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hms.Models
{
    public class FeedBack
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FeedbackId { get; set; }
        //Todo : Either make it string or completely change it to number.
        [MaxLength(100),Column("Rating"),Range(1,5)]
        public short? Description { get; set; }
        [ForeignKey("User")]
        public string? UserID { get; set; }
        public virtual User? User { get; set; }
    }
}
