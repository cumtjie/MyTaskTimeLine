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
using TaskTimeLineLab.Common;

namespace TaskTimeLineLab.Control
{
    /// <summary>
    /// TaskBar.xaml 的交互逻辑 任务控件 by cumtjie 
    /// </summary>
    public partial class TaskBar : UserControl
    {
        public TaskBar()
        {
            InitializeComponent();
            this.Loaded += TaskBar_Loaded;
        }
        /// <summary>
        /// 根据用户设置初始化UI
        /// </summary>
        private void TaskBar_Loaded(object sender, RoutedEventArgs e)
        {
            StartTime = UserSetStartTime;
            EndTime = UserSetEndTime;
            BarWidth = (EndTime - StartTime) * BaseWidth;
            Canvas.SetLeft(task, StartTime * BaseWidth);
        }
        #region 依赖属性
        public static readonly DependencyProperty CanEditProperty = DependencyProperty.Register(
           "CanEdit", typeof(bool), typeof(TaskBar), new PropertyMetadata(true));
        /// <summary>
        /// 设定该控件是否为可编辑
        /// </summary>
        public bool CanEdit
        {
            get { return (bool)GetValue(CanEditProperty); }
            set { SetValue(CanEditProperty, value); }
        }

        public static readonly DependencyProperty IsPlayingProperty = DependencyProperty.Register(
            "IsPlaying", typeof(bool), typeof(TaskBar), new PropertyMetadata(false));
        /// <summary>
        /// 是否在播放状态
        /// </summary>
        public bool IsPlaying
        {
            get { return (bool)GetValue(IsPlayingProperty); }
            set { SetValue(IsPlayingProperty, value); }
        }
        public static readonly DependencyProperty SelecTaskItemProperty = DependencyProperty.Register(
            "SelectedTaskItem", typeof(TaskItem), typeof(TaskBar), new PropertyMetadata(default(TaskItem)));

        public TaskItem SelectedTaskItem
        {
            get { return (TaskItem)GetValue(SelecTaskItemProperty); }
            set { SetValue(SelecTaskItemProperty, value); }
        }

        ///**
        // * 根据用户设置初始化时间初始化UI（面向用户设计界面接口） by cumtjie
        // */

        public static readonly DependencyProperty UserSetStartTimeProperty = DependencyProperty.Register(
            "UserSetStartTime", typeof(double), typeof(TaskBar), new PropertyMetadata(0.0));
        /// <summary>
        /// 用户设置起始时间
        /// </summary>
        public double UserSetStartTime
        {
            get { return (double)GetValue(UserSetStartTimeProperty); }
            set { SetValue(UserSetStartTimeProperty, value); }
        }



        public static readonly DependencyProperty UserSetEndTimeProperty = DependencyProperty.Register(
           "UserSetEndTime", typeof(double), typeof(TaskBar), new PropertyMetadata(2.0));
        /// <summary>
        /// 用户设置结束时间
        /// </summary>
        public double UserSetEndTime
        {
            get { return (double)GetValue(UserSetEndTimeProperty); }
            set { SetValue(UserSetEndTimeProperty, value); }
        }

        /**
         *基本依赖属性（面向底层逻辑实现） by cumtjie
         * 
         */

        public static readonly DependencyProperty StartTimeProperty = DependencyProperty.Register(
            "StartTime", typeof (double), typeof (TaskBar), new PropertyMetadata(default(double),new PropertyChangedCallback(StartTimeChanged)));
        /// <summary>
        /// 起始时间
        /// </summary>
        public double StartTime
        {
            get { return (double) GetValue(StartTimeProperty); }
            set { SetValue(StartTimeProperty, value); }
        }

        /// <summary>
        /// UserSetStartTime绑定到用户数据，通过此函数反向更新用户数据 by cumtjie
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void StartTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as TaskBar).StartTimeChange(e);
        }

        private void StartTimeChange(DependencyPropertyChangedEventArgs e)
        {
            UserSetStartTime = double.Parse(e.NewValue.ToString());
        }


        public static readonly DependencyProperty EndTimeProperty = DependencyProperty.Register(
            "EndTime", typeof(double), typeof(TaskBar), new PropertyMetadata(default(double),new PropertyChangedCallback(EndTimeChanged)));
        /// <summary>
        /// 结束时间
        /// </summary>
        public double EndTime
        {
            get { return (double)GetValue(EndTimeProperty); }
            set { SetValue(EndTimeProperty, value); }
        }
        /// <summary>
        /// UserSetEndTime绑定到用户数据，通过此函数反向更新用户数据 by cumtjie
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void EndTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as TaskBar).EndTimeChange(e);
        }

        private void EndTimeChange(DependencyPropertyChangedEventArgs e)
        {
            UserSetEndTime = double.Parse(e.NewValue.ToString());
        }


        public static readonly DependencyProperty BarWidthProperty = DependencyProperty.Register(
            "BarWidth", typeof (double), typeof (TaskBar), new PropertyMetadata(40.0));
        /// <summary>
        /// TaskBar宽度（取决于起始时间、结束时间和单元基本宽度）
        /// </summary>
        public double BarWidth
        {
            get { return (double) GetValue(BarWidthProperty); }
            set { SetValue(BarWidthProperty, value); }
        }
       


        public static readonly DependencyProperty BaseWidthProperty = DependencyProperty.Register(
            "BaseWidth", typeof (double), typeof (TaskBar), new PropertyMetadata(20.0));

        /// <summary>
        /// 单元基本宽度
        /// </summary>
        public double BaseWidth
        {
            get { return (double) GetValue(BaseWidthProperty); }
            set { SetValue(BaseWidthProperty, value); }
        }

        public static readonly DependencyProperty TimeLineWidthProperty = DependencyProperty.Register(
            "TimeLineWidth", typeof (double), typeof (TaskBar), new PropertyMetadata(500.0));
        /// <summary>
        /// 时间轴总长度
        /// </summary>
        public double TimeLineWidth
        {
            get { return (double) GetValue(TimeLineWidthProperty); }
            set { SetValue(TimeLineWidthProperty, value); }
        }
        #endregion
        #region 路由事件

        public static readonly RoutedEvent TimeLineWidthChangedRoutedEvent =
            EventManager.RegisterRoutedEvent("TimeLineWidthChanged", RoutingStrategy.Bubble, typeof (RoutedEventHandler),
                typeof (TaskBar));
        public event RoutedEventHandler TimeLineWidthChanged
        {
            add { AddHandler(TimeLineWidthChangedRoutedEvent,value);}
            remove { RemoveHandler(TimeLineWidthChangedRoutedEvent,value);}
        }

        public static readonly RoutedEvent MoveThumebChangedRoutedEvent =
            EventManager.RegisterRoutedEvent("MoveThumebChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler),
                typeof(TaskBar));
        public event RoutedEventHandler MoveThumebChanged
        {
            add { AddHandler(MoveThumebChangedRoutedEvent, value); }
            remove { RemoveHandler(MoveThumebChangedRoutedEvent, value); }
        }
        #endregion
        #region 废弃
        private void TaskBar_OnLoaded(object sender, RoutedEventArgs e)
        {
            var layer = AdornerLayer.GetAdornerLayer(canvas);
            foreach (UIElement ui in canvas.Children)
                layer.Add(new MyCanvasAdorner(ui));
        }


        private bool isDragging = false;
        private double pressDownX;
        private double pressUpX;
        private void Task_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isDragging = true;
            Cursor = Cursors.Hand;
            pressDownX = e.GetPosition(this.canvas).X;
        }

        private void Task_OnMouseMove(object sender, MouseEventArgs e)
        {
            
            if (isDragging)
            {
                pressUpX = e.GetPosition(this.canvas).X;
                double distance = (pressUpX - pressDownX) + (double)task.GetValue(Canvas.LeftProperty);
                if (distance < 0)
                {
                    distance = 0.0;
                }
                else
                {
                    distance = (int)(distance / BaseWidth) * BaseWidth;
                }
                if (distance != 0)
                {
                    task.SetValue(Canvas.LeftProperty, distance);
                    pressDownX = e.GetPosition(canvas).X;
                }
                

            }
        }

        private void Task_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            Cursor = Cursors.Arrow;
        }
        #endregion

#region 用于通知主界面做相应更新
        private void OnTimeLineWidthChanged(object sender, RoutedEventArgs e)
        {
            RoutedEventArgs args = new RoutedEventArgs(TimeLineWidthChangedRoutedEvent, this);
            RaiseEvent(args);
        }

        private void MoveThumb_OnMoveThumebChanged(object sender, RoutedEventArgs e)
        {
            RoutedEventArgs args = new RoutedEventArgs(MoveThumebChangedRoutedEvent, this);
            RaiseEvent(args);
        }
#endregion
    }
}
