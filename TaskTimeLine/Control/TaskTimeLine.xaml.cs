using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Threading;

namespace TaskTimeLineLab.Control
{
    //public delegate void RefreshScaleBarPopup(bool isVisible);
    /// <summary>
    /// TaskTimeLine.xaml 的交互逻辑  by cumtjie 2017/12/5 
    /// </summary>
    public partial class TaskTimeLine : UserControl
    {
        /// <summary>
        /// 在窗口位置或窗口可见属性发生变化时，调用此接口刷新刻度条
        /// </summary>
        //public RefreshScaleBarPopup RefreshScaleBarPopupHandel = null;
        public TaskTimeLine()
        {
            InitializeComponent();
            this.Loaded += TaskTimeLine_Loaded;
            this.MouseWheel += TaskTimeLine_MouseWheel;
            //this.KeyDown += TaskTimeLine_KeyDown;
            //this.KeyUp += TaskTimeLine_KeyUp;
           // RefreshScaleBarPopupHandel += UpdateScaleBar;
        }

        private bool isZoom = false;

        private void TaskTimeLine_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyboardDevice.Modifiers == ModifierKeys.Control)
            {
                isZoom = false;
            }
        }

        private void TaskTimeLine_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyboardDevice.Modifiers == ModifierKeys.Control)
            {
                isZoom = true;
            }
        }

        private void TaskTimeLine_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            bool handle = (Keyboard.Modifiers & ModifierKeys.Control) > 0;
            if (!handle)
                return;

            if (e.Delta > 0)
            {
                Shrink_OnClick(null, null);
            }
            else if (e.Delta < 0)
            {
                Enlarge_OnClick(null, null);
            }
            //if (isZoom)
            //{
            //    if (e.Delta > 0)
            //    {
            //        Shrink_OnClick(null, null);
            //    }
            //    else if(e.Delta < 0)
            //    {
            //        Enlarge_OnClick(null, null);
            //    }
            //}
        }

        private void TaskTimeLine_Loaded(object sender, RoutedEventArgs e)
        {
            ScaleBaseWidth = BaseWidth;
            ReFreshScaleBar();
            InitTimer();
        }

        private double ScaleBaseWidth = 30;//刻度线宽度初始后固定


        /// <summary>
        /// 构造刻度线 
        /// </summary>
        public void ReFreshScaleBar()
        {
            ticksPanel.Children.Clear();
            int scaleCount = (int)(TimeLineWidth/ScaleBaseWidth);
            for (int i = 0; i < scaleCount; i++)
            {
                int yu = i%(60/ScaleDuration);
                int zheng = i/(60/ScaleDuration);

                NewScale newScale = new NewScale();
                newScale.ScaleWidth = ScaleBaseWidth;
                if (yu == 0 && zheng > 0)
                {
                    newScale.ScaleValue = zheng + "分";
                }
                else
                {
                    newScale.ScaleValue = yu*ScaleDuration + "秒";
                }
                ticksPanel.Children.Add(newScale);   
            }
        }
        private ObservableCollection<TaskItem> TempTaskItemSource = new ObservableCollection<TaskItem>();
        /// <summary>
        /// 放大刻度值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Enlarge_OnClick(object sender, RoutedEventArgs e)
        {
            if (ScaleDuration == 8)
            {
                return;
            }
            ScaleDuration = ScaleDuration*2;
            ZoomScale(ScaleDuration);
        }
        /// <summary>
        /// 缩小刻度值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Shrink_OnClick(object sender, RoutedEventArgs e)
        {
            if (ScaleDuration == 1)
            {
                return;
            }
            ScaleDuration = ScaleDuration/2;
            ZoomScale(ScaleDuration);
        }
        /// <summary>
        /// 缩放
        /// </summary>
        /// <param name="scaleDuration"></param>
        private void ZoomScale(int scaleDuration)
        {
            if (TempTaskItemSource == null)
            {
                TempTaskItemSource = new ObservableCollection<TaskItem>();
            }
            if (TaskItemSource == null)
            {
                TaskItemSource = new ObservableCollection<TaskItem>();
            }
            TempTaskItemSource.Clear();
            foreach (TaskItem item in TaskItemSource)
            {
                TempTaskItemSource.Add(item);
            }
            //TempTaskItemSource = Common.Common.Clone<ObservableCollection<TaskItem>>(TaskItemSource);
            BaseWidth = ScaleBaseWidth/scaleDuration;
            TaskItemSource.Clear();
            foreach (TaskItem item in TempTaskItemSource)
            {
                TaskItemSource.Add(item);
            }
            //TaskItemSource = Common.Common.Clone<ObservableCollection<TaskItem>>(TempTaskItemSource);
            ReFreshScaleBar();
            SetTimePara();
        }
#region 刷新刻度相关  
        /// <summary>
        /// 刷新刻度条
        /// </summary>
        /// <param name="isVisible">刻度窗口是否还处于可见状态</param>
        private void UpdateScaleBar(bool isVisible)
        {
            int i = 0;
            foreach (var child in ticksPanel.Children)
            {
                Scale scale = child as Scale;
                if (scale != null)
                {
                    if (!isVisible)
                    {
                        scale.IsPopupOpen = false;
                        continue;
                    }
                    double currentPosition = BaseWidth*i - TimeLineScrollViewer.HorizontalOffset;
                    if (currentPosition >= -1 && currentPosition <= ticksGrid.ActualWidth - 10 )
                    {
                        scale.IsPopupOpen = true;
                    }
                    else
                    {
                        scale.IsPopupOpen = false;
                    }
                    i++;
                    scale.Rectangle_OnLayoutUpdated(null, null);
                }
            }
        }
        /// <summary>
        /// 滚动条滚动刷新ScaleBar,垂直滚动联动TaskName_ListBox_ScrollViewer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimeLineScrollViewer_OnScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.VerticalChange != 0)
            {
                TaskName_ListBox_ScrollViewer.ScrollToVerticalOffset(TimeLineScrollViewer.VerticalOffset);
            }
            if (e.HorizontalChange != 0)
            {
                //UpdateScaleBar(true);
                //Thickness thickness = TrendLine.Margin;
                //CurrentLocation = TimeLineScrollViewer.HorizontalOffset + thickness.Left;
                //currentTime = Math.Round(CurrentLocation / BaseWidth);
                //if (Math.Abs(CurrentLocation - currentTime * BaseWidth) < distanceCompareNum)
                //{
                //    RefreshTimer();
                //}
            }
        }

        #endregion
        #region 依赖属性 
        public static readonly DependencyProperty ScaleDurationProperty = DependencyProperty.Register(
            "ScaleDuration", typeof(int), typeof(TaskTimeLine), new PropertyMetadata(1));
        /// <summary>
        /// 刻度值放大倍数
        /// </summary>
        public int ScaleDuration
        {
            get { return (int)GetValue(ScaleDurationProperty); }
            set { SetValue(ScaleDurationProperty, value); }
        }

        public static readonly DependencyProperty CanEditProperty = DependencyProperty.Register(
            "CanEdit", typeof(bool), typeof(TaskTimeLine), new PropertyMetadata(true));
        /// <summary>
        /// 设定该控件是否为可编辑
        /// </summary>
        public bool CanEdit
        {
            get { return (bool)GetValue(CanEditProperty); }
            set { SetValue(CanEditProperty, value); }
        }

        public static readonly DependencyProperty IsPlayingProperty = DependencyProperty.Register(
            "IsPlaying", typeof(bool), typeof(TaskTimeLine), new PropertyMetadata(false));
        /// <summary>
        /// 是否在播放状态（私有属性，内部控制使用）
        /// </summary>
        private bool IsPlaying
        {
            get { return (bool)GetValue(IsPlayingProperty); }
            set { SetValue(IsPlayingProperty, value); }
        }

        public static readonly DependencyProperty NowMinuteProperty = DependencyProperty.Register(
            "NowMinute", typeof(string), typeof(TaskTimeLine), new PropertyMetadata("00"));
        /// <summary>
        /// 当前播放分钟
        /// </summary>
        public string NowMinute
        {
            get { return (string)GetValue(NowMinuteProperty); }
            set { SetValue(NowMinuteProperty, value); }
        }
        public static readonly DependencyProperty NowSecondProperty = DependencyProperty.Register(
            "NowSecond", typeof(string), typeof(TaskTimeLine), new PropertyMetadata("00"));
        /// <summary>
        /// 当前播放秒
        /// </summary>
        public string NowSecond
        {
            get { return (string)GetValue(NowSecondProperty); }
            set { SetValue(NowSecondProperty, value); }
        }

        public static readonly DependencyProperty SelectedTaskItemIndexProperty = DependencyProperty.Register(
            "SelectedTaskItemIndex", typeof(int), typeof(TaskTimeLine), new PropertyMetadata(default(int)));

        public int SelectedTaskItemIndex
        {
            get { return (int)GetValue(SelectedTaskItemIndexProperty); }
            set { SetValue(SelectedTaskItemIndexProperty, value); }
        }

        public static readonly DependencyProperty SelecTaskItemProperty = DependencyProperty.Register(
            "SelectedTaskItem", typeof(TaskItem), typeof(TaskTimeLine), new PropertyMetadata(default(TaskItem)));

        public TaskItem SelectedTaskItem
        {
            get { return (TaskItem)GetValue(SelecTaskItemProperty); }
            set { SetValue(SelecTaskItemProperty, value); }
        }

        public static readonly DependencyProperty TaskItemSourceProperty = DependencyProperty.Register(
            "TaskItemSource", typeof (ObservableCollection<TaskItem>), typeof (TaskTimeLine), new PropertyMetadata(new ObservableCollection<TaskItem>()));

        public ObservableCollection<TaskItem> TaskItemSource
        {
            get { return (ObservableCollection<TaskItem>) GetValue(TaskItemSourceProperty); }
            set { SetValue(TaskItemSourceProperty, value); }
        }

        public static readonly DependencyProperty TimeLineWidthProperty = DependencyProperty.Register(
            "TimeLineWidth", typeof (double), typeof (TaskTimeLine), new PropertyMetadata(500.0,new PropertyChangedCallback(TimeLineWidthChanged)));
        public static void TimeLineWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as TaskTimeLine).ReFreshScaleBar();
        }
        public double TimeLineWidth
        {
            get { return (double) GetValue(TimeLineWidthProperty); }
            set { SetValue(TimeLineWidthProperty, value); }
        }

        public static readonly DependencyProperty BaseWidthProperty = DependencyProperty.Register(
            "BaseWidth", typeof (double), typeof (TaskTimeLine), new PropertyMetadata(20.0));

        public double BaseWidth
        {
            get { return (double) GetValue(BaseWidthProperty); }
            set { SetValue(BaseWidthProperty, value); }
        }

        //public static readonly DependencyProperty StartTimeProperty = DependencyProperty.Register(
        //    "StartTime", typeof(double), typeof(TaskTimeLine), new PropertyMetadata(default(double)));

        //public double StartTime
        //{
        //    get { return (double)GetValue(StartTimeProperty); }
        //    set { SetValue(StartTimeProperty, value); }
        //}
        //public static readonly DependencyProperty EndTimeProperty = DependencyProperty.Register(
        //    "EndTime", typeof(double), typeof(TaskTimeLine), new PropertyMetadata(default(double)));

        //public double EndTime
        //{
        //    get { return (double)GetValue(EndTimeProperty); }
        //    set { SetValue(EndTimeProperty, value); }
        //}
        #endregion
        #region 路由事件  

        public static readonly RoutedEvent TaskItemSelectChangedEvent = EventManager.RegisterRoutedEvent("TaskItemSelectChanged",
    RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(TaskTimeLine));
        /// <summary>
        /// 选择改变事件
        /// </summary>
        public event RoutedEventHandler TaskItemSelectChanged
        {
            add { AddHandler(TaskItemSelectChangedEvent, value); }
            remove { RemoveHandler(TaskItemSelectChangedEvent, value); }
        }

        public static readonly RoutedEvent WindowExpanderEvent = EventManager.RegisterRoutedEvent("WindowExpander",
   RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(TaskTimeLine));
        /// <summary>
        /// 展开
        /// </summary>
        public event RoutedEventHandler WindowExpander
        {
            add { AddHandler(WindowExpanderEvent, value); }
            remove { RemoveHandler(WindowExpanderEvent, value); }
        }

        public static readonly RoutedEvent WindowCollapsedEvent = EventManager.RegisterRoutedEvent("WindowCollapsed",
  RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(TaskTimeLine));
        /// <summary>
        /// 收起
        /// </summary>
        public event RoutedEventHandler WindowCollapsed
        {
            add { AddHandler(WindowCollapsedEvent, value); }
            remove { RemoveHandler(WindowCollapsedEvent, value); }
        }

        public static readonly RoutedEvent WindowMaxEvent = EventManager.RegisterRoutedEvent("WindowMax",
    RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(TaskTimeLine));
        /// <summary>
        /// 最大化事件
        /// </summary>
        public event RoutedEventHandler WindowMax
        {
            add { AddHandler(WindowMaxEvent, value); }
            remove { RemoveHandler(WindowMaxEvent, value); }
        }
        public static readonly RoutedEvent WindowMinEvent = EventManager.RegisterRoutedEvent("WindowMin",
        RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(TaskTimeLine));
        /// <summary>
        /// 最小化事件
        /// </summary>
        public event RoutedEventHandler WindowMin
        {
            add { AddHandler(WindowMinEvent, value); }
            remove { RemoveHandler(WindowMinEvent, value); }
        }

        public static readonly RoutedEvent WindowCloseEvent = EventManager.RegisterRoutedEvent("WindowClose",
        RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(TaskTimeLine));
        /// <summary>
        /// 关闭事件
        /// </summary>
        public event RoutedEventHandler WindowClose
        {
            add { AddHandler(WindowCloseEvent, value); }
            remove { RemoveHandler(WindowCloseEvent, value); }
        }

        public static readonly RoutedEvent TaskBarStartEvent = EventManager.RegisterRoutedEvent("TaskBarStart",
            RoutingStrategy.Bubble, typeof (RoutedEventHandler), typeof (TaskTimeLine));
        /// <summary>
        /// 任务开始事件（到达每个任务开始时间触发该事件）
        /// </summary>
        public event RoutedEventHandler TaskBarStart
        {
            add { AddHandler(TaskBarStartEvent,value);}
            remove { RemoveHandler(TaskBarStartEvent,value);}
        }

        public static readonly RoutedEvent TaskBarEndEvent = EventManager.RegisterRoutedEvent("TaskBarEnd",
            RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(TaskTimeLine));
        /// <summary>
        /// 任务结束事件（到达每个任务结束时间触发该事件）
        /// </summary>
        public event RoutedEventHandler TaskBarEnd
        {
            add { AddHandler(TaskBarEndEvent, value); }
            remove { RemoveHandler(TaskBarEndEvent, value); }
        }
        #endregion
        #region 双击定位
        private void EventSetter_OnHandler(object sender, MouseButtonEventArgs e)
        {
            if (IsPlaying)
            {
                return;
            }
            if (SelectedTaskItem != null)
            {
                TimeLineScrollViewer.ScrollToHorizontalOffset(SelectedTaskItem.StartTime * BaseWidth);
                ScaleBarScrollViewer.ScrollToHorizontalOffset(SelectedTaskItem.StartTime * BaseWidth);
            }
        }
        #endregion
        #region 界面联动相关函数  
                #region 子控件触发
        /// <summary>
        /// 更新最大时间线长度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TaskBar_OnTimeLineWidthChanged(object sender, RoutedEventArgs e)
        {
            TaskBar taskBar = sender as TaskBar;
            if(TimeLineWidth < taskBar.TimeLineWidth)
                TimeLineWidth = taskBar.TimeLineWidth;

        }

        /// <summary>
        /// 拖拽TaskBar与滚动条联动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TaskBar_OnMoveThumebChanged(object sender, RoutedEventArgs e)
        {
            TaskBar taskBar = e.OriginalSource as TaskBar;
            double offSet = taskBar.EndTime * taskBar.BaseWidth ;
            double leftOffSet = taskBar.StartTime * taskBar.BaseWidth;
            double currentOffset = TimeLineScrollViewer.HorizontalOffset + ticksGrid.ActualWidth ;
            if (offSet >= TimeLineScrollViewer.HorizontalOffset + ticksGrid.ActualWidth)
            {
                TimeLineScrollViewer.ScrollToHorizontalOffset(TimeLineScrollViewer.HorizontalOffset + taskBar.BaseWidth);
                ScaleBarScrollViewer.ScrollToHorizontalOffset(TimeLineScrollViewer.HorizontalOffset + taskBar.BaseWidth);
            }
            else if(leftOffSet <= TimeLineScrollViewer.HorizontalOffset)
            {
                TimeLineScrollViewer.ScrollToHorizontalOffset(TimeLineScrollViewer.HorizontalOffset - taskBar.BaseWidth);
                ScaleBarScrollViewer.ScrollToHorizontalOffset(TimeLineScrollViewer.HorizontalOffset - taskBar.BaseWidth);
            }
        }
                #endregion

        /// <summary>
        /// 刻度滚轮滚动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TicksPanel_OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (IsPlaying)
            {
                return;
            }
            if (TaskItemSource == null || TaskItemSource.Count == 0)//防止ScaleBarScrollViewer出现滚动显示BUG
            {
                return;
            }
            bool handle = (Keyboard.Modifiers & ModifierKeys.Control) > 0;
            if (handle)
            {
                if (e.Delta > 0)
                {
                    Shrink_OnClick(null, null);
                }
                else if (e.Delta < 0)
                {
                    Enlarge_OnClick(null, null);
                }
            }
            else
            {
                if (e.Delta < 0)
                {
                    TimeLineScrollViewer.ScrollToHorizontalOffset(TimeLineScrollViewer.HorizontalOffset + ScaleBaseWidth);
                    ScaleBarScrollViewer.ScrollToHorizontalOffset(TimeLineScrollViewer.HorizontalOffset + ScaleBaseWidth);
                }
                else
                {
                    TimeLineScrollViewer.ScrollToHorizontalOffset(TimeLineScrollViewer.HorizontalOffset - ScaleBaseWidth);
                    ScaleBarScrollViewer.ScrollToHorizontalOffset(TimeLineScrollViewer.HorizontalOffset - ScaleBaseWidth);
                }
            }
        }
        #endregion
        #region 刻度拖拽  
        private bool isDragging = false;
        private double pressDownX;
        private double pressUpX;
        private void TicksPanel_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (IsPlaying)
            {
                return;
            }
            isDragging = true;
            Cursor = Cursors.Hand;
            pressDownX = e.GetPosition(this.ticksGrid).X;
        }

        private void TicksPanel_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (IsPlaying)
            {
                return;
            }
            if (TaskItemSource == null || TaskItemSource.Count == 0)//防止ScaleBarScrollViewer出现滚动显示BUG
            {
                return;
            }
            if (isDragging)
            {
                pressUpX = e.GetPosition(this.ticksGrid).X;
                double offSet = pressUpX - pressDownX;
                if (Math.Abs(offSet) >= BaseWidth)//一格一格滚动
                {
                    if (offSet < 0)
                    {
                        TimeLineScrollViewer.ScrollToHorizontalOffset(TimeLineScrollViewer.HorizontalOffset - Math.Floor(offSet / BaseWidth) * BaseWidth);
                        ScaleBarScrollViewer.ScrollToHorizontalOffset(TimeLineScrollViewer.HorizontalOffset - Math.Floor(offSet / BaseWidth) * BaseWidth);
                    }
                    else
                    {
                        TimeLineScrollViewer.ScrollToHorizontalOffset(TimeLineScrollViewer.HorizontalOffset - Math.Floor(offSet / BaseWidth) * BaseWidth);
                        ScaleBarScrollViewer.ScrollToHorizontalOffset(TimeLineScrollViewer.HorizontalOffset - Math.Floor(offSet / BaseWidth) * BaseWidth);
                    }
                    pressDownX = e.GetPosition(this.ticksGrid).X;
                }
                /* 平滑移动
                double offSet = TimeLineScrollViewer.HorizontalOffset - (pressUpX - pressDownX);
                if (offSet < 0)
                {
                    offSet = 0;
                }
                else if (offSet > TimeLineWidth)
                {
                    TimeLineWidth = offSet;
                }
                TimeLineScrollViewer.ScrollToHorizontalOffset(offSet);
                pressDownX = e.GetPosition(this.ticksGrid).X;
                */
            }
        }

        private void TicksPanel_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (IsPlaying)
            {
                return;
            }
            isDragging = false;
            Cursor = Cursors.Arrow;
        }

        private void TicksPanel_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (IsPlaying)
            {
                return;
            }
            isDragging = false;
            Cursor = Cursors.Arrow;
        }
        #endregion
    #region 播放线拖拽  
        private bool isTrendDragging = false;
        private double pressTrendDownX;
        private double pressTrendUpX;
        private void Trend_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            isDragging = true;
            Cursor = Cursors.Hand;
            pressTrendDownX = e.GetPosition(this.ticksGrid).X;
        }

        private void Trend_OnMouseMove(object sender, MouseEventArgs e)
        {

            if (isDragging)
            {
                pressTrendUpX = e.GetPosition(this.ticksGrid).X;
                double offSet = pressTrendUpX - pressTrendDownX;
                if (Math.Abs(offSet) >= BaseWidth)
                {
                    if (offSet < 0 && e.GetPosition(this.ticksGrid).X >= BaseWidth)
                    {
                        Thickness thickness = TrendLine.Margin;
                        var v = thickness.Left - BaseWidth;
                        TrendLine.Margin = new Thickness(v,0,0,0);
                    }
                    else if(e.GetPosition(this.ticksGrid).X + BaseWidth < this.ticksGrid.ActualWidth)
                    {
                        Thickness thickness = TrendLine.Margin;
                        var v = thickness.Left + BaseWidth;
                        TrendLine.Margin = new Thickness(v, 0, 0, 0);
                    }
                    pressTrendDownX = e.GetPosition(this.ticksGrid).X;
                }
                
            }
        }

        private void Trend_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            Cursor = Cursors.Arrow;
        }

        private void Trend_OnMouseLeave(object sender, MouseEventArgs e)
        {
            //isDragging = false;
            //Cursor = Cursors.Arrow;
        }
        #endregion
#region 播放控制  
        DispatcherTimer timer = new DispatcherTimer();

        private int BaseSpeedTime = 100;//基准速度
        private int CurrentSpeedTime = 100;//当前速度
        private double TrendLineMoveWidth;//播放线移动宽度(最多移动距离，超过后移动Scroll)
        private int MaxTime;//所有轴中最后结束时间
        private double ExistLastTime;//当前存在的所有任务中最后结束时间
        private double CurrentLocation;//当前播放位置(距离)
        private double MoveUnit;//每帧移动距离
        private double distanceCompareNum;//距离比较判断精度
        private double timeCompareNum;//时间比较判断精度
        private double currentTime;//通过距离算出的当前时间

        /// <summary>
        /// 初始化定时器
        /// </summary>
        private void InitTimer()
        {
            timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            timer.Tick += StartPlay;
            SetTimePara();
        }
        /// <summary>
        /// 设置播放相关参数
        /// </summary>
        private void SetTimePara()
        {
            timeCompareNum = 0.05;
            distanceCompareNum = BaseWidth/10;
            MoveUnit = BaseWidth/10;
        }
        /// <summary>
        /// 播放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Play_Stop_OnChecked(object sender, RoutedEventArgs e)
        {
            if (TaskItemSource == null || TaskItemSource.Count == 0)
            {
                ExistLastTime = 0;
                Play_Stop.IsChecked = false;
                return;
            }
            ExistLastTime = 0;
            foreach (TaskItem item in TaskItemSource)
            {
                if (item.EndTime > ExistLastTime)
                {
                    ExistLastTime = item.EndTime;
                }
            }
            TrendLineMoveWidth = (Math.Floor(ticksGrid.ActualWidth / BaseWidth)-1) * BaseWidth;

            if (CurrentLocation >= ExistLastTime*BaseWidth)
            {
                ReSet();
            }
            IsPlaying = true;
            timer.Start();
        }
        /// <summary>
        /// 暂停
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Play_Stop_OnUnchecked(object sender, RoutedEventArgs e)
        {
            IsPlaying = false;
            timer.Stop();
        }
        /// <summary>
        /// 播放处理程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartPlay(object sender, EventArgs e)
        {
            Thickness thickness = TrendLine.Margin;
            double currentLeft = thickness.Left;
            if (TimeLineScrollViewer.HorizontalOffset + thickness.Left > ExistLastTime * BaseWidth)
            {
                Play_Stop.IsChecked = false;
                CurrentSpeedTime = BaseSpeedTime;//回到常规速度
                return;
            }

            if (TimeLineScrollViewer.HorizontalOffset + currentLeft < 0.001)//0秒单独处理
            {
                RespondEvent(0);
                RefreshTimer();
            }

            if (thickness.Left >= TrendLineMoveWidth)
            {
                TimeLineScrollViewer.ScrollToHorizontalOffset(TimeLineScrollViewer.HorizontalOffset + MoveUnit);
                ScaleBarScrollViewer.ScrollToHorizontalOffset(TimeLineScrollViewer.HorizontalOffset + MoveUnit);
            }
            else
            {
                currentLeft = thickness.Left + MoveUnit;
                TrendLine.Margin = new Thickness(currentLeft, -10, 0, 0);
            }
            
            CurrentLocation = TimeLineScrollViewer.HorizontalOffset + currentLeft;
            currentTime = Math.Round(CurrentLocation/BaseWidth);

            if (Math.Abs(CurrentLocation - currentTime*BaseWidth) < distanceCompareNum)
            {
                //Task.Factory.StartNew(() =>
                //{
                    RespondEvent(currentTime);
                RefreshTimer();
                //});
            }
            
        }
        /// <summary>
        /// 刷新计时器
        /// </summary>
        private void RefreshTimer()
        {
            NowMinute = string.Format("{0:00}", Math.Floor(currentTime / 60) );
            NowSecond = string.Format("{0:00}", Math.Floor(currentTime % 60) );
        }

        private double tempTime = -1.0;
        /// <summary>
        /// 每帧检测所有任务，并触发相关事件（任务开始、任务结束）
        /// </summary>
        /// <param name="time"></param>
        private void RespondEvent(double time)
        {
            if (tempTime == time)
            {
                return;
            }
            tempTime = time;
            foreach (TaskItem taskItem in TaskItemSource)
            {
                if (Math.Abs(taskItem.StartTime - time) < timeCompareNum)
                {
                    RoutedEventArgs args = new RoutedEventArgs(TaskBarStartEvent,taskItem);
                    RaiseEvent(args);
                }
                if (Math.Abs(taskItem.EndTime - time) < timeCompareNum)
                {
                    RoutedEventArgs args = new RoutedEventArgs(TaskBarEndEvent, taskItem);
                    RaiseEvent(args);
                }
            }
        }

        /// <summary>
        /// 加速
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Accelerate_OnClick(object sender, RoutedEventArgs e)
        {
            CurrentSpeedTime = CurrentSpeedTime / 2;
            timer.Interval = new TimeSpan(0,0,0,0, CurrentSpeedTime);
        }
        /// <summary>
        /// 减速
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Decelerate_OnClick(object sender, RoutedEventArgs e)
        {
            CurrentSpeedTime = CurrentSpeedTime * 2;
            timer.Interval = new TimeSpan(0, 0, 0, 0, CurrentSpeedTime);
        }
        /// <summary>
        /// 停止（复原）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Stop_OnClick(object sender, RoutedEventArgs e)
        {
            Play_Stop.IsChecked = false;
            ReSet();
        }

        /// <summary>
        /// 重置播放器
        /// </summary>
        private void ReSet()
        {
            TrendLine.Margin = new Thickness(0, -10, 0, 0);
            TimeLineScrollViewer.ScrollToHorizontalOffset(0);
            ScaleBarScrollViewer.ScrollToHorizontalOffset(0);
            CurrentSpeedTime = BaseSpeedTime;
            timer.Interval = new TimeSpan(0, 0, 0, 0, CurrentSpeedTime);
            CurrentLocation = 0;
            currentTime = 0;
            tempTime = -1.0;
            RefreshTimer();
        }
        #endregion
#region 同步ListBox  
        private void TaskName_ListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Task_ListBox.SelectedIndex = TaskName_ListBox.SelectedIndex;
            RoutedEventArgs args = new RoutedEventArgs(TaskItemSelectChangedEvent, this);
            RaiseEvent(args);
        }

        private void Task_ListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TaskName_ListBox.SelectedIndex = Task_ListBox.SelectedIndex;
            RoutedEventArgs args = new RoutedEventArgs(TaskItemSelectChangedEvent, this);
            RaiseEvent(args);
        }
        #endregion
#region 任务管理  
        /// <summary>
        /// 新增任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_OnClick(object sender, RoutedEventArgs e)
        {
            if (TaskItemSource == null)
            {
                TaskItemSource = new ObservableCollection<TaskItem>();
            }
            TaskItemSource.Add(new TaskItem() {StartTime = 3,EndTime = 6,TaskName = "新增任务"});
        }
        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Del_OnClick(object sender, RoutedEventArgs e)
        {
            TaskItem delItem = Task_ListBox.SelectedItem as TaskItem;
            if (delItem != null)
            {
                TaskItemSource.Remove(delItem);
            }
        }
        #endregion
#region 窗口变化事件
        private void Close_OnClick(object sender, RoutedEventArgs e)
        {
            RoutedEventArgs args = new RoutedEventArgs(WindowCloseEvent, this);
            RaiseEvent(args);
        }

        private void MaxAndMin_OnChecked(object sender, RoutedEventArgs e)
        {
            PlayStackPanel.Visibility = Visibility.Collapsed;
            CollapsedAndExpander.Visibility = Visibility.Collapsed;
            Close.Margin = new Thickness(5,0,25,0);
            RoutedEventArgs args = new RoutedEventArgs(WindowMinEvent, this);
            RaiseEvent(args);
        }

        private void MaxAndMin_OnUnchecked(object sender, RoutedEventArgs e)
        {
            PlayStackPanel.Visibility = Visibility.Visible;
            CollapsedAndExpander.Visibility = Visibility.Visible;
            Close.Margin = new Thickness(1, 0, 5, 0);
            RoutedEventArgs args = new RoutedEventArgs(WindowMaxEvent, this);
            RaiseEvent(args);
        }

        /// <summary>
        /// 折叠
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CollapsedAndExpander_OnChecked(object sender, RoutedEventArgs e)
        {
            RoutedEventArgs args = new RoutedEventArgs(WindowCollapsedEvent, this);
            RaiseEvent(args);
        }
        /// <summary>
        ///展开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CollapsedAndExpander_OnUnchecked(object sender, RoutedEventArgs e)
        {
            RoutedEventArgs args = new RoutedEventArgs(WindowExpanderEvent, this);
            RaiseEvent(args);
        }

        #endregion

        
    }

    //[Serializable]
    /// <summary>
    /// 任务类型  
    /// </summary>
    public class TaskItem : INotifyPropertyChanged
    {
        //[field:NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

#region 字段

        private string _taskName ;
        private string _taskTag;
        private double _startTime = 0;
        private double _endTime = 2;
#endregion
#region 属性
        /// <summary>
        /// 任务名
        /// </summary>
        public string TaskName
        {
            get { return _taskName; }
            set
            {
                _taskName = value;
                OnPropertyChanged("TaskName");
            }
        }
        /// <summary>
        /// 任务附加信息
        /// </summary>
        public string TaskTag
        {
            get { return _taskTag; }
            set
            {
                _taskTag = value;
                OnPropertyChanged("TaskTag");
            }
        }
        /// <summary>
        /// 起始时间
        /// </summary>
        public double StartTime
        {
            get { return _startTime; }
            set
            {
                _startTime = value;
                OnPropertyChanged("StartTime");
            }
        }
        /// <summary>
        /// 结束时间
        /// </summary>
        public double EndTime
        {
            get { return _endTime; }
            set
            {
                _endTime = value;
                OnPropertyChanged("EndTime");
            }
        }
#endregion
    }
}
