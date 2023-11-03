using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.Model.DTOs
{
    public class BookUpdateDto
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public Guid GenreId { get; set; }
        //public int ViewCount { get; set; }
        public string ISBN { get; set; }
        public string Publisher { get; set; }
        public string ImageUrl { get; set; }
        public string PublishedYear { get; set; }
        public string Content { get; set; }
        public string Description { get; set; }
    }
}
