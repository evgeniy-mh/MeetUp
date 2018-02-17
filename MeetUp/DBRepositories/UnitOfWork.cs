using MeetUp.DB;
using System;

namespace MeetUp.DBRepositories
{
    class UnitOfWork : IDisposable
    {
        private MeetUpContext db = new MeetUpContext();
        private ConcilRepository concilRepository;
        private EmployeeRepository employeeRepository;
        private MeetingRepository meetingRepository;

        public ConcilRepository ConcilRepository
        {
            get
            {
                if (concilRepository == null) concilRepository = new ConcilRepository(db);
                return concilRepository;
            }
        }

        public EmployeeRepository EmployeeRepository
        {
            get
            {
                if (employeeRepository == null) employeeRepository = new EmployeeRepository(db);
                return employeeRepository;
            }
        }

        public MeetingRepository MeetingRepository
        {
            get
            {
                if (meetingRepository == null) meetingRepository = new MeetingRepository(db);
                return meetingRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
