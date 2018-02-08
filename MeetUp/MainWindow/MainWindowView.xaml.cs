using MeetUp.ConcilsControll;
using MeetUp.EmployeesControl;
using MeetUp.MainWindow;
using System.Collections.Generic;
using System.Windows;

namespace MeetUp.MainWindowView
{
    public partial class MainWindow : Window
    {
        List<MainMenuItem> menuItems = new List<MainMenuItem> {
            new MainMenuItem { Name = "Сотрудники", View = new EmployeeControl() },
            new MainMenuItem { Name = "Совет", View = new ConcilControll() }
            };

        MainMenuItem selectedMenuItem { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            MenuItemsListBox.ItemsSource = menuItems;
            MenuItemsListBox.SelectedItem = selectedMenuItem;
            MenuItemsListBox.SelectedIndex = 0;
        }

        public void MenuSelector_OnSelectionChanged(object sender, RoutedEventArgs e)
        { 
            MainView.Children.Clear();
            MainMenuItem item = MenuItemsListBox.SelectedItem as MainMenuItem;
            MainView.Children.Add(item.View);
        }
    }
}
