using BookLibrary.Model.Enums;
using Microsoft.AspNetCore.Identity;

namespace BookLibrary.Model.Entities
{
    public class User :  IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }

    }
}
