using System.Windows.Controls;

namespace MeetUp.EmployeesControl
{
    public partial class EmployeeControl : UserControl
    {
        public EmployeeControl()
        {
            InitializeComponent();
            DataContext = new EmployeesControllVM();
        }
    }
}
