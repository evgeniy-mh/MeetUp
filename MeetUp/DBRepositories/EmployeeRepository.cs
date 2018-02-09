using MeetUp.DB;
using MeetUp.DBEntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetUp.DBRepositories
{
    class EmployeeRepository
    {
        private EFGenericRepository<Employee> Repository;

        public EmployeeRepository()
        {
            Repository = new EFGenericRepository<Employee>(new MeetUpContext());
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return Repository.Get();
        }
    }
}
