namespace MeetUp.DBEntityModels
{
    public class Record : IHasId
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }

        public int? MeetingId { get; set; }
        public Meeting Meeting { get; set; }

        public Record()
        {

        }

        public Record(Record record)
        {
            Id = record.Id;
            Name = record.Name;
            Content = record.Content;
            Meeting = new Meeting(record.Meeting);
        }
    }
}
