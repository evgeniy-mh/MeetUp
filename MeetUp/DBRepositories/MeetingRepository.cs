﻿using MeetUp.DB;
using MeetUp.DBEntityModels;
using System;
using System.Collections.Generic;
using System.Linq;

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

            if (meeting.Records != null && meeting.Records.Count != 0)
            {
                List<Record> dbRecords = new List<Record>();
                foreach (Record r in meeting.Records)
                {
                    var dbRecord = Context.Records.Find(r.Id);
                    dbRecords.Add(dbRecord);
                }
                meeting.Records = dbRecords;
            }

            if (meeting.Employees != null && meeting.Employees.Count != 0)
            {
                List<Employee> dbEmployees = new List<Employee>();
                foreach (Employee e in meeting.Employees)
                {
                    var dbEmployee = Context.Employees.Find(e.Id);
                    dbEmployees.Add(dbEmployee);
                }
                meeting.Employees = dbEmployees;
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

            if (meeting.Employees != null && meeting.Employees.Count != 0)
            {
                foreach (Employee e in meeting.Employees)
                {
                    //добавить новые
                    if (dbMeeting.Employees.SingleOrDefault(employee => employee.Id == e.Id) == null)
                    {
                        var dbRecord = Context.Employees.Find(e.Id);
                        dbMeeting.Employees.Add(dbRecord);
                    }
                }

                foreach (Employee e in dbMeeting.Employees.ToList())
                {
                    //удалить старые
                    if (meeting.Employees.SingleOrDefault(employee => employee.Id == e.Id) == null)
                    {
                        var dbRecord = Context.Employees.Find(e.Id);
                        dbMeeting.Employees.Remove(dbRecord);
                    }
                }
            }
            Context.SaveChanges();
        }

        public new void Remove(Meeting meeting)
        {
            Remove(meeting, false);
        }

        public void Remove(Meeting meeting, bool deleteWithRecords)
        {
            var m = Context.Meetings.Find(meeting.Id);
            Context.Entry(m).Collection(m2 => m2.Records).Load();

            foreach (Record r in m.Records.ToList())
            {
                if (deleteWithRecords)
                {
                    Context.Records.Remove(r);
                }
                else
                {
                    r.Meeting = null;
                }
            }
            Context.Meetings.Remove(m);
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

        public IEnumerable<Meeting> SearchMeetings(string queryName, DateTime? queryFromDate, DateTime? queryToDate, bool? queryIsCarriedOut)
        {
            var meetings = GetAll();

            if (queryName != null)
            {
                queryName = queryName.ToLower();
                meetings = meetings.Where(m =>
                 {
                     return
                     m.Name != null && m.Name.ToLower().Contains(queryName);
                 });
            }

            if (queryFromDate != null)
            {
                meetings = meetings.Where(m =>
                {
                    return m.Date >= queryFromDate;
                });
            }

            if (queryToDate != null)
            {
                meetings = meetings.Where(m =>
                {
                    return m.Date <= queryFromDate;
                });
            }

            if (queryIsCarriedOut != null)
            {
                meetings = meetings.Where(m =>
                {
                    return m.IsCarriedOut == queryIsCarriedOut;
                });
            }
            return meetings;
        }

        public class MeetingIdComparer : IEqualityComparer<Meeting>
        {
            public bool Equals(Meeting x, Meeting y)
            {
                return x.Id == y.Id;
            }

            public int GetHashCode(Meeting obj)
            {
                return obj.GetHashCode();
            }
        }
    }
}
