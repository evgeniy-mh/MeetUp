using MeetUp.DBEntityModels;
using MeetUp.DBRepositories;
using MeetUp.RecordWindow;
using MeetUp.SelectConcilWindow;
using MeetUp.SelectEmployeeWindow;
using MeetUp.SelectRecordWindow;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace MeetUp.MeetingWindow
{
    class MeetingWindowVM : INotifyPropertyChanged
    {
        private UnitOfWork unitOfWork;
        private MeetingWindowView meetingWindowView;
        private bool IsCreatingNewMeeting;

        public Meeting Meeting { get; set; }

        public ObservableCollection<Record> MeetingRecords { get; set; }
        public ObservableCollection<Employee> MeetingEmployees { get; set; }

        public Record SelectedRecord { get; set; }
        public Employee SelectedEmployee { get; set; }

        public MeetingWindowVM(MeetingWindowView meetingWindowView)
        {
            unitOfWork = new UnitOfWork();
            this.meetingWindowView = meetingWindowView;
            IsCreatingNewMeeting = true;
            Meeting = new Meeting();
            MeetingRecords = new ObservableCollection<Record>();
            MeetingEmployees = new ObservableCollection<Employee>();
        }

        public MeetingWindowVM(MeetingWindowView meetingWindowView, Meeting meeting)
        {
            unitOfWork = new UnitOfWork();
            this.meetingWindowView = meetingWindowView;
            IsCreatingNewMeeting = false;
            Meeting = new Meeting(meeting);
            MeetingRecords = new ObservableCollection<Record>(Meeting.Records);
            MeetingEmployees = new ObservableCollection<Employee>(Meeting.Employees);
        }

        private RelayCommand selectConcilCommand;
        public RelayCommand SelectConcilCommand
        {
            get
            {
                return selectConcilCommand ?? (selectConcilCommand = new RelayCommand(obj =>
                {
                    SelectConcilWindowView window = new SelectConcilWindowView();
                    if (window.ShowDialog() == true)
                    {
                        if (IsCreatingNewMeeting)
                        {
                            Meeting.Concil = window.SelectedConcil;
                        }
                        else
                        {
                            Meeting.Concil = window.SelectedConcil;
                        }
                    }
                }));
            }
        }

        private RelayCommand addRecordCommand;
        public RelayCommand AddRecordCommand
        {
            get
            {
                return addRecordCommand ?? (addRecordCommand = new RelayCommand(obj =>
                {
                    var freeRecords = unitOfWork.RecordRepository.GetFreeRecords();
                    SelectRecordWindowView window = new SelectRecordWindowView(freeRecords);
                    if (window.ShowDialog() == true)
                    {
                        MeetingRecords.Add(window.SelectedRecord);
                    }
                }));
            }
        }

        private RelayCommand removeRecordCommand;
        public RelayCommand RemoveRecordCommand
        {
            get
            {
                return removeRecordCommand ?? (removeRecordCommand = new RelayCommand(obj =>
                {
                    MeetingRecords.Remove(SelectedRecord);
                }, (obj) => { return SelectedRecord != null; }));
            }
        }

        private RelayCommand showRecordCommand;
        public RelayCommand ShowRecordCommand
        {
            get
            {
                return showRecordCommand ?? (showRecordCommand = new RelayCommand(obj =>
                {
                    RecordWindowView window = new RecordWindowView(SelectedRecord);
                    window.IsEditableRecord = false;
                    window.ShowDialog();
                }, (obj) => { return SelectedRecord != null; }));
            }
        }

        private RelayCommand addEmployeeCommand;
        public RelayCommand AddEmployeeCommand
        {
            get
            {
                return addEmployeeCommand ?? (addEmployeeCommand = new RelayCommand(obj =>
                {
                    IEnumerable<Employee> freeEmployees;
                    if (IsCreatingNewMeeting)
                    {
                        freeEmployees = unitOfWork.EmployeeRepository.GetAll().Except(MeetingEmployees, new EmployeeRepository.EmployeeIdComparer());
                    }
                    else
                    {
                        freeEmployees = unitOfWork.EmployeeRepository.GetAllFreeEmployeesForMeeting(Meeting).Except(MeetingEmployees, new EmployeeRepository.EmployeeIdComparer());
                    }

                    SelectEmployeeWindowView window = new SelectEmployeeWindowView(freeEmployees);
                    if (window.ShowDialog() == true)
                    {
                        MeetingEmployees.Add(window.SelectedEmployee);
                    }

                }));
            }
        }

        private RelayCommand removeEmployeeCommand;
        public RelayCommand RemoveEmployeeCommand
        {
            get
            {
                return removeEmployeeCommand ?? (removeEmployeeCommand = new RelayCommand(obj =>
                {
                    MeetingEmployees.Remove(SelectedEmployee);
                }, (obj) => { return SelectedEmployee != null; }));
            }
        }

        private RelayCommand showEmployeeCommand;
        public RelayCommand ShowEmployeeCommand
        {
            get
            {
                return showEmployeeCommand ?? (showEmployeeCommand = new RelayCommand(obj =>
                {

                }, (obj) => { return SelectedEmployee != null; }));
            }
        }

        private RelayCommand accept_Click;
        public RelayCommand Accept_Click
        {
            get
            {
                return accept_Click ?? (accept_Click = new RelayCommand(obj =>
                {
                    Meeting.Records = MeetingRecords;
                    Meeting.Employees = MeetingEmployees;

                    meetingWindowView.DialogResult = true;
                }, (obj) => { return IsAllFieldsValid(meetingWindowView.MeetingInfoPanel); }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private bool IsAllFieldsValid(DependencyObject obj)
        {
            foreach (object child in LogicalTreeHelper.GetChildren(obj))
            {
                TextBox element = child as TextBox;
                if (element == null) continue;

                if (Validation.GetHasError(element))
                {
                    return false;
                }

                IsAllFieldsValid(element);
            }
            return true;
        }

        public void OnWindowClosing(object sender, CancelEventArgs e)
        {
            unitOfWork.Dispose();
        }
    }
}
