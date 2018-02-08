using System.Collections.Generic;

namespace MeetUp.DBEntityModels
{
    public class Concil : IHasId
    {
        public int Id { get; set; }
        public string Name { get; set; }        

        public ICollection<Employee> Employees { get; set; }
        public ICollection<Meeting> Meetings { get; set; }

        public Concil()
        {
            Employees = new List<Employee>();
            Meetings = new List<Meeting>();
        }
    }
}
