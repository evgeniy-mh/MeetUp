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
    }
}
