﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MeetUp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


            using (UserContext db = new UserContext())
            {
                /*db.Employees.Add(new Employee { FirstName = "Hello" });
                db.Employees.Add(new Employee { FirstName = "Hello2" });
                db.Employees.Add(new Employee { FirstName = "Hello3" });

                db.SaveChanges();*/

                List<Employee> empls = new List<Employee>();
                UsersListView.ItemsSource = db.Employees.ToList();
            }
        }

        private void AddEmployee_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Кнопка нажата");
        }

        private void DeleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Кнопка нажата");
        }
    }
}
