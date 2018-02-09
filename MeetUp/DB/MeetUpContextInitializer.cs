using MeetUp.DBEntityModels;
using System.Collections.Generic;
using System.Data.Entity;

namespace MeetUp.DB
{
    class MeetUpContextInitializer : CreateDatabaseIfNotExists<MeetUpContext>
    {
        protected override void Seed(MeetUpContext db)
        {
            List<Employee> employees = new List<Employee>{
                new Employee{ FirstName="Василий", MiddleName="Иванов", LastName="Иванович", Position="Директор",},
                new Employee{ FirstName="Петр", MiddleName="Мизинцев", LastName="Иванович", Position="Преподаватель",},
                new Employee{ FirstName="Николай", MiddleName="Меньшов", LastName="Петрович", Position="Научный сотрудник",},
            };

            List<Concil> concils = new List<Concil>{
                new Concil{Name="Общий совет", Employees=new List<Employee>{employees[0], employees[1] } },
                new Concil{Name="Особо важный совет", Employees=employees },
                new Concil{Name="Научный совет", Employees=new List<Employee>{employees[1], employees[2] } },
            };

            db.Employees.AddRange(employees);
            db.Concils.AddRange(concils);
            db.SaveChanges();
        }
    }
}