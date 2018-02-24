using MeetUp.DB;
using MeetUp.DBEntityModels;
using System.Collections.Generic;
using System.Linq;

namespace MeetUp.DBRepositories
{
    class EmployeeRepository : EFGenericRepository<Employee>
    {
        public EmployeeRepository(MeetUpContext context) : base(context)
        {

        }

        public IEnumerable<Employee> GetEmployeesForConcil(Concil concil)
        {
            return Get("Concils").Where((employee) =>
            {
                return employee.Concils.Contains(concil, new ConcilRepository.ConcilIdComparer());
            });
        }

        //все работники что не входят в данный совет
        public IEnumerable<Employee> GetAllFreeEmployeesForConcil(Concil concil)
        {
            if (concil.Employees.Count == 0)
            {
                return Get("Concils");
            }
            else
            {
                return Get("Concils").Where((employee) =>
                {
                    return !employee.Concils.Contains(concil, new ConcilRepository.ConcilIdComparer());
                });
            }
        }

        public IEnumerable<Employee> GetEmployeesForMeeting(Meeting meeting)
        {
            var m = Context.Meetings.Find(meeting.Id);
            Context.Entry(m).Collection(m2 => m2.Employees).Load();
            return m.Employees;
        }

        //все employee что не входят в данный meeting
        public IEnumerable<Employee> GetAllFreeEmployeesForMeeting(Meeting meeting)
        {
            if (meeting.Employees.Count == 0)
            {
                return Get("Meetings");
            }
            else
            {
                return Get("Meetings").Where(employee =>
                {
                    return !employee.Meetings.Contains(meeting, new MeetingRepository.MeetingIdComparer());
                });
            }
        }

        public class EmployeeIdComparer : IEqualityComparer<Employee>
        {
            public bool Equals(Employee x, Employee y)
            {
                return x.Id == y.Id;
            }

            public int GetHashCode(Employee obj)
            {
                return obj.GetHashCode();
            }
        }

        public IEnumerable<Employee> SearchEmployees(string query)
        {
            query = query.ToLower();
            return GetAll().Where(e =>
            {
                return
                (e.FirstName != null && e.FirstName.ToLower().Contains(query)) ||
                (e.MiddleName != null && e.MiddleName.ToLower().Contains(query)) ||
                (e.LastName != null && e.LastName.ToLower().Contains(query)) ||
                (e.Position != null && e.Position.ToLower().Contains(query)) ||
                (e.TelephoneNumber != null && e.TelephoneNumber.ToLower().Contains(query)) ||
                (e.Email != null && e.Email.ToLower().Contains(query));
            });

            /*var employees = GetAll();
            foreach (Employee e in employees)
            {
                Context.Entry.
            }*/
        }
    }
}
