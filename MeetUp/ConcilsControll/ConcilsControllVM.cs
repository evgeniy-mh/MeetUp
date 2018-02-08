using MeetUp.DB;
using MeetUp.DBEntityModels;
using MeetUp.DBRepositories;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MeetUp.ConcilsControll
{
    class ConcilsControllVM : INotifyPropertyChanged
    {
        public Concil SelectedConcil { get; set; }
        private EFGenericRepository<Concil> ConcilRepository;
        private ObservableCollection<Concil> _concils;
        public ObservableCollection<Concil> Concils
        {
            get
            {
                return _concils;
            }
            set
            {
                _concils = value;
                OnPropertyChanged("Concils");
            }
        }

        public ConcilsControllVM()
        {
            ConcilRepository = new EFGenericRepository<Concil>(new MeetUpContext());
            Concils = new ObservableCollection<Concil>(ConcilRepository.Get());
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
