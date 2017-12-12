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
    /// 自定义TaskBar大小变化Thumb by cumtjie
    /// </summary>
    public class ResizeThumb : Thumb
    {
        #region 依赖属性
        public static readonly DependencyProperty CanEditProperty = DependencyProperty.Register(
           "CanEdit", typeof(bool), typeof(ResizeThumb), new PropertyMetadata(true));
        /// <summary>
        /// 设定该控件是否为可编辑
        /// </summary>
        public bool CanEdit
        {
            get { return (bool)GetValue(CanEditProperty); }
            set { SetValue(CanEditProperty, value); }
        }

        public static readonly DependencyProperty IsPlayingProperty = DependencyProperty.Register(
            "IsPlaying", typeof(bool), typeof(ResizeThumb), new PropertyMetadata(false));
        /// <summary>
        /// 是否在播放状态
        /// </summary>
        public bool IsPlaying
        {
            get { return (bool)GetValue(IsPlayingProperty); }
            set { SetValue(IsPlayingProperty, value); }
        }
        /**
         * 根据变化设置总时长宽度，并传递上去 by cumtjie 
         */
        public static readonly DependencyProperty TimeLineWidthProperty = DependencyProperty.Register(
            "TimeLineWidth", typeof(double), typeof(ResizeThumb), new PropertyMetadata(500.0));

        public double TimeLineWidth
        {
            get { return (double)GetValue(TimeLineWidthProperty); }
            set { SetValue(TimeLineWidthProperty, value); }
        }

        /**
         * 绑定TaskBar对应属性 by cumtjie
         */
        public static readonly DependencyProperty BarWidthProperty = DependencyProperty.Register(
            "BarWidth", typeof(double), typeof(ResizeThumb), new PropertyMetadata(40.0));

        public double BarWidth
        {
            get { return (double)GetValue(BarWidthProperty); }
            set { SetValue(BarWidthProperty, value); }
        }
        public static readonly DependencyProperty BaseWidthProperty = DependencyProperty.Register(
            "BaseWidth", typeof(int), typeof(ResizeThumb), new PropertyMetadata(10));

        public int BaseWidth
        {
            get { return (int)GetValue(BaseWidthProperty); }
            set { SetValue(BaseWidthProperty, value); }
        }

        /**
         * 通过起始时间结束时间改变宽度，并把起始时间结束时间传递上去 by cumtjie 
         */
        public static readonly DependencyProperty ResizeStartTimeProperty = DependencyProperty.Register(
            "ResizeStartTime", typeof(double), typeof(ResizeThumb), new PropertyMetadata(default(double)));

        public double ResizeStartTime
        {
            get { return (double)GetValue(ResizeStartTimeProperty); }
            set { SetValue(ResizeStartTimeProperty, value); }
        }
        public static readonly DependencyProperty ResizeEndTimeProperty = DependencyProperty.Register(
           "ResizeEndTime", typeof(double), typeof(ResizeThumb), new PropertyMetadata(default(double)));

        public double ResizeEndTime
        {
            get { return (double)GetValue(ResizeEndTimeProperty); }
            set { SetValue(ResizeEndTimeProperty, value); }
        }
        #endregion
        #region 路由事件

        public static readonly RoutedEvent TimeLineWidthChangedRoutedEvent =
            EventManager.RegisterRoutedEvent("TimeLineWidthChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler),
                typeof(ResizeThumb));
        public event RoutedEventHandler TimeLineWidthChanged
        {
            add { AddHandler(TimeLineWidthChangedRoutedEvent, value); }
            remove { RemoveHandler(TimeLineWidthChangedRoutedEvent, value); }
        }
        #endregion
        public ResizeThumb()
        {
            DragDelta += new DragDeltaEventHandler(this.ResizeThumb_DragDelta);
        }

        private void ResizeThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (CanEdit && !IsPlaying)
            {
                System.Windows.Controls.Control designerItem = this.DataContext as System.Windows.Controls.Control;

                double changeWidth = Math.Ceiling(e.HorizontalChange / BaseWidth) * BaseWidth;
                if (Math.Abs(changeWidth) < BaseWidth)
                    return;
                if (designerItem != null)
                {
                    double deltaVertical, deltaHorizontal;

                    switch (this.VerticalAlignment)
                    {
                        case System.Windows.VerticalAlignment.Bottom:
                            deltaVertical = Math.Min(-e.VerticalChange, designerItem.ActualHeight - designerItem.MinHeight);
                            designerItem.Height -= deltaVertical;
                            break;
                        case System.Windows.VerticalAlignment.Top:
                            deltaVertical = Math.Min(e.VerticalChange, designerItem.ActualHeight - designerItem.MinHeight);
                            Canvas.SetTop(designerItem, Canvas.GetTop(designerItem) + deltaVertical);
                            designerItem.Height -= deltaVertical;
                            break;
                        default:
                            break;
                    }

                    switch (HorizontalAlignment)
                    {
                        case System.Windows.HorizontalAlignment.Left:
                            deltaHorizontal = Math.Min(changeWidth, designerItem.ActualWidth - designerItem.MinWidth);
                            Canvas.SetLeft(designerItem, Canvas.GetLeft(designerItem) + deltaHorizontal);
                            designerItem.Width -= deltaHorizontal;
                            break;
                        case System.Windows.HorizontalAlignment.Right:

                            deltaHorizontal = Math.Min(-changeWidth, designerItem.ActualWidth - designerItem.MinWidth);
                            designerItem.Width -= deltaHorizontal;
                            break;
                        default:
                            break;
                    }
                    BarWidth = designerItem.Width;
                    ResizeStartTime = (int)(Canvas.GetLeft(designerItem) / BaseWidth);
                    ResizeEndTime = ResizeStartTime + Math.Floor(BarWidth / BaseWidth);
                    if ((ResizeStartTime * BaseWidth + BarWidth) > TimeLineWidth)
                    {
                        TimeLineWidth = ResizeStartTime * BaseWidth + BarWidth;
                        RoutedEventArgs args = new RoutedEventArgs(TimeLineWidthChangedRoutedEvent, this);
                        RaiseEvent(args);
                    }
                }

                e.Handled = true;
            } 
        }
    }
}
