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

namespace MeetUp.RecordWindow
{
    public partial class RecordWindowView : Window
    {
        private RecordWindowVM recordWindowVM;

        public Record Record
        {
            get
            {
                return recordWindowVM.Record;
            }
        }

        public RecordWindowView()
        {
            InitializeComponent();
            recordWindowVM = new RecordWindowVM(this);
            DataContext = recordWindowVM;
        }

        public RecordWindowView(Record record)
        {
            InitializeComponent();
            recordWindowVM = new RecordWindowVM(this, record);
            DataContext = recordWindowVM;
        }
    }
}
