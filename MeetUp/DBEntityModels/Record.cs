using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MeetUp.DBEntityModels
{
    public class Record : IHasId, INotifyPropertyChanged
    {
        public int Id { get; set; }


        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        private string content;
        public string Content
        {
            get
            {
                return content;
            }
            set
            {
                content = value;
                OnPropertyChanged("Content");
            }
        }

        public int? MeetingId { get; set; }

        private Meeting meeting;
        public Meeting Meeting
        {
            get
            {
                return meeting;
            }
            set
            {
                meeting = value;
                OnPropertyChanged("Meeting");
            }
        }

        public Record()
        {

        }

        public Record(Record record)
        {
            Id = record.Id;
            Name = record.Name;
            Content = record.Content;
            Meeting = record.Meeting == null ? new Meeting() : new Meeting(record.Meeting);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
