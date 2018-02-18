using MeetUp.DB;
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

        public MeetingRepository(MeetUpContext context) : base(context)
        {
        }

        public new void Create(Meeting meeting)
        {
            if (meeting.Concil != null)
            {
                var c = Context.Concils.Find(meeting.Concil.Id);
                meeting.Concil = c;
            }            

            Context.Meetings.Add(meeting);
            Context.SaveChanges();
        }

        public void SetConcilForMeeting(Meeting meeting, Concil concil)
        {
            var c = Context.Concils.Find(concil.Id);
            var m = Context.Meetings.Find(meeting.Id);
            m.Concil = c;
            Context.SaveChanges();
        }
    }
}
