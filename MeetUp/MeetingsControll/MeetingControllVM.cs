using MeetUp.DBEntityModels;
using MeetUp.DBRepositories;
using MeetUp.MeetingWindow;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace MeetUp.MeetingsControll
{
    class MeetingControllVM : INotifyPropertyChanged
    {
        private MeetingControll meetingControllUserControll;
        private UnitOfWork unitOfWork;
        public Meeting SelectedMeeting { get; set; }
        private ObservableCollection<Meeting> _meetings;
        public ObservableCollection<Meeting> Meetings
        {
            get
            {
                return _meetings;
            }
            set
            {
                _meetings = value;
                OnPropertyChanged("Meetings");
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
                Search(SearchString, SearchFromDate, SearchToDate, IsCarriedOutCheckBox);
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
                Search(SearchString, SearchFromDate, SearchToDate, IsCarriedOutCheckBox);
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
                Search(SearchString, SearchFromDate, SearchToDate, IsCarriedOutCheckBox);
            }
        }

        private bool? isCarriedOutCheckBox;
        public bool? IsCarriedOutCheckBox
        {
            get
            {
                return isCarriedOutCheckBox;
            }
            set
            {
                isCarriedOutCheckBox = value;
                Search(SearchString, SearchFromDate, SearchToDate, IsCarriedOutCheckBox);
            }
        }

        public MeetingControllVM(MeetingControll meetingControllUserControll)
        {
            this.meetingControllUserControll = meetingControllUserControll;
            unitOfWork = new UnitOfWork();
            Meetings = new ObservableCollection<Meeting>(unitOfWork.MeetingRepository.Get("Concil"));
        }

        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ?? (addCommand = new RelayCommand(obj =>
                {
                    MeetingWindowView window = new MeetingWindowView();
                    if (window.ShowDialog() == true)
                    {
                        unitOfWork.MeetingRepository.Create(window.Meeting);
                        Meetings = new ObservableCollection<Meeting>(unitOfWork.MeetingRepository.Get("Concil"));
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
                    SelectedMeeting.Records = new ObservableCollection<Record>(unitOfWork.RecordRepository.GetRecordsForMeeting(SelectedMeeting));
                    SelectedMeeting.Employees = new ObservableCollection<Employee>(unitOfWork.EmployeeRepository.GetEmployeesForMeeting(SelectedMeeting));

                    MeetingWindowView window = new MeetingWindowView(SelectedMeeting);
                    if (window.ShowDialog() == true)
                    {
                        unitOfWork.MeetingRepository.Update(window.Meeting);
                        Meetings = new ObservableCollection<Meeting>(unitOfWork.MeetingRepository.Get("Concil"));
                    }

                }, (obj) => { return SelectedMeeting != null; }));
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
                        String.Format("Вы действительно хотите удалить заседание {0} ?", SelectedMeeting.Name),
                        "Удалить заседание?",
                        MessageBoxButton.OKCancel,
                        MessageBoxImage.Warning);

                    if (result == MessageBoxResult.OK)
                    {
                        MessageBoxResult isDeleteWithRecords = MessageBox.Show(
                            "Удалить заседание вместе с его протоколами ?",
                            "Удалить заседание вместе с его протоколами ?",
                            MessageBoxButton.YesNo,
                            MessageBoxImage.Warning);

                        if (isDeleteWithRecords == MessageBoxResult.Yes)
                        {
                            unitOfWork.MeetingRepository.Remove(SelectedMeeting, true);
                        }
                        else
                        {
                            unitOfWork.MeetingRepository.Remove(SelectedMeeting, false);
                        }
                        Meetings = new ObservableCollection<Meeting>(unitOfWork.MeetingRepository.Get("Concil"));
                    }
                }, (obj) => { return SelectedMeeting != null; }));
            }
        }

        private void Search(string queryName, DateTime? queryFromDate, DateTime? queryToDate, bool? queryIsCarriedOut)
        {
            Meetings = new ObservableCollection<Meeting>(unitOfWork.MeetingRepository.SearchMeetings(queryName, queryFromDate, queryToDate, queryIsCarriedOut));
        }

        private RelayCommand clearSearchStringCommand;
        public RelayCommand ClearSearchStringCommand
        {
            get
            {
                return clearSearchStringCommand ?? (clearSearchStringCommand = new RelayCommand(query =>
                {
                    meetingControllUserControll.SearchTextBox.Text = "";
                }, (obj) =>
                {
                    return meetingControllUserControll.SearchTextBox.Text.Length > 0;
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
                    meetingControllUserControll.fromDateDatePicker.SelectedDate = null;
                }, (obj) => { return meetingControllUserControll.fromDateDatePicker.SelectedDate != null; }));
            }
        }

        private RelayCommand clearToDateDatePicker;
        public RelayCommand ClearToDateDatePicker
        {
            get
            {
                return clearToDateDatePicker ?? (clearToDateDatePicker = new RelayCommand(obj =>
                {
                    meetingControllUserControll.toDateDatePicker.SelectedDate = null;
                }, (obj) => { return meetingControllUserControll.toDateDatePicker.SelectedDate != null; }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
