using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using TaskTimeLineLab.Control;

namespace TaskTimeLineLab.Common
{
    /// <summary>
    /// 自定义Thumb（控制TaskBar移动）by cumtjie
    /// </summary>
    public class MoveThumb : Thumb
    {
        #region 依赖属性
        public static readonly DependencyProperty CanEditProperty = DependencyProperty.Register(
           "CanEdit", typeof(bool), typeof(MoveThumb), new PropertyMetadata(true));
        /// <summary>
        /// 设定该控件是否为可编辑
        /// </summary>
        public bool CanEdit
        {
            get { return (bool)GetValue(CanEditProperty); }
            set { SetValue(CanEditProperty, value); }
        }

        public static readonly DependencyProperty IsPlayingProperty = DependencyProperty.Register(
            "IsPlaying", typeof(bool), typeof(MoveThumb), new PropertyMetadata(false));
        /// <summary>
        /// 是否在播放状态
        /// </summary>
        public bool IsPlaying
        {
            get { return (bool)GetValue(IsPlayingProperty); }
            set { SetValue(IsPlayingProperty, value); }
        }
        public static readonly DependencyProperty SelecTaskItemProperty = DependencyProperty.Register(
            "SelectedTaskItem", typeof(TaskItem), typeof(MoveThumb), new PropertyMetadata(default(TaskItem)));

        public TaskItem SelectedTaskItem
        {
            get { return (TaskItem)GetValue(SelecTaskItemProperty); }
            set { SetValue(SelecTaskItemProperty, value); }
        }

        public static readonly DependencyProperty BarWidthProperty = DependencyProperty.Register(
            "BarWidth", typeof(double), typeof(MoveThumb), new PropertyMetadata(40.0));

        public double BarWidth
        {
            get { return (double)GetValue(BarWidthProperty); }
            set { SetValue(BarWidthProperty, value); }
        }
        public static readonly DependencyProperty TimeLineWidthProperty = DependencyProperty.Register(
            "TimeLineWidth", typeof(double), typeof(MoveThumb), new PropertyMetadata(500.0));

        public double TimeLineWidth
        {
            get { return (double)GetValue(TimeLineWidthProperty); }
            set { SetValue(TimeLineWidthProperty, value); }
        }
        public static readonly DependencyProperty BaseWidthProperty = DependencyProperty.Register(
            "BaseWidth", typeof(int), typeof(MoveThumb), new PropertyMetadata(20));

        public int BaseWidth
        {
            get { return (int)GetValue(BaseWidthProperty); }
            set { SetValue(BaseWidthProperty, value); }
        }
        public static readonly DependencyProperty MoveStartTimeProperty = DependencyProperty.Register(
            "MoveStartTime", typeof(double), typeof(MoveThumb), new PropertyMetadata(default(double)));

        public double MoveStartTime
        {
            get { return (double)GetValue(MoveStartTimeProperty); }
            set { SetValue(MoveStartTimeProperty, value); }
        }
        public static readonly DependencyProperty MoveEndTimeProperty = DependencyProperty.Register(
            "MoveEndTime", typeof(double), typeof(MoveThumb), new PropertyMetadata(default(double)));

        public double MoveEndTime
        {
            get { return (double)GetValue(MoveEndTimeProperty); }
            set { SetValue(MoveEndTimeProperty, value); }
        }
        #endregion
        #region 路由事件

        public static readonly RoutedEvent MoveThumebChangedRoutedEvent =
            EventManager.RegisterRoutedEvent("MoveThumebChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler),
                typeof(MoveThumb));
        public event RoutedEventHandler MoveThumebChanged
        {
            add { AddHandler(MoveThumebChangedRoutedEvent, value); }
            remove { RemoveHandler(MoveThumebChangedRoutedEvent, value); }
        }

        public static readonly RoutedEvent TimeLineWidthChangedRoutedEvent =
            EventManager.RegisterRoutedEvent("TimeLineWidthChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler),
                typeof(MoveThumb));
        public event RoutedEventHandler TimeLineWidthChanged
        {
            add { AddHandler(TimeLineWidthChangedRoutedEvent, value); }
            remove { RemoveHandler(TimeLineWidthChangedRoutedEvent, value); }
        }
        #endregion
        public MoveThumb()
        {
            DragStarted += MoveThumb_DragStarted;
            DragDelta += new DragDeltaEventHandler(this.MoveThumb_DragDelta);
        }

        /// <summary>
        /// 由于MoveThumb在TaskBar之上，此事件阻塞ListBox左键选中事件
        /// 通过TaskBar的Tag属性携带选中Bar的信息，然后赋给SelectedTaskItem
        /// 通过绑定传给ListBox的SelectItem,从而使用鼠标选中MoveThumb之后可以设置
        /// ListBox选中对应item（使用装饰器，在装饰层添加Thumb，是否能解决消息屏蔽，有待测试）（by cumtjie）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MoveThumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            System.Windows.Controls.Control designerItem = this.DataContext as System.Windows.Controls.Control;
            TaskBar taskBar = designerItem.Tag as TaskBar;
            SelectedTaskItem = taskBar.Tag as TaskItem;
        }
        /// <summary>
        /// 传递当前所选TaskBar（由于MoveThumb遮挡住）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MoveThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (CanEdit && !IsPlaying)
            {
                System.Windows.Controls.Control designerItem = this.DataContext as System.Windows.Controls.Control;

                if (designerItem != null)
                {
                    double left = Canvas.GetLeft(designerItem);
                    //double top = Canvas.GetTop(designerItem);
                    double offsize = 0.0;
                    if (left + e.HorizontalChange < 0)
                    {
                        offsize = 0.0;
                    }
                    else
                    {
                        offsize = left + e.HorizontalChange;
                        offsize = (int)(offsize / BaseWidth) * BaseWidth;
                    }
                    Canvas.SetLeft(designerItem, offsize);
                    MoveStartTime = (int)(Canvas.GetLeft(designerItem) / BaseWidth);
                    BarWidth = designerItem.Width;
                    MoveEndTime = MoveStartTime + Math.Floor(BarWidth / BaseWidth);
                    if ((MoveStartTime * BaseWidth + BarWidth) > TimeLineWidth)
                    {
                        TimeLineWidth = MoveStartTime * BaseWidth + BarWidth;
                        RoutedEventArgs args = new RoutedEventArgs(TimeLineWidthChangedRoutedEvent, this);
                        RaiseEvent(args);
                    }

                    RoutedEventArgs movergs = new RoutedEventArgs(MoveThumebChangedRoutedEvent, this);
                    RaiseEvent(movergs);
                    //Canvas.SetTop(designerItem, top + e.VerticalChange);
                }
            }
           
        }
    }
}
