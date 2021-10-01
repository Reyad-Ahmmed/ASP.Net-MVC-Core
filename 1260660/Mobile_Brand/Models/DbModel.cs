using Microsoft.EntityFrameworkCore;
using Mobile_Brand.Custom_Attribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Mobile_Brand.Models
{
    public class Brand
    {
        public Brand()
        {
            this.Mobiles = new List<Mobile>();
        }
        public int BrandId { get; set; }
        [Required,StringLength(30),Display(Name ="Brand Name"),
            ValidBrand(ErrorMessage = "Brand must be 'Nokia', 'Samsung','Xaomi','One Plus' or 'iPhone'.")]
        public string BrandName { get; set; }
        [Required, StringLength(30)]
        public string Country { get; set; }
        public virtual ICollection<Mobile> Mobiles { get; set; }
    }
    public class Mobile
    {
        public int MobileId { get; set; }
        [Required, StringLength(30)]
        public string Model { get; set; }
        [Required,Column(TypeName ="date"), Display(Name = "Publish Date")
            ,DataType(DataType.Date),DisplayFormat(DataFormatString ="0:yyyy-MM-dd",ApplyFormatInEditMode =true)]
        public DateTime PublishDate { get; set; }
        [Required, Column(TypeName = "money")]
        public decimal Price { get; set; }
        [Required,StringLength(200)]
        public string Picture { get; set; }
        [Required,ForeignKey("Brand")]
        public int BrandId { get; set; }
        public virtual Brand Brand { get; set; }
    }
    public class MobileDbContext : DbContext
    {
        
        public MobileDbContext(DbContextOptions<MobileDbContext> options) : base(options) { }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Mobile> Mobiles { get; set; }
    }
}
