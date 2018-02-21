using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace MeetUp.DBEntityModels
{
    public class Concil : IHasId, INotifyPropertyChanged
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

        private ICollection<Employee> employees;
        public ICollection<Employee> Employees
        {
            get
            {
                return employees;
            }
            set
            {
                employees = value;
                OnPropertyChanged("Employees");
            }
        }

        private ICollection<Meeting> meetings;
        public ICollection<Meeting> Meetings
        {
            get
            {
                return meetings;
            }
            set
            {
                meetings = value;
                OnPropertyChanged("Meetings");
            }
        }

        public Concil()
        {
            Employees = new List<Employee>();
            Meetings = new List<Meeting>();
        }

        public Concil(Concil concil)
        {
            Id = concil.Id;
            Name = concil.Name;
            Employees = concil.Employees?.ToList();
            Meetings = concil.Meetings?.ToList();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
