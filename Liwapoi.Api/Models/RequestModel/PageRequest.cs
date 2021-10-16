using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Liwapoi.Api.Models.RequestModel
{
    public class PageRequest
    {
        [Required(ErrorMessage = "PageNumber is required")]
        [Range(1, int.MaxValue, ErrorMessage = "PageNumber must be grater than 0")]
        public int PageNumber { get; set; }

        [Required(ErrorMessage = "PageSize is required")]
        [Range(1, int.MaxValue, ErrorMessage = "PageSize must be grater than 0")]
        public int PageSize { get; set; }

        public PageRequest()
        {
            PageNumber = 1;
            PageSize = 10;
        }
    }
}
