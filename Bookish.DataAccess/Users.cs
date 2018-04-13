using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookish.DataAccess
{
    public class Users
    {
        public int UserId { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
    }
}
