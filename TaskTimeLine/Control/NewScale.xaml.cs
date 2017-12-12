using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TaskTimeLineLab.Control
{
    /// <summary>
    /// NewScale.xaml 的交互逻辑
    /// </summary>
    public partial class NewScale : UserControl
    {
        public NewScale()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ScaleWidthProperty = DependencyProperty.Register(
           "ScaleWidth", typeof(double), typeof(NewScale), new PropertyMetadata(30.0));
        /// <summary>
        /// 刻度值
        /// </summary>
        public double ScaleWidth
        {
            get { return (double)GetValue(ScaleWidthProperty); }
            set { SetValue(ScaleWidthProperty, value); }
        }

        public static readonly DependencyProperty ScaleValueProperty = DependencyProperty.Register(
           "ScaleValue", typeof(string), typeof(NewScale), new PropertyMetadata(default(string)));
        /// <summary>
        /// 刻度值
        /// </summary>
        public string ScaleValue
        {
            get { return (string)GetValue(ScaleValueProperty); }
            set { SetValue(ScaleValueProperty, value); }
        }
    }
}
