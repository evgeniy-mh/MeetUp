using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace MeetUp.MeetingsControll
{
    public partial class MeetingControll : UserControl
    {
        public MeetingControll()
        {
            InitializeComponent();
            DataContext = new MeetingControllVM();
        }
    }
}
