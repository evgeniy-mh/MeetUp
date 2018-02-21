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

        public new void Update(Meeting meeting)
        {
            var dbMeeting = Context.Meetings.Find(meeting.Id);
            Context.Entry(dbMeeting).CurrentValues.SetValues(meeting);

            if (meeting.Concil != null)
            {
                var dbConcil = Context.Concils.Find(meeting.Concil.Id);
                dbMeeting.Concil = dbConcil;
            }

            if (meeting.Records != null && meeting.Records.Count != 0)
            {
                foreach (Record r in meeting.Records)
                {
                    //добавить новые
                    if (dbMeeting.Records.SingleOrDefault(record => record.Id == r.Id) == null)
                    {
                        var dbRecord = Context.Records.Find(r.Id);
                        dbMeeting.Records.Add(dbRecord);
                    }
                }

                foreach (Record r in dbMeeting.Records.ToList())
                {
                    //удалить старые
                    if (meeting.Records.SingleOrDefault(record => record.Id == r.Id) == null)
                    {
                        var dbRecord = Context.Records.Find(r.Id);
                        dbMeeting.Records.Remove(dbRecord);
                    }
                }
            }

            Context.SaveChanges();
        }

        public void SetConcilForMeeting(Meeting meeting, Concil concil)
        {
            var c = Context.Concils.Find(concil.Id);
            var m = Context.Meetings.Find(meeting.Id);
            m.Concil = c;
            Context.SaveChanges();
        }

        public void RemoveRecordFromMeeting(Meeting meeting, Record record)
        {
            var m = Context.Meetings.Find(meeting.Id);
            Context.Entry(m).Collection(m2 => m2.Records).Load();

            var r = Context.Records.Find(record.Id);

            if (m.Records.Contains(r))
            {
                m.Records.Remove(r);
            }

            Context.SaveChanges();
        }
    }
}
