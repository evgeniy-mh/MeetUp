using MeetUp.DB;
using System;

namespace MeetUp.DBRepositories
{
    class UnitOfWork : IDisposable
    {
        private MeetUpContext db = new MeetUpContext();

        private ConcilRepository concilRepository;
        public ConcilRepository ConcilRepository
        {
            get
            {
                if (concilRepository == null) concilRepository = new ConcilRepository(db);
                return concilRepository;
            }
        }

        private EmployeeRepository employeeRepository;
        public EmployeeRepository EmployeeRepository
        {
            get
            {
                if (employeeRepository == null) employeeRepository = new EmployeeRepository(db);
                return employeeRepository;
            }
        }

        private MeetingRepository meetingRepository;
        public MeetingRepository MeetingRepository
        {
            get
            {
                if (meetingRepository == null) meetingRepository = new MeetingRepository(db);
                return meetingRepository;
            }
        }

        private RecordRepository recordRepository;
        public RecordRepository RecordRepository
        {
            get
            {
                if (recordRepository == null) recordRepository = new RecordRepository(db);
                return recordRepository;
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
