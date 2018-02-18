using MeetUp.DBEntityModels;
using MeetUp.DBRepositories;
using MeetUp.RecordWindow;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MeetUp.RecordsControll
{
    class RecordsControllVM : INotifyPropertyChanged
    {
        private UnitOfWork unitOfWork;
        public Record SelectedRecord { get; set; }
        private ObservableCollection<Record> _records;
        public ObservableCollection<Record> Records
        {
            get
            {
                return _records;
            }
            set
            {
                _records = value;
                OnPropertyChanged("Records");
            }
        }

        public RecordsControllVM()
        {
            unitOfWork = new UnitOfWork();
            Records = new ObservableCollection<Record>(unitOfWork.RecordRepository.Get("Meeting.Concil"));
        }

        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ?? (addCommand = new RelayCommand(obj =>
                {
                    /*MeetingWindowView window = new MeetingWindowView();
                    if (window.ShowDialog() == true)
                    {
                        unitOfWork.MeetingRepository.Create(window.Meeting);
                        Records = new ObservableCollection<Meeting>(unitOfWork.MeetingRepository.Get("Concil"));
                    }*/
                    RecordWindowView window = new RecordWindowView();
                    if (window.ShowDialog() == true)
                    {
                        
                    }
                }));
            }
        }

        private RelayCommand changeCommand;
        public RelayCommand ChangeCommand
        {
            get
            {
                return changeCommand ?? (changeCommand = new RelayCommand(obj =>
                {
                    /*MeetingWindowView window = new MeetingWindowView(SelectedRecord);
                    if (window.ShowDialog() == true)
                    {
                        unitOfWork.MeetingRepository.Update(window.Meeting);
                        Records = new ObservableCollection<Meeting>(unitOfWork.MeetingRepository.Get("Concil"));
                    }*/

                }, (obj) => { return SelectedRecord != null; }));
            }
        }

        private RelayCommand removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                return removeCommand ?? (removeCommand = new RelayCommand(obj =>
                {
                    /*MessageBoxButton button = MessageBoxButton.OKCancel;
                    MessageBoxImage icon = MessageBoxImage.Warning;
                    MessageBoxResult result = MessageBox.Show(
                        String.Format("Вы действительно хотите удалить заседание {0} ?",
                        SelectedRecord.Name),
                        "Удалить заседание?", button, icon);

                    if (result == MessageBoxResult.OK)
                    {
                        unitOfWork.MeetingRepository.Remove(SelectedRecord);
                        Records = new ObservableCollection<Meeting>(unitOfWork.MeetingRepository.Get("Concil"));
                    }*/
                }, (obj) => { return SelectedRecord != null; }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
