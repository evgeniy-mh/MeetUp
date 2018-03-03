using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MeetUp.DBEntityModels
{
    public class Record : IHasId, INotifyPropertyChanged, IDataErrorInfo
    {
        public int Id { get; set; }
        public int? MeetingId { get; set; }

        private string name;
        private string content;
        private Meeting meeting;

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

        public Record() { }

        public Record(Record record)
        {
            Id = record.Id;
            Name = record.Name;
            Content = record.Content;
            Meeting = record.Meeting == null ? new Meeting() : new Meeting(record.Meeting);
        }

        public string Error => throw new NotImplementedException();
        public string this[string columnName]
        {
            get
            {
                string error = String.Empty;
                switch (columnName)
                {
                    case "Name":
                        if (Name == null || Name.Length == 0)
                        {
                            error = "Вы не ввели название протокола собрания";
                        }
                        break;
                }
                return error;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
