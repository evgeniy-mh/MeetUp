using MeetUp.DB;
using MeetUp.DBEntityModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MeetUp.DBRepositories
{
    class RecordRepository : EFGenericRepository<Record>
    {
        public RecordRepository(MeetUpContext context) : base(context)
        {
        }

        public new void Update(Record record)
        {
            var r = Context.Records.Find(record.Id);
            Context.Entry(r).CurrentValues.SetValues(record);
            if (record.Meeting != null) r.Meeting = Context.Meetings.Find(record.Meeting.Id);
            Context.SaveChanges();
        }

        public IEnumerable<Record> GetFreeRecords()
        {
            return Context.Records.Where(r => r.Meeting == null).ToList();
        }

        public IEnumerable<Record> GetRecordsForMeeting(Meeting meeting)
        {
            return Context.Records.Where(r => r.Meeting.Id == meeting.Id);
        }

        public IEnumerable<Record> SearchRecords(string queryName, DateTime? queryFromDate, DateTime? queryToDate)
        {
            var records = Get("Meeting");

            if (queryName != null)
            {
                queryName = queryName.ToLower();
                records = records.Where(r =>
                {
                    return r.Name != null && r.Name.ToLower().Contains(queryName);
                });
            }

            if (queryFromDate != null)
            {
                records = records.Where(r =>
                {
                    return r.Meeting.Date >= queryFromDate;
                });
            }

            if (queryToDate != null)
            {
                records = records.Where(r =>
                {
                    return r.Meeting.Date <= queryFromDate;
                });
            }

            return records;
        }
    }
}
