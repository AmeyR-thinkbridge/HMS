using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hms.Models
{
    [Index("TableNumber", IsUnique = true)]
    public class Table
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TableId { get; set; }
        public int? TableNumber { get; set; }

        [MaxLength(100)]
        public string? Description { get; set; }
    }
}
