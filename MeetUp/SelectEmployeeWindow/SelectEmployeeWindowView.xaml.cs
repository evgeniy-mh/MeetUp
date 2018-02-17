using MeetUp.DBEntityModels;
using System.Collections.Generic;
using System.Windows;

namespace MeetUp.SelectEmployeeWindow
{
    public partial class SelectEmployeeWindowView : Window
    {
        private SelectEmployeeWindowVM selectEmployeeWindowVM;

        public Employee SelectedEmployee
        {
            get
            {
                return selectEmployeeWindowVM.SelectedEmployee;
            }
        }

        public SelectEmployeeWindowView()
        {
            InitializeComponent();
            selectEmployeeWindowVM = new SelectEmployeeWindowVM(this);
            DataContext = selectEmployeeWindowVM;
            Closing += selectEmployeeWindowVM.OnWindowClosing;
        }

        public SelectEmployeeWindowView(IEnumerable<Employee> employees)
        {
            InitializeComponent();
            selectEmployeeWindowVM = new SelectEmployeeWindowVM(this, employees);
            DataContext = selectEmployeeWindowVM;
            Closing += selectEmployeeWindowVM.OnWindowClosing;
        }
    }
}
