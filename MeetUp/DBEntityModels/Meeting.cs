using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace MeetUp.DBEntityModels
{
    public class Meeting : IHasId, INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Name { get; set; }

        private DateTime? date;
        public DateTime? Date {
            get
            {
                return date;
            }
            set
            {
                date = value;
                OnPropertyChanged("Date");
            }
        }

        public bool IsCarriedOut { get; set; }
        public string Agenda { get; set; }

        public int? ConcilId { get; set; }

        private Concil concil;
        public Concil Concil {
            get
            {
                return concil;
            }
            set
            {
                concil = value;
                OnPropertyChanged("Concil");
            }
        }

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

            Concil = meeting.Concil == null ? null : new Concil(meeting.Concil);
            Employees = meeting.Employees == null ? new List<Employee>() : meeting.Employees.ToList();
            Records = meeting.Records == null ? new List<Record>() : meeting.Records.ToList();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
