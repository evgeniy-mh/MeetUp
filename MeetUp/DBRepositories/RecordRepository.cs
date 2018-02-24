using MeetUp.DB;
using MeetUp.DBEntityModels;
using System.Collections.Generic;
using System.Linq;

namespace MeetUp.DBRepositories
{
    class RecordRepository : EFGenericRepository<Record>
    {
        public RecordRepository(MeetUpContext context) : base(context)
        {
        }

        public IEnumerable<Record> GetFreeRecords()
        {
            return Context.Records.Where(r => r.Meeting == null).ToList();
        }

        public IEnumerable<Record> GetRecordsForMeeting(Meeting meeting)
        {
            return Context.Records.Where(r => r.Meeting.Id == meeting.Id);
        }
    }
}
