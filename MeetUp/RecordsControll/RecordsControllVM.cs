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
        private RecordControll recordControllUserControll;
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

        public RecordsControllVM(RecordControll recordControllUserControll)
        {
            this.recordControllUserControll = recordControllUserControll;
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
                        unitOfWork.RecordRepository.Update(window.Record);
                        Records = new ObservableCollection<Record>(unitOfWork.RecordRepository.Get("Meeting.Concil"));
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

        private string searchString;
        public string SearchString
        {
            get
            {
                return searchString;
            }
            set
            {
                searchString = value;
                Search(SearchString, SearchFromDate, SearchToDate);
            }
        }

        private DateTime? searchFromDate;
        public DateTime? SearchFromDate
        {
            get
            {
                return searchFromDate;
            }
            set
            {
                searchFromDate = value;
                Search(SearchString, SearchFromDate, SearchToDate);
            }
        }

        private DateTime? searchToDate;
        public DateTime? SearchToDate
        {
            get
            {
                return searchToDate;
            }
            set
            {
                searchToDate = value;
                Search(SearchString, SearchFromDate, SearchToDate);
            }
        }

        private void Search(string queryName, DateTime? queryFromDate, DateTime? queryToDate)
        {
            Records = new ObservableCollection<Record>(unitOfWork.RecordRepository.SearchRecords(queryName, queryFromDate, queryToDate));
        }

        private RelayCommand clearSearchStringCommand;
        public RelayCommand ClearSearchStringCommand
        {
            get
            {
                return clearSearchStringCommand ?? (clearSearchStringCommand = new RelayCommand(query =>
                {
                    recordControllUserControll.SearchTextBox.Text = "";
                }, (obj) =>
                {
                    return recordControllUserControll.SearchTextBox.Text.Length > 0;
                }));
            }
        }

        private RelayCommand clearFromDateDatePicker;
        public RelayCommand ClearFromDateDatePicker
        {
            get
            {
                return clearFromDateDatePicker ?? (clearFromDateDatePicker = new RelayCommand(obj =>
                {
                    recordControllUserControll.fromDateDatePicker.SelectedDate = null;
                }, (obj) => { return recordControllUserControll.fromDateDatePicker.SelectedDate != null; }));
            }
        }

        private RelayCommand clearToDateDatePicker;
        public RelayCommand ClearToDateDatePicker
        {
            get
            {
                return clearToDateDatePicker ?? (clearToDateDatePicker = new RelayCommand(obj =>
                {
                    recordControllUserControll.toDateDatePicker.SelectedDate = null;
                }, (obj) => { return recordControllUserControll.toDateDatePicker.SelectedDate != null; }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
