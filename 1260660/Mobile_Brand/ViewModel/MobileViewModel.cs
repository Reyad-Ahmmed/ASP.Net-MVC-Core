using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Mobile_Brand.ViewModel
{
    public class MobileViewModel
    {
        public int MobileId { get; set; }
        [Required, StringLength(30)]
        public string Model { get; set; }
        [Required, Column(TypeName = "date"), Display(Name = "Publish Date"),
            DataType(DataType.Date), DisplayFormat(DataFormatString = "0:yyyy-MM-dd", ApplyFormatInEditMode = true)]
        public DateTime PublishDate { get; set; }
        [Required, Column(TypeName = "money")]
        public decimal Price { get; set; }
        
        public IFormFile Picture { get; set; }
        [Required]
        public int BrandId { get; set; }
    }
}
