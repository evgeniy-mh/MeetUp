using System.Windows.Controls;

namespace MeetUp.ConcilsControll
{
    public partial class ConcilControll : UserControl
    {
        public ConcilControll()
        {
            InitializeComponent();
            DataContext = new ConcilsControllVM(this);
        }
    }
}
