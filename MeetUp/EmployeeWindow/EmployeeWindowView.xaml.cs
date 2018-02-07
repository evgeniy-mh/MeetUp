using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MeetUp.EmployeeWindow
{
    /// <summary>
    /// Interaction logic for EmployeeWindow.xaml
    /// </summary>
    public partial class EmployeeWindowView : Window
    {
        public Employee Employee { get; set; }

        public EmployeeWindowView()
        {
            InitializeComponent();
            this.Employee = new Employee();
            this.DataContext = Employee;
        }

        public EmployeeWindowView(Employee employee)
        {
            InitializeComponent();
            this.Employee = new Employee(employee);
            this.DataContext = Employee;
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
