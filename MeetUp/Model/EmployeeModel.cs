using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetUp.Model
{
    class EmployeeModel
    {
        private MeetUpContext db;

        public EmployeeModel()
        {
            db = new MeetUpContext();
        }

        public ObservableCollection<Employee> GetEmployees()
        {        
            return new ObservableCollection<Employee>(db.Employees.ToList());            
        }

        public void AddEmployee(Employee employee)
        {
            db.Employees.Add(employee);
            db.SaveChanges();
        }

        public void RemoveEmployee(Employee employee)
        {
            db.Employees.Remove(employee);
            db.SaveChanges();
        }
    }
}
