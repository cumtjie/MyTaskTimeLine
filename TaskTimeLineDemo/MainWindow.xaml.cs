using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TaskTimeLineLab.Control;

namespace TaskTimeLineDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            TaskItem task1 = new TaskItem() {StartTime = 3,EndTime = 8,TaskName = "任务1",TaskTag = "分类1"};
            TaskItem task2 = new TaskItem() { StartTime = 3, EndTime = 8, TaskName = "任务1", TaskTag = "分类1" };
            TaskItem task3 = new TaskItem() { StartTime = 3, EndTime = 8, TaskName = "任务1", TaskTag = "分类1" };
            TaskItem task4 = new TaskItem() { StartTime = 3, EndTime = 8, TaskName = "任务1", TaskTag = "分类1" };
            TaskItem task5 = new TaskItem() { StartTime = 3, EndTime = 8, TaskName = "任务1", TaskTag = "分类1" };
            Items = new ObservableCollection<TaskItem>();
            Items.Add(task1);
            Items.Add(task2);
            Items.Add(task3);
            Items.Add(task4);
            Items.Add(task5);
            InitializeComponent();
        }

        public ObservableCollection<TaskItem> Items { get; set; }
        //private void MainWindow_OnLocationChanged(object sender, EventArgs e)
        //{
        //    RefreshScaleBarPopup();
        //}

        //public void RefreshScaleBarPopup() //老版本废弃
        //{
        //    if (TaskTimeLineLab.RefreshScaleBarPopupHandel != null)
        //    {
        //        if (this.Visibility == Visibility.Visible)
        //        {
        //            TaskTimeLineLab.RefreshScaleBarPopupHandel(true);
        //        }
        //        else
        //        {
        //            TaskTimeLineLab.RefreshScaleBarPopupHandel(false);
        //        }
        //    }
        //}

        private void TaskTimeLineLab_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void TaskTimeLineLab_TaskBarStart(object sender, RoutedEventArgs e)
        {
            ;
        }

        private void TaskTimeLineLab_TaskBarEnd(object sender, RoutedEventArgs e)
        {
            ;
        }

        private void TaskTimeLineLab_TaskItemSelectChanged(object sender, RoutedEventArgs e)
        {
            ;
        }
    }
}
