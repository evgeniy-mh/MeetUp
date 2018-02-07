using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace MeetUp.EmployeeWindow
{
    class EmployeeWindowVM : INotifyPropertyChanged
    {
        private Window EmployeeWindowView;

        public Employee Employee { get; set; }

        public EmployeeWindowVM(Window employeeWindowView)
        {
            this.EmployeeWindowView = employeeWindowView;
            this.Employee = new Employee();
        }

        public EmployeeWindowVM(Window employeeWindowView, Employee employee)
        {
            this.EmployeeWindowView = employeeWindowView;
            this.Employee = new Employee(employee);
        }

        private RelayCommand accept_Click;
        public RelayCommand Accept_Click
        {
            get
            {
                return accept_Click ?? (accept_Click = new RelayCommand(obj =>
                {
                    EmployeeWindowView.DialogResult = true;
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
