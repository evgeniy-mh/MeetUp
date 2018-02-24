using System.Windows.Controls;

namespace MeetUp.RecordsControll
{
    public partial class RecordControll : UserControl
    {
        public RecordControll()
        {
            InitializeComponent();
            DataContext = new RecordsControllVM(this);
        }
    }
}
