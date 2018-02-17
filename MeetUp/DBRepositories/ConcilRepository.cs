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

        public ConcilRepository(MeetUpContext context) : base(context)
        {
            //EmployeeRepository = new EmployeeRepository();
        }

        public new void Create(Concil concil)
        {
            if (concil.Employees == null || concil.Employees.Count == 0)
            {
                base.Create(concil);
                //Context.SaveChanges();
            }
            else
            {
                Concil c = new Concil { Name = concil.Name };
                Create(c);

                foreach (Employee e in concil.Employees)
                {
                    AddEmployeeToConcil(c, e);
                }
            }

        }

        public void AddEmployeeToConcil(Concil concil, Employee employee)
        {
            /*_dbSet = Context.Set<Concil>();
            Concil resultConcil = _dbSet.Include("Employees").SingleOrDefault(c => c.Id == concil.Id);
            Employee resultEmployee = EmployeeRepository.GetAllFreeEmployeesForConcil(resultConcil).FirstOrDefault(e => e.Id == employee.Id);

            if (resultEmployee != null)
            {
                resultConcil.Employees.Add(resultEmployee);
                Context.SaveChanges();
            }*/
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
