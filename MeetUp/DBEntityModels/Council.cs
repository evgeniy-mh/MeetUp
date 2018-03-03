using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace MeetUp.DBEntityModels
{
    public class Concil : IHasId, INotifyPropertyChanged, IDataErrorInfo
    {
        public int Id { get; set; }

        private string name;
        private ICollection<Employee> employees;
        private ICollection<Meeting> meetings;

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
        
        public ICollection<Meeting> Meetings
        {
            get
            {
                return meetings;
            }
            set
            {
                meetings = value;
                OnPropertyChanged("Meetings");
            }
        }

        public Concil()
        {
            Employees = new List<Employee>();
            Meetings = new List<Meeting>();
        }

        public Concil(Concil concil)
        {
            Id = concil.Id;
            Name = concil.Name;
            Employees = concil.Employees?.ToList();
            Meetings = concil.Meetings?.ToList();
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
                            error = "Вы не ввели название совета";
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
