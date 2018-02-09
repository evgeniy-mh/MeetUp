using MeetUp.DBEntityModels;
using System.Collections.Generic;
using System.Windows;

namespace MeetUp.SelectEmployeeWindow
{
    public partial class SelectEmployeeWindowView : Window
    {
        private SelectEmployeeWindowVM selectEmployeeWindowVM;

        public SelectEmployeeWindowView()
        {
            InitializeComponent();
            selectEmployeeWindowVM = new SelectEmployeeWindowVM(this);
            DataContext = selectEmployeeWindowVM;
        }

        public SelectEmployeeWindowView(IEnumerable<Employee> employees)
        {
            InitializeComponent();
            selectEmployeeWindowVM = new SelectEmployeeWindowVM(this, employees);
            DataContext = selectEmployeeWindowVM;
        }
    }
}
