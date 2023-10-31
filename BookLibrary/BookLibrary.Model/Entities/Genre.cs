using BookLibrary.Model.Entities.Shared;
using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Model.Entities
{
    public class Genre : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string GenreName { get; set; }

        IEnumerable<Book> BookList { get; set; }
    }
}
