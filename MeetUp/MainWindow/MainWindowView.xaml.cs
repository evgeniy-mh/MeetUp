using MeetUp.ConcilsControll;
using MeetUp.EmployeesControl;
using MeetUp.MainWindow;
using MeetUp.MeetingsControll;
using MeetUp.RecordsControll;
using System.Collections.Generic;
using System.Windows;

namespace MeetUp.MainWindowView
{
    public partial class MainWindow : Window
    {
        List<MainMenuItem> menuItems = new List<MainMenuItem> {
            new MainMenuItem { Name = "Сотрудники", View = new EmployeeControl() },
            new MainMenuItem { Name = "Советы", View = new ConcilControll() },
            new MainMenuItem { Name = "Заседания", View = new MeetingControll() },
            new MainMenuItem{ Name="Протоколы заседаний", View=new RecordControll() }
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
