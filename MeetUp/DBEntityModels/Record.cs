namespace MeetUp.DBEntityModels
{
    public class Record : IHasId
    {
        public int Id { get; set; }
        public string Content { get; set; }

        public int? MeetingId { get; set; }
        public Meeting Meeting { get; set; }
    }
}
