using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.Model.Entities.Shared
{
    public class BaseEntity
    {
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated  {get;set;}
    }
}
