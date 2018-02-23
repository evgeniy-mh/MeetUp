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

        private bool isEditableRecord;
        public bool IsEditableRecord
        {
            get
            {
                return isEditableRecord;
            }
            set
            {
                isEditableRecord = value;
                if (isEditableRecord)
                {
                    SelectRecordFileButton.Visibility = Visibility.Visible;
                    RecordNameTextBox.IsReadOnly = false;
                }
                else
                {
                    SelectRecordFileButton.Visibility = Visibility.Collapsed;
                    RecordNameTextBox.IsReadOnly = true;
                }
            }
        }

        public Record Record
        {
            get
            {
                return recordWindowVM.Record;
            }
        }

        public RichTextBox recordContentRichTextBox
        {
            get
            {
                return RecordContentRichTextBox;
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
