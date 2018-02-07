using MeetUp.EmployeesControl;
using MeetUp.View;
using System.Collections.Generic;
using System.Windows;

namespace MeetUp
{
    public partial class MainWindow : Window
    {
        List<MainMenuItem> menuItems = new List<MainMenuItem> {
            new MainMenuItem { Name = "Сотрудники", View = new EmployeeControl() },
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
