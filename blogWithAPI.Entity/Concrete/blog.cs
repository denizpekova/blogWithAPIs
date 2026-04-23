using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blogWithAPI.Entity.Concrete
{
    public class Blog
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public required string Author { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public required string ImageUrl { get; set; }
        public string Category { get; set; } = "YAZILIM & YAPAY ZEKA & SİBER GÜVENLİK";
        
    }
}