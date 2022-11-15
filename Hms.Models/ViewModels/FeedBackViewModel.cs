using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hms.Models.ViewModels
{
    public class FeedBackViewModel
    {
        [Range(1, 5, ErrorMessage = "please select the values between 1 to 5")]
        public short? Description { get; set; }

        public string? UserId { get; set; }
    }
}
