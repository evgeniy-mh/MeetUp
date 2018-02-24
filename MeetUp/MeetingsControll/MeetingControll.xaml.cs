using System.Windows.Controls;

namespace MeetUp.MeetingsControll
{
    public partial class MeetingControll : UserControl
    {
        public MeetingControll()
        {
            InitializeComponent();
            DataContext = new MeetingControllVM(this);
        }
    }
}
