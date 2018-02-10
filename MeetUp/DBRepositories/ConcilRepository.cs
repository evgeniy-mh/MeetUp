using MeetUp.DB;
using MeetUp.DBEntityModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetUp.DBRepositories
{
    class ConcilRepository : EFGenericRepository<Concil>
    {
        private EmployeeRepository EmployeeRepository;

        public ConcilRepository(DbContext context) : base(context)
        {
            EmployeeRepository = new EmployeeRepository(context);
        }

        public void AddEmployeeToConcil(Concil concil, Employee employee)
        {
            Concil resultConcil = DBSet.Include("Employees").SingleOrDefault(c => c.Id == concil.Id);
            Employee resultEmployee = EmployeeRepository.GetAllFreeEmployeesForConcil(resultConcil).FirstOrDefault(e => e.Id == employee.Id);

            if (resultEmployee != null)
            {
                resultConcil.Employees.Add(resultEmployee);
                Context.SaveChanges();
            }
        }

        public class ConcilIdComparer : IEqualityComparer<Concil>
        {
            public bool Equals(Concil x, Concil y)
            {
                return x.Id == y.Id;
            }

            public int GetHashCode(Concil obj)
            {
                return obj.GetHashCode();
            }
        }
    }
}
