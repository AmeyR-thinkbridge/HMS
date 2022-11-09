using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hms.Models.ViewModels
{
    public class TableViewModel
    {
        public int? TableNumber { get; set; }

        [MaxLength(100)]
        public string? Description { get; set; }
    }
}
