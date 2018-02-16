using MeetUp.DBEntityModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetUp.DBRepositories
{
    class MeetingRepository : EFGenericRepository<Meeting>
    {
        private ConcilRepository ConcilRepository;

        public MeetingRepository(DbContext context) : base(context)
        {
            ConcilRepository = new ConcilRepository(context);
        }


        public void SetConcilForMeeting(Meeting meeting, Concil concil)
        {
            Meeting resultMeeting = _dbSet.Include("Concil").SingleOrDefault(m => m.Id == meeting.Id);
            Concil resultConcil = ConcilRepository.GetAll().SingleOrDefault(c => c.Id == concil.Id);

            if (resultMeeting != null && resultConcil != null)
            {
                resultMeeting.Concil = resultConcil;
                Context.SaveChanges();
            }
        }
    }
}
