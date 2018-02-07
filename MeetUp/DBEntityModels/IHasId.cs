using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetUp.DBEntityModels
{
    interface IHasId
    {
        int Id { get; set; }
    }
}
