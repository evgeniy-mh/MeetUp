using MeetUp.DBEntityModels;
using MeetUp.DBRepositories;
using MeetUp.RecordWindow;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

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
                    RecordWindowView window = new RecordWindowView();
                    if (window.ShowDialog() == true)
                    {
                        unitOfWork.RecordRepository.Create(window.Record);
                        Records = new ObservableCollection<Record>(unitOfWork.RecordRepository.Get("Meeting.Concil"));
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
                    RecordWindowView window = new RecordWindowView(SelectedRecord);
                    if (window.ShowDialog() == true)
                    {
                        
                    }
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
                    MessageBoxResult result = MessageBox.Show(
                        String.Format("Вы действительно хотите удалить протокол заседания {0} ?", SelectedRecord.Name),
                        "Удалить заседание?", 
                        MessageBoxButton.OKCancel, 
                        MessageBoxImage.Warning);

                    if (result == MessageBoxResult.OK)
                    {
                        unitOfWork.RecordRepository.Remove(SelectedRecord);
                        Records = new ObservableCollection<Record>(unitOfWork.RecordRepository.Get("Meeting.Concil"));
                    }
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
