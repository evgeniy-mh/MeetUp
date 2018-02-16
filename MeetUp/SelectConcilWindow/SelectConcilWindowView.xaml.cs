using MeetUp.DBEntityModels;
using System.Windows;

namespace MeetUp.SelectConcilWindow
{
    public partial class SelectConcilWindowView : Window
    {
        private SelectConcilWindowVM selectConcilWindowVM;

        public Concil SelectedConcil
        {
            get
            {
                return selectConcilWindowVM.SelectedConcil;
            }
        }

        public SelectConcilWindowView()
        {
            InitializeComponent();
            selectConcilWindowVM = new SelectConcilWindowVM(this);
            DataContext = selectConcilWindowVM;
        }
    }
}
