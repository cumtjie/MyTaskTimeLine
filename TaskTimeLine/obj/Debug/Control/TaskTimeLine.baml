   M S B A M L   `   `   `  ����J  FTaskTimeLineLab, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:TaskTimeLineLab.ControlTaskTimeLineLab.Control  5      A%clr-namespace:TaskTimeLineLab.ConvertTaskTimeLineLab.Convert  5      *    $TaskTimeLineLab.Control.TaskTimeLine   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      7local%clr-namespace:TaskTimeLineLab.Control     5      9convert%clr-namespace:TaskTimeLineLab.Convert     5      -   5          >�Name$
  this��5	      ���	#FF505b66 � 	Resources 5
       �� MergedDictionaries �� Source/   )TaskTimeLineLab.Convert.TimeConvertMinute   TimeConvertMinute/   )TaskTimeLineLab.Convert.TimeConvertSecond  TimeConvertSecond1   +TaskTimeLineLab.Convert.BoolConvertVisiable  BoolConvertVisiable8   2TaskTimeLineLab.Convert.ReverseBoolConvertVisiable  ReverseBoolConvertVisiable�� 5   
    5      �� 5      $ ../Themes/Generic.xaml?�6%   %t   &	        &	      &	 &     &	 9       5      6(     5      6(     5      6*     5      61   5       J� InputBindings 5      d� 5   
    d� Gesture$  a�6   6$   .��5      �  � RowDefinitions 5   
   �� 5      $��40 �6   �� 5      $��35 �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $	��100 �6   �� 5       $��1 �6   �� 5!      $��* �6   .+�5$   
   �� $��0��6   $��3��6   $��3��6/   $��0q�6C   $��0 0 0 10q�6W   ��5%      �� 5&       �� EndPoint$ 0.5,1&�6&   	 �� 
StartPoint$	 0.5,0&�67   .��5'      � 
 � Color$
 	#F2142E40��6#    � Offset$ 0Q�65   � 5(      $
 	#F2142E40��6#   $ 1Q�65   � 5)      $
 	#CC142E40��6#   $ 0.502Q�65   5*      5,      �� 5-   
   $��0��6   $��3��6   $��0q�62   $��0 0 0 10q�6F   ��5.      �� 5/       �� ImageSource$ ../Resources/headbg.png��6   51      �� 52   
   -   5         ��Name$ editStackPanel��52      $��0��6�   $��0��6�   $��	10 5 0 13q�6�    �� Orientation$ 
Horizontal=�6�   y�6-   ��  �� ElementName$
 this�� �� Path$ CanEdit� �� 	Converter# [ .+�55      �� -   5         ��Name$	 Add��55       � ToolTip$ 新增��6"   $��25��6/   $��0 0 5 0q�6:   ��56      �� 57      $ ../Resources/新增.png��6!   59      �� 5:      -   5        $	 Del��5:      $ 删除��6"   $��15��6/   ��5;      �� 5<      $ ../Resources/减少.png��6!   5>      5?      �� 5@   
   -   5        $ PlayStackPanel��5@       J� 	AllowDrop . $��Center=�6>   $��3��6[   $��0��6p   $��	10 5 0 13q�6}   $ 
Horizontal=�6�   .+�5A      �� -   5        $ 
Decelerate��5A      $ 减速��6)   $��15��66   $��15��6A   $��6 0 6 0q�6M   ��5B      �� 5C      $ ../Resources/快退.png��6!   5E      �� 5F      -   5         ��Name$ 	Play_Stop��5F      $ 播放/暂停��6*   $��6 0 6 0q�6:   $��15��6K     PlayCheckBoxStyle#��[ 6W   $��15��6�   6�   �� 5G      -   5        $ 
Accelerate��5G      $ 加速��6)   $��15��66   $��15��6A   $��6 0 6 0q�6M   ��5H      �� 5I      $ ../Resources/快进.png��6!   5K      �� 5L      -	   5        $
 Stop��5L      $ 停止��6#   $��15��60   $��15��6;   $��6 0 6 0q�6G   ��5M      �� 5N      $ ../Resources/停止.png��6!   5P      �� 5Q      -
   5        $ Enlarge��5Q      $ 放大��6&   $��15��63   $��15��6>   $��6 0 6 0q�6J   ��5R      �� 5S      $ ../Resources/放大.png��6!   5U      �� 5V      -   5        $ Shrink��5V      $ 缩小��6%   $��15��62   $��15��6=   $��6 0 6 0q�6I   ��5W      �� 5X      $ ../Resources/缩小.png��6!   5Z      5[      �� 5\   
   $ 
Horizontal=�6�   $��Right=�6�   $��0��6�   $��3��6�   $��0��6�   $��	0 5 10 13q�6�   y�6   �� $
 this��$ CanEdit�# [ .+�5]      �� -   5        $ CollapsedAndExpander��5]      $ 折叠/展开��65     ExpanderCheckBoxStyle#��[ 6E   $��15��6t   $��1 5 1 0q�6�   6�   �� 5^      -   5        $ 	MaxAndMin��5^      $ 最大/最小��6*     MaxMinCheckBoxStyle#��[ 6:   $��18��6g   $��1,4,1,0q�6t   6�   �� 5_      -   5        $ Close��5_      $ 关闭��6$   $��23��61   $��23��6<   $��1 0 5 0q�6H   ��5`      �� 5a      $ ../Resources/关闭.png��6!   5c      5d      �� 5e   
   $��1��6   $��0��6"   $��20 0 0 0q�62   $ 
Horizontal=�6D   $��Center=�6]   
������ $��14(�6�   $��微软雅黑)�6�   
�������.+�5f      �� $��1q�6   ��6#   �� $
 this��$ 	NowMinute��� 5g      $��1q�6   $	��：��6#   �� 5h      $��1q�6   ��6#   �� $
 this��$ 	NowSecond�5i      �� 5j   
   $��1��6   $��0��6!   ��61   �� $
 this��$ NowTime��� 5k   
   -   5         ��Name$ ScaleBarScrollViewer��5k   B     ScrollViewerStyle#��[ 6   $��2��6`   $��1��6p   $��Disabled=�6~   $��Hidden=�6�   .��5l      �� -   5        $ 
ticksPanel��5l      $��5 0 5 0q�66  $ 
Horizontal=�6G  
������ ��6  �� $ TimeLineWidth� �� Mode$ TwoWay=�6a  5m      � 5n   
   -   5         �Name$ taskNameGrid��5n      $��2��6%   $��0��62   .+�5o      �� -   5        $# TaskName_ListBox_ScrollViewer��5o      $��0q�6B   $��Hidden=�6V   .��5p      �� -   5         ��Name$ TaskName_ListBox��5p      ���	#66606971$��0 0 0 0q�6M   $��0q�6_   $	��Top=�6�   ��6�   �� $
 this��$ TaskItemSource���5q      �� 5r       �� 
TargetType" ��6      ListBoxItemStyle �� BasedOn# [ 69   .�5s      -   5        5t      ��5v      �� 5w      .R�5x      �� .+�5y   "   �� 
������ 
�������$��20��6X   ��6d   �� *
TaskName+$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=��� 5z   "   $��1��6,   
���   � J� Opacity$	 0.6Q�6E   $��0 1 0 0q�6S   6f   5{      5|      5~      5      5�      �� 5�   
   -   5         ��Name$ GridSplitter��5�      $��1��6-   � 5�   
   -   5        $ 	ticksGrid��5�   -   $��2��6   $��2��6   .+�5�      �� -   5        $ TimeLineScrollViewer��5�   z    	 for_scrollviewer#��[	 6   $
��Auto=�6�   $��Hidden=�6�   .��5�      �� -   5        $ Task_ListBox��5�      ���	#665D666F$��0q�6  $	��Top=�6}   �� SelectedIndex 6~   �� $
 this��$ SelectedTaskItemIndex�$ TwoWay=�  �� SelectedItem  6�   �� $
 this��$ SelectedTaskItem�$ TwoWay=���62  �� $
 this��$ TaskItemSource�$ TwoWay=���5�      �� 5�      " ��6    # [ 69   .�5�      -   5        5�      ��5�      �� 5�      .R�5�      �� .+�5�   "   %   TaskTimeLineLab.Control.TaskBar  !  Name$! taskBar��60   %   TaskTimeLineLab.Control.TaskBar"   MoveThumebChanged" TaskBar_OnMoveThumebChanged6�   #   TimeLineWidthChanged"# TaskBar_OnTimeLineWidthChanged6�   
$ � Tag$ 6B   �� %   SelectedTaskItem% 6R   �� $
 this��$ SelectedTaskItem�$ TwoWay=�&   	BaseWidth& 5�   4   �� $
 this��$ 	BaseWidth�'   TimeLineWidth' 6j   �� $
 this��$ TimeLineWidth�$ TwoWay=�(   	StartTime( 5�   4   �� *	StartTime+$ TwoWay=�)   EndTime) 6b   �� *	EndTime+$ TwoWay=�*   	IsPlaying* 6�   �� $
 this��$ 	IsPlaying�$ TwoWay=�+   CanEdit+ 5�   0   �� $
 this��$ CanEdit�$ TwoWay=� 5�   &   _� 5�   *    

 Simple#��[
 62   .��5�   .   �� $ 
Horizontal=�69   
������ $��14(�6k   $��微软雅黑)�6�   
�������.+�5�   2   �� $��1q�6<   ��6G   �� *	StartTime+# [  �� 5�   2   $��1q�6<   $	��：��6G   �� 5�   2   $��1q�6<   ��6G   �� *	StartTime+# [ 5�   /   5�   +   5�   #   �� 5�   "   $��1��6,   
���   �$	 0.6Q�6E   $��0 1 0 0q�6S   6f   5�      5�      5�      5�      '   !TaskTimeLineLab.Control.TrendLine  5�      -   5        ,  Name$, 	TrendLine��5�      '   !TaskTimeLineLab.Control.TrendLine-   	MouseMove$- Trend_OnMouseMove��61   .   MouseLeftButtonUp$. Trend_OnMouseLeftButtonUp��6O   /   
MouseLeave$/ Trend_OnMouseLeave��6}   0   MouseLeftButtonDown$!0 Trend_OnMouseLeftButtonDown��6�   $
��Left=�6  $��	0 -10 0 0q�6   1   	BaseWidth1 6�   �� $
 this��$ 	BaseWidth�6   5�      5�      5�      