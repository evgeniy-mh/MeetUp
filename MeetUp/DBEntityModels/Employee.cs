using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace MeetUp.DBEntityModels
{
    public class Employee : IDataErrorInfo, IHasId
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public DateTime? BirthDate { get; set; }
        public string TelephoneNumber { get; set; }
        public string Email { get; set; }

        public ICollection<Concil> Councils { get; set; }
        public ICollection<Meeting> Meetings { get; set; }

        public Employee()
        {
            Councils = new List<Concil>();
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
            Councils = employee.Councils.ToList();
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
    }
}
