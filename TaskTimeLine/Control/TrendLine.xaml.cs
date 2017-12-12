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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TaskTimeLineLab.Control
{
    /// <summary>
    /// TrendLine.xaml 的交互逻辑 播放线控件 by cumtjie
    /// </summary>
    public partial class TrendLine : UserControl
    {
        public TrendLine()
        {
            InitializeComponent();
        }
        public static readonly DependencyProperty BaseWidthProperty = DependencyProperty.Register(
            "BaseWidth", typeof(double), typeof(TrendLine), new PropertyMetadata(20.0));

        public double BaseWidth
        {
            get { return (double)GetValue(BaseWidthProperty); }
            set { SetValue(BaseWidthProperty, value); }
        }

        
    }
}
