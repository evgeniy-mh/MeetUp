using MeetUp.View;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MeetUp
{
    public partial class MainWindow : Window
    {
        List<MainMenuItem> menuItems = new List<MainMenuItem> {
            new MainMenuItem { Name = "Сотрудники", View = new UserControl1() },
            new MainMenuItem { Name = "Совет", View = new ConferenceControll() }
            };

        MainMenuItem selectedMenuItem { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            MenuItemsListBox.ItemsSource = menuItems;
            MenuItemsListBox.SelectedItem = selectedMenuItem;
            MenuItemsListBox.SelectedIndex = 0;

            //DataContext = new EmployeesVM();
            //MainView.Children.Add(new UserControl1());
        }

        public void MenuSelector_OnSelectionChanged(object sender, RoutedEventArgs e)
        {
            MainView.Children.Clear();
            MainMenuItem item = MenuItemsListBox.SelectedItem as MainMenuItem;
            MainView.Children.Add(item.View);
        }
    }
}
