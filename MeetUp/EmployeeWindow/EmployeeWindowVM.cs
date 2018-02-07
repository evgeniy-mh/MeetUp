using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace MeetUp.EmployeeWindow
{
    class EmployeeWindowVM : INotifyPropertyChanged
    {
        private EmployeeWindowView employeeWindowView;

        public Employee Employee { get; set; }

        public EmployeeWindowVM(EmployeeWindowView employeeWindowView)
        {
            this.employeeWindowView = employeeWindowView;
            this.Employee = new Employee();
        }

        public EmployeeWindowVM(EmployeeWindowView employeeWindowView, Employee employee)
        {
            this.employeeWindowView = employeeWindowView;
            this.Employee = new Employee(employee);
        }

        private RelayCommand accept_Click;
        public RelayCommand Accept_Click
        {
            get
            {
                return accept_Click ?? (accept_Click = new RelayCommand(obj =>
                {
                    employeeWindowView.DialogResult = true;
                }, (obj) => { return IsAllFieldsValid(employeeWindowView.EmployeeInfoPanel); }));
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
    }
}
