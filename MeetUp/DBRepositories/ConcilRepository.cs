using MeetUp.DB;
using MeetUp.DBEntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetUp.DBRepositories
{
    class ConcilRepository
    {
        private EFGenericRepository<Concil> Repository;

        public ConcilRepository()
        {
            Repository = new EFGenericRepository<Concil>(new MeetUpContext());
        }

        public void Create(Concil concil)
        {
            Repository.Create(concil);
        }

        public IEnumerable<Concil> GetConcils()
        {
            return Repository.Get();
        }

        public ICollection<Employee> GetEmployees(Concil concil)
        {
            return Repository.Get("Employees").FirstOrDefault((c) => { return c.Id == concil.Id; }).Employees;
        }

    }
}
