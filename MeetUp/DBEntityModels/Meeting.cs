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

        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        private DateTime? date;
        public DateTime? Date
        {
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

        private bool isCarriedOut;
        public bool IsCarriedOut
        {
            get
            {
                return isCarriedOut;
            }
            set
            {
                isCarriedOut = value;
                OnPropertyChanged("IsCarriedOut");
            }
        }

        private string agenda;
        public string Agenda
        {
            get
            {
                return agenda;
            }
            set
            {
                agenda = value;
                OnPropertyChanged("Agenda");
            }
        }

        public int? ConcilId { get; set; }

        private Concil concil;
        public Concil Concil
        {
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

        private ICollection<Employee> employees;
        public ICollection<Employee> Employees
        {
            get
            {
                return employees;
            }
            set
            {
                employees = value;
                OnPropertyChanged("Employees");
            }
        }

        private ICollection<Record> records;
        public ICollection<Record> Records
        {
            get
            {
                return records;
            }
            set
            {
                records = value;
                OnPropertyChanged("Records");
            }
        }

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
