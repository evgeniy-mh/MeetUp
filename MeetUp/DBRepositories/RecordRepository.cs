using MeetUp.DB;
using MeetUp.DBEntityModels;

namespace MeetUp.DBRepositories
{
    class RecordRepository : EFGenericRepository<Record>
    {
        public RecordRepository(MeetUpContext context) : base(context)
        {
        }
    }
}
