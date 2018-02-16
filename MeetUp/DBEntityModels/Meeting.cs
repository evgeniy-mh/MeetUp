using System;
using System.Collections.Generic;
using System.Linq;

namespace MeetUp.DBEntityModels
{
    public class Meeting : IHasId
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? Date { get; set; }
        public bool IsCarriedOut { get; set; }
        public string Agenda { get; set; }

        public int? ConcilId { get; set; }
        public Concil Concil { get; set; }

        public ICollection<Employee> Employees { get; set; }
        public ICollection<Record> Records { get; set; }

        public Meeting()
        {
            Employees = new List<Employee>();
            Records = new List<Record>();
        }

        public Meeting(Meeting meeting)
        {
            Id = meeting.Id;
            Name = meeting.Name;
            Date = meeting.Date;
            IsCarriedOut = meeting.IsCarriedOut; ;
            Agenda = meeting.Agenda;
            Concil = new Concil(meeting.Concil);

            Employees = meeting.Employees == null ? new List<Employee>() : meeting.Employees.ToList();
            Records = meeting.Records==null ? new List<Record>() : meeting.Records.ToList();
        }
    }
}
