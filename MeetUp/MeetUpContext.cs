using MeetUp.DBEntityModels;
using System.Data.Entity;

namespace MeetUp
{
    class MeetUpContext : DbContext
    {
        public MeetUpContext() : base("MeetUpDb")
        {
            
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Concil> Concils { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
    }
}
