using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookish.DataAccess
{
    public class Borrow
    {
        public int UserId { get; set; }

        public int BookId { get; set; }

        public DateTime DateBorrowed { get; set; }
        public DateTime DueDate { get; set; }

        public DateTime? DateReturned { get; set; }

    }
}
