using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hms.Models
{
    [Index("CategoryCode", IsUnique = true)]
    public class DishCategroy
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }

        //ToDo: Change name to categoryname max change in index as well
        public string? CategoryCode { get; set; }
        [MaxLength(100)]
        public string? Description { get; set; }

        public virtual ICollection<Dish>? DishList { get; set; }
    }
}
