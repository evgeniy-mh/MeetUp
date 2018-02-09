using MeetUp.DBEntityModels;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace MeetUp.EmployeesControl
{
    /// <summary>
    /// Interaction logic for EmployeeControl.xaml
    /// </summary>
    public partial class EmployeeControl : UserControl
    {
        public EmployeeControl()
        {
            InitializeComponent();
            DataContext = new EmployeesControllVM();
        }

        public EmployeeControl(ObservableCollection<Employee> employees)
        {
            InitializeComponent();
            DataContext = new EmployeesControllVM(employees);
        }
    }
}
