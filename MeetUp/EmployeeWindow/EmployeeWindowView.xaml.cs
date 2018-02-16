using MeetUp.DBEntityModels;
using System.Windows;

namespace MeetUp.EmployeeWindow
{
    public partial class EmployeeWindowView : Window
    {
        private EmployeeWindowVM employeeWindowVM;

        public Employee Employee
        {
            get
            {
                return employeeWindowVM.Employee;
            }
        }

        public EmployeeWindowView()
        {
            InitializeComponent();
            employeeWindowVM = new EmployeeWindowVM(this);
            DataContext = employeeWindowVM;
        }

        public EmployeeWindowView(Employee employee)
        {
            InitializeComponent();
            employeeWindowVM = new EmployeeWindowVM(this, employee);
            DataContext = employeeWindowVM;
        }        
    }
}
