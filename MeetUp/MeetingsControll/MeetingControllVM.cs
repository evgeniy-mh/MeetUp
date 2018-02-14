using MeetUp.DB;
using MeetUp.DBEntityModels;
using MeetUp.DBRepositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MeetUp.MeetingsControll
{
    class MeetingControllVM : INotifyPropertyChanged
    {
        public Meeting SelectedMeeting { get; set; }
        private EFGenericRepository<Meeting> MeetingRepository;
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

        public MeetingControllVM()
        {
            MeetingRepository = new EFGenericRepository<Meeting>(new MeetUpContext());
            Meetings = new ObservableCollection<Meeting>(MeetingRepository.GetAll());
        }

        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ?? (addCommand = new RelayCommand(obj =>
                {
                    /*EmployeeWindowView window = new EmployeeWindowView();
                    if (window.ShowDialog() == true)
                    {
                        EmployeeRepository.Create(window.Employee);
                        Employees = new ObservableCollection<Employee>(EmployeeRepository.GetAll());
                    }*/
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
                    /*EmployeeWindowView window = new EmployeeWindowView(SelectedEmployee);
                    if (window.ShowDialog() == true)
                    {
                        EmployeeRepository.Update(window.Employee);
                        Employees = new ObservableCollection<Employee>(EmployeeRepository.GetAll());
                    }*/
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
                    /*MessageBoxButton button = MessageBoxButton.OKCancel;
                    MessageBoxImage icon = MessageBoxImage.Warning;
                    MessageBoxResult result = MessageBox.Show(
                        String.Format("Вы действительно хотите удалить сотрудника {0} {1} {2} ?",
                        SelectedEmployee.MiddleName,
                        SelectedEmployee.FirstName,
                        SelectedEmployee.LastName),
                        "Удалить сотрудника?", button, icon);

                    if (result == MessageBoxResult.OK)
                    {
                        EmployeeRepository.Remove(SelectedEmployee);
                        Employees = new ObservableCollection<Employee>(EmployeeRepository.GetAll());
                    }*/
                }, (obj) => { return SelectedMeeting != null; }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
