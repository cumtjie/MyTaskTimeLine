using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// scale.xaml 的交互逻辑 刻度控件 by cumtjie
    /// </summary>
    public partial class Scale : UserControl
    {
        public Scale()
        {
            InitializeComponent();
        }
        public static readonly DependencyProperty SecondValueProperty = DependencyProperty.Register(
            "SecondValue", typeof(string), typeof(Scale), new PropertyMetadata(default(string)));
        /// <summary>
        /// 刻度值
        /// </summary>
        public string SecondValue
        {
            get { return (string)GetValue(SecondValueProperty); }
            set { SetValue(SecondValueProperty, value); }
        }
        public static readonly DependencyProperty IsPopupOpenProperty = DependencyProperty.Register(
            "IsPopupOpen", typeof(bool), typeof(Scale), new PropertyMetadata(default(bool)));
        /// <summary>
        /// 刻度值
        /// </summary>
        public bool IsPopupOpen
        {
            get { return (bool)GetValue(IsPopupOpenProperty); }
            set { SetValue(IsPopupOpenProperty, value); }
        }

        public void Rectangle_OnLayoutUpdated(object sender, EventArgs e)
        {
            if (IsPopupOpen)
            {
                Random random = new Random();
                popup.PlacementRectangle = new Rect(new Point(random.NextDouble() / 1000, 0), new Size(75, 25));
            }
        }
    }
}
