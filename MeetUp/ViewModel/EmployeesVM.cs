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

        /*public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
                MessageBox.Show(info);
            }
        }*/

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
                    AddEmployeeWindow window = new AddEmployeeWindow();
                    if (window.ShowDialog() == true)
                    {
                        employeeModel.AddEmployee(window.NewEmployee);
                        Employees = employeeModel.GetEmployees();
                    }
                }));
            }
        }

        private RelayCommand removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                return removeCommand ?? (removeCommand = new RelayCommand(obj =>
                {
                    MessageBox.Show("remove");
                }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
