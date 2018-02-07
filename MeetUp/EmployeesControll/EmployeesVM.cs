using MeetUp.Model;
using MeetUp.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MeetUp
{
    class EmployeesVM : INotifyPropertyChanged
    {
        public Employee SelectedEmployee { get; set; }

        private EmployeeModel employeeModel;

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

        public EmployeesVM()
        {
            employeeModel = new EmployeeModel();
            Employees = employeeModel.GetEmployees();
        }

        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ?? (addCommand = new RelayCommand(obj =>
                {
                    //AddEmployeeWindow window = new AddEmployeeWindow();
                    
                    EmployeeWindow window = new EmployeeWindow(new Employee());
                    if (window.ShowDialog() == true)
                    {
                        employeeModel.AddEmployee(window.Employee);
                        Employees = employeeModel.GetEmployees();
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
                    EmployeeWindow window = new EmployeeWindow(SelectedEmployee);
                    if (window.ShowDialog() == true)
                    {
                        employeeModel.UpdateEmployee(window.Employee);
                        Employees = employeeModel.GetEmployees();
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
                        employeeModel.RemoveEmployee(SelectedEmployee);
                        Employees = employeeModel.GetEmployees();
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
