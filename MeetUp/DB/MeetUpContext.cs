using MeetUp.DBEntityModels;
using System.Data.Entity;

namespace MeetUp.DB
{
    class MeetUpContext : DbContext
    {
        static MeetUpContext()
        {
            Database.SetInitializer<MeetUpContext>(new MeetUpContextInitializer());
        }

        public MeetUpContext() : base("MeetUpDb")
        {
            
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Concil> Concils { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<Record> Records { get; set; }
    }
}
