using MeetUp.DBEntityModels;
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

namespace MeetUp.ConcilWindow
{
    public partial class ConcilWindowView : Window
    {
        private ConcilWindowVM concilWindowVM;

        public Concil Concil
        {
            get
            {
                return concilWindowVM.Concil;
            }
        }

        public ConcilWindowView()
        {
            InitializeComponent();
            concilWindowVM = new ConcilWindowVM(this);
            DataContext = concilWindowVM;
        }

        public ConcilWindowView(Concil concil)
        {
            InitializeComponent();
            concilWindowVM = new ConcilWindowVM(this, concil);
            DataContext = concilWindowVM;
        }
    }
}
