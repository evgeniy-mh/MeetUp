using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetUp
{
    class UserContext: DbContext
    {
        public UserContext() : base("MeetUpDb")
        {

        }

        public DbSet<Employee> Employees { get; set; }
    }
}
