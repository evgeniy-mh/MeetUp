﻿using MeetUp.DB;
using MeetUp.DBEntityModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetUp.DBRepositories
{
    class EmployeeRepository : EFGenericRepository<Employee>
    {
        public EmployeeRepository(DbContext context) : base(context)
        {

        }

        public IEnumerable<Employee> GetEmployeesForConcil(Concil concil)
        {
            return Get("Concils").Where((employee) =>
            {
                return employee.Concils.Contains(concil, new ConcilRepository.ConcilIdComparer());
            });
        }

        //все работник что не входят в данный совет
        public IEnumerable<Employee> GetAllFreeEmployeesForConcil(Concil concil)
        {
            return Get("Concils").Where((employee) =>
            {
                return !employee.Concils.Contains(concil, new ConcilRepository.ConcilIdComparer());
            });
        }
    }
}
