using MeetUp.DBEntityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetUp
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

        public ICollection<Conference> Conferences { get; set; }

        public Employee()
        {
            Conferences = new List<Conference>();
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
            Conferences = employee.Conferences.ToList();
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
                            error = "sadsad";
                        }
                        break;
                }
                return error;
            }
        }
    }
}
