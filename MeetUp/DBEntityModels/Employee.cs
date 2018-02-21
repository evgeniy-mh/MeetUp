using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace MeetUp.DBEntityModels
{
    public class Employee : IDataErrorInfo, IHasId, INotifyPropertyChanged
    {
        public int Id { get; set; }

        private string firstName;
        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                firstName = value;
                OnPropertyChanged("FirstName");
            }
        }

        private string middleName;
        public string MiddleName
        {
            get
            {
                return middleName;
            }
            set
            {
                middleName = value;
                OnPropertyChanged("MiddleName");
            }
        }

        private string lastName;
        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                lastName = value;
                OnPropertyChanged("LastName");
            }
        }

        private string position;
        public string Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
                OnPropertyChanged("Position");
            }
        }

        private DateTime? birthDate;
        public DateTime? BirthDate
        {
            get
            {
                return birthDate;
            }
            set
            {
                birthDate = value;
                OnPropertyChanged("BirthDate");
            }
        }

        private string telephoneNumber;
        public string TelephoneNumber
        {
            get
            {
                return telephoneNumber;
            }
            set
            {
                telephoneNumber = value;
                OnPropertyChanged("TelephoneNumber");
            }
        }

        private string email;
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }

        private ICollection<Concil> concils;
        public ICollection<Concil> Concils
        {
            get
            {
                return concils;
            }
            set
            {
                concils = value;
                OnPropertyChanged("Concils");
            }
        }

        private ICollection<Meeting> meetings;
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

        public Employee()
        {
            Concils = new List<Concil>();
            Meetings = new List<Meeting>();
        }

        public Employee(Employee employee)
        {
            Id = employee.Id;
            FirstName = employee.FirstName;
            MiddleName = employee.MiddleName;
            LastName = employee.LastName;
            Position = employee.Position;
            BirthDate = employee.BirthDate;
            TelephoneNumber = employee.TelephoneNumber;
            Email = employee.Email;
            Concils = employee.Concils.ToList();
            Meetings = employee.Meetings.ToList();
        }

        public string Error => throw new NotImplementedException();
        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "FirstName":
                        if (FirstName == null || FirstName.Length == 0)
                        {
                            error = "Вы не ввели имя сотрудника";
                        }
                        break;
                    case "MiddleName":
                        if (MiddleName == null || MiddleName.Length == 0)
                        {
                            error = "Вы не ввели фамилию сотрудника";
                        }
                        break;
                    case "LastName":
                        if (LastName == null || LastName.Length == 0)
                        {
                            error = "Вы не ввели отчество сотрудника";
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
