using MeetUp.DBEntityModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace MeetUp.DB
{
    class MeetUpContextInitializer : CreateDatabaseIfNotExists<MeetUpContext>
    {
        protected override void Seed(MeetUpContext db)
        {
            List<Employee> employees = new List<Employee>{
                new Employee{
                    FirstName ="Василий",
                    MiddleName ="Иванов",
                    LastName ="Иванович",
                    Position ="Директор",
                },

                new Employee{ FirstName="Петр",
                    MiddleName ="Мизинцев",
                    LastName ="Иванович",
                    Position ="Преподаватель",
                },

                new Employee{
                    FirstName ="Николай",
                    MiddleName ="Меньшов",
                    LastName ="Петрович",
                    Position ="Научный сотрудник",
                },
            };
            db.Employees.AddRange(employees);

            List<Concil> concils = new List<Concil>{
                new Concil{
                    Name ="Общий совет",
                    Employees =new List<Employee>{employees[0], employees[1] }
                },

                new Concil{
                    Name ="Особо важный совет",
                    Employees =employees
                },

                new Concil{
                    Name ="Научный совет",
                    Employees =new List<Employee>{employees[1], employees[2] }
                },
            };
            db.Concils.AddRange(concils);


            List<Record> records = new List<Record> {
                new Record{Name="Название протокола 1",Content="1 Содержание протокола собрания "},
                new Record{Name="Название протокола 2",Content="2 Содержание протокола собрания "},
                new Record{Name="Название протокола 3",Content="3 Содержание протокола собрания "},
                new Record{Name="Название протокола 4",Content="Особо важный протокол собрания "}
            };
            db.Records.AddRange(records);

            DateTime localDate = DateTime.Now;
            List<Meeting> meetings = new List<Meeting> {
                new Meeting{
                    Name ="Первое заседание",
                    Date =localDate,
                    Concil =concils[0],
                    Agenda ="Очень длинная повестка собрания",
                    Employees=employees,
                    IsCarriedOut=true,
                    Records=new List<Record>{ records[0] }
                },

                new Meeting{
                    Name ="Заседание по вопросам проведения олимпады",
                    Date =localDate.AddDays(5),
                    Concil =concils[2],
                    Agenda ="Очень длинная повестка собрания",
                    Employees=new List<Employee>{ employees[0], employees[1] },
                    IsCarriedOut=false,
                    Records=new List<Record>{ records[1], records[2] }
                },

                new Meeting{
                    Name ="Особо важное заседание",
                    Date =localDate.AddDays(7),
                    Concil =concils[1],
                    Agenda ="Очень длинная повестка собрания",
                    Employees=new List<Employee>{ employees[0], employees[1] },
                    IsCarriedOut=false,
                    Records=new List<Record>{ records[3] }
                }
            };
            db.Meetings.AddRange(meetings);

            db.SaveChanges();
        }
    }
}