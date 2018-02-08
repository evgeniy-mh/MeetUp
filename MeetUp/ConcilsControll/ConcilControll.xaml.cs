﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MeetUp.ConcilsControll
{
    /// <summary>
    /// Interaction logic for ConferenceControll.xaml
    /// </summary>
    public partial class ConcilControll : UserControl
    {
        public ConcilControll()
        {
            InitializeComponent();
            DataContext = new ConcilsControllVM();
        }
    }
}