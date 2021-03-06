﻿using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace AlgoVisionGUI
{
    /// <summary>
    /// Interaction logic for ArrayControl.xaml
    /// </summary>
    public partial class ArrayControl : UserControl
    {
        public static readonly DependencyProperty ArrayProperty = DependencyProperty
            .Register("Array",
                      typeof(ColumnItem[]),
                      typeof(ArrayControl));

        public ArrayControl()
        {
            InitializeComponent();
            Root.DataContext = this;
        }

        public ColumnItem[] Array
        {
            get => (ColumnItem[])GetValue(ArrayProperty);
            set => SetValue(ArrayProperty, value);
        }
    }
}
