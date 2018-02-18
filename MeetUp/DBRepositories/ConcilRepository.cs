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
    class ConcilRepository : EFGenericRepository<Concil>
    {
        public ConcilRepository(MeetUpContext context) : base(context)
        {
        }

        public new void Create(Concil concil)
        {
            if (concil.Employees == null || concil.Employees.Count == 0)
            {
                base.Create(concil);
            }
            else
            {
                foreach (Employee e in concil.Employees)
                {
                    e.Concils.Clear();
                    Context.Employees.Attach(e);
                }
                base.Create(concil);
            }
        }

        public void AddEmployeeToConcil(Concil concil, Employee employee)
        {
            var c = Context.Concils.Find(concil.Id);
            var e = Context.Employees.Find(employee.Id);
            c.Employees.Add(e);
            Context.SaveChanges();
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
