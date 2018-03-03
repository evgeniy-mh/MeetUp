using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace MeetUp.DBEntityModels
{
    public class Employee : IDataErrorInfo, IHasId, INotifyPropertyChanged
    {
        public int Id { get; set; }

        private string firstName;
        private string middleName;
        private string lastName;
        private string position;
        private DateTime? birthDate;
        private string telephoneNumber;
        private string email;
        private ICollection<Concil> concils;
        private ICollection<Meeting> meetings;

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
                    case "Position":
                        if (Position == null || Position.Length == 0)
                        {
                            error = "Вы не ввели должность сотрудника";
                        }
                        break;
                    case "TelephoneNumber":
                        Regex regex = new Regex("[^0-9]");
                        if (TelephoneNumber==null || regex.IsMatch(TelephoneNumber))
                        {
                            error = "Вы неправильно ввели номер сотрудника";
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
