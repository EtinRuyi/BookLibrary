using BookLibrary.Model.Entities.Shared;
using BookLibrary.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.Model.Entities
{
    public class Book : BaseEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int ViewCount { get; set; }
        public string ISBN { get; set; }
        public string Publisher { get; set; }
        public string ImageUrl { get; set; }
        public string PublishedYear { get; set; }
        public string Content { get; set;}
        public string Description { get; set; }

        [ForeignKey("Genre")]
        public Guid GenreId { get; set; }
        public Genre Genre { get; set; }

    }
}
