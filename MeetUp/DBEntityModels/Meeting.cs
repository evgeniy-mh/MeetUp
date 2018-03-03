using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace MeetUp.DBEntityModels
{
    public class Meeting : IHasId, INotifyPropertyChanged, IDataErrorInfo
    {
        public int Id { get; set; }
        public int? ConcilId { get; set; }

        private string name;
        private DateTime? date;
        private bool isCarriedOut;
        private string agenda;
        private Concil concil;
        private ICollection<Employee> employees;
        private ICollection<Record> records;

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

        public string Error => throw new NotImplementedException();
        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "Name":
                        if (Name == null || Name.Length == 0)
                        {
                            error = "Вы не ввели название собрания";
                        }
                        break;
                }
                return error;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
