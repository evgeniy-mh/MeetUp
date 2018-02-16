using System;
using System.Collections.Generic;

namespace MeetUp.DBEntityModels
{
    public class Meeting : IHasId
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? Date { get; set; }
        public bool IsCarriedOut { get; set; }
        public string Agenda { get; set; }

        public int? ConcilId { get; set; }
        public Concil Concil { get; set; }

        public ICollection<Employee> Employees { get; set; }
        public ICollection<Record> Records { get; set; }

        public Meeting()
        {
            Employees = new List<Employee>();
        }
    }
}
