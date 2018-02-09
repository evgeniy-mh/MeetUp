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
        public ConcilRepository(DbContext context) : base(context)
        {
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
