   M S B A M L   `   `   `  ����J  FTaskTimeLineLab, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:TaskTimeLineLab.CommonTaskTimeLineLab.Common  5      %    TaskTimeLineLab.Control.TaskBar   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2s$clr-namespace:TaskTimeLineLab.Common     5      -   5          >�Name$
  this��5      $��20��5      $
��Auto��6    � 	Resources 5	         MoveThumbTemplate �� 
TargetType&    TaskTimeLineLab.Common.MoveThumb  ResizeDecoratorTemplate(   "TaskTimeLineLab.Common.ResizeThumb � Cursor   ResizeStartTime �� ElementName �� Path �� Mode   ResizeEndTime	   	BaseWidth
   CanEdit   	IsPlaying(   "TaskTimeLineLab.Common.ResizeThumb   TimeLineWidthChanged   TimeLineWidth   BarWidth  DesignerItemTemplate � DataContext �� RelativeSource ��Name&    TaskTimeLineLab.Common.MoveThumb   MoveThumebChanged   TimeLineWidthChanged   SelectedTaskItem
 � Tag   BarWidth   TimeLineWidth   MoveStartTime   MoveEndTime   	BaseWidth   CanEdit   	IsPlaying �� Triggers P� Property P� Value  �� 
TargetName! �� Property" �� Value��%$  &	        &	 B     &	 Y    7  7   �� 5
   
   "  64   .R�5      �� 
������ 6   5      �� 5   
   " ��6:   .R�5      � .+�5        
�������$��6��5   c   ���	#FF6a9189$ SizeWE��6�   $��-3 0 0 0q�6�   $��Stretch=�5      $
��Left=�64    5   4   �� $
 this��$ 	StartTime�$ TwoWay=� 6~   �� $
 this��$ EndTime�$ TwoWay=�	 5       �� $
 this��$ 	BaseWidth�$ TwoWay=�
 5   O   �� $
 this��$ CanEdit�$ TwoWay=� 6�   �� $
 this��$ 	IsPlaying�$ TwoWay=�  5       OnTimeLineWidthChanged64   
�������$��6��5   b   ���	#FF6a9189$ SizeWE��6�   $��0 0 -3 0q�6�   $��Stretch=�5      $��Right=�64    5   b   �� $
 this��$ TimeLineWidth�$ TwoWay=� 6�   �� $
 this��$ BarWidth�$ TwoWay=� 5       �� $
 this��$ 	StartTime�$ TwoWay=� 6k   �� $
 this��$ EndTime�$ TwoWay=�	 5       �� $
 this��$ 	BaseWidth�$ TwoWay=�
 5       �� $
 this��$ CanEdit�$ TwoWay=� 6^   �� $
 this��$ 	IsPlaying�$ TwoWay=�5   4   5#      5$      �� 5%   
   " ��67   .R�5&      �  6   ��  �� *TemplatedParent+.+�5'      �� $ control��6   8��  6+   �� 5(      #��z��6$     5)      ! MoveThumb_OnMoveThumebChanged6�    OnTimeLineWidthChanged6�   $��3 0 3 0q�6�   8�� $ SizeAll��5,   L    5)      �� $
 this��$ SelectedTaskItem�$ TwoWay=� 6n   �� $
 this��$	 Tag� 6	  �� $
 this��$ BarWidth�$ TwoWay=� 5*      �� $
 this��$ TimeLineWidth�$ TwoWay=� 6i   �� $
 this��$ 	StartTime�$ TwoWay=� 6�   �� $
 this��$ EndTime�$ TwoWay=� 6�   �� $
 this��$ 	BaseWidth�$ TwoWay=� 5+      �� $
 this��$ CanEdit�$ TwoWay=� 6\   �� $
 this��$ 	IsPlaying�$ TwoWay=�5,   L   5-       5.      P� 5/       �@J�IsMouseOver .  . �50      �� $  control��6   ! � y�$" 	Collapsed=�6H   6\   51      53      54      .��55      �� $��0q�6   # �� Padding$# 0,1,0 1q�6"   ._�56   
   �� -   5        $ ��Name$$ canvas��56      ��6!   �� $
 this��$ TimeLineWidth�$ TwoWay=�.+�57      �� -   5        % ��Name$
% task��57      & �� Left$& 0��6L   $��18��6]   #��[ 6�    6+   �� $
 this����6i   �� $
 this��$ BarWidth�$ TwoWay=�.��58      �� ���	#FF1AA9C1' �� Stretch$
' Fill=�6-   59      5:      5;      5<      