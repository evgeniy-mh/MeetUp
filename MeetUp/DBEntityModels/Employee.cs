using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetUp
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public DateTime? BirthDate { get; set; }
        public string TelephoneNumber { get; set; }
        public string Email { get; set; }

        public ICollection<Conference> Conferences { get; set; }
        public Employee()
        {
            Conferences = new List<Conference>();
        }
    }
}
