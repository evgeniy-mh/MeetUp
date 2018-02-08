using MeetUp.DBEntityModels;
using MeetUp.DBRepositories;
using MeetUp.EmployeeWindow;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace MeetUp.EmployeesControl
{
    class EmployeesControllVM : INotifyPropertyChanged
    {
        public Employee SelectedEmployee { get; set; }
        private EFGenericRepository<Employee> EmployeeRepository;
        private ObservableCollection<Employee> _employees;
        public ObservableCollection<Employee> Employees
        {
            get
            {
                return _employees;
            }
            set
            {
                _employees = value;
                OnPropertyChanged("Employees");
            }
        }

        public EmployeesControllVM()
        {
            EmployeeRepository = new EFGenericRepository<Employee>(new MeetUpContext());
            Employees = new ObservableCollection<Employee>(EmployeeRepository.Get());
        }

        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ?? (addCommand = new RelayCommand(obj =>
                {
                    EmployeeWindowView window = new EmployeeWindowView();
                    if (window.ShowDialog() == true)
                    {
                        EmployeeRepository.Create(window.Employee);
                        Employees = new ObservableCollection<Employee>(EmployeeRepository.Get());
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
                    EmployeeWindowView window = new EmployeeWindowView(SelectedEmployee);
                    if (window.ShowDialog() == true)
                    {
                        EmployeeRepository.Update(window.Employee);
                        Employees = new ObservableCollection<Employee>(EmployeeRepository.Get());
                    }
                }, (obj) => { return SelectedEmployee != null; }));
            }
        }

        private RelayCommand removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                return removeCommand ?? (removeCommand = new RelayCommand(obj =>
                {
                    MessageBoxButton button = MessageBoxButton.OKCancel;
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
                        Employees = new ObservableCollection<Employee>(EmployeeRepository.Get());
                    }
                }, (obj) => { return SelectedEmployee != null; }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
