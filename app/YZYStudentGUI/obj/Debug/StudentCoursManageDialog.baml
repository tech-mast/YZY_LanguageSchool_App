   M S B A M L   `   `   `  ����H  DYZYStudentGUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:YZYStudentGUIYZYStudentGUI  5   	   C&clr-namespace:YZYStudentGUI.PropertiesYZYStudentGUI.Properties  5   	   ,    &YZYStudentGUI.StudentCoursManageDialog   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:YZYStudentGUI     5   	   4p&clr-namespace:YZYStudentGUI.Properties     5   	   b ^MaterialDesignThemes.Wpf, Version=4.0.0.2422, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5	   	   -   5          � Title$  StudentCoursManageDialog��5
   	   $	��750��6*   $
��1300��67    � WindowStartupLocation$ CenterOwner=�6[    � 
ResizeMode$ NoResize=�6   .��5      ]� .+�5   
   �  � ColumnDefinitions 5      �� 5      $
��1.3* �6#   �� 5      $	��350 �6#    � RowDefinitions 5      �� 5      $��* �6    .+�5      (  "MaterialDesignThemes.Wpf.ColorZone     CornerRadius$ 10��5      +  %MaterialDesignThemes.Wpf.ShadowAssist   ShadowDepth$ Depth3=�6#   ���	#FF2196F3$��1q�6l   $��5q�6�   $��Stretch=�6�   $��Stretch=�6�   .��5       � -   5         �Name$ gridLeftPanel��5       $��0��6.    5"      �� 5#      $��25 �6(   �� 5$      $��* �6(   �� 5%      $��25 �6(   �� 5&      $��* �6(   .+�5(      �� (   "YZYStudentGUI.Properties.Resources!   content_registered_courses#��Z 6   $��0��6Y   �� 5+      $��1��6#   $��0��60   $��8q�6@   $
��Auto=�6K   $
��Auto=�6p   .��5,      �� -   5        	 ��Name$	 lvRegisters��5,   #   
 �� View
 5-      �� 5.   "   .P�5/   &   �� "   column_category_description#��Z 65    �� DisplayMemberBinding 6q   �� *
CateDesc+�� 50   &      column_course_name#��Z 65    6h   �� *
CourseDesc+�� 51   &      column_teacher_name#��Z 65    6i   �� *	Teacher+�� 52   &      column_start_date#��Z 65    6g   �� *StartDateStr+�� 53   &      column_end_date#��Z 65    6e   �� *
EndDateStr+�� 54   &      column_tuition#��Z 65    6d   �� *
TuitionStr+55   #   5=      5>      �� 5A         content_open_courses#��Z 6   $��2��6S   �� 5C      $��3��6#   $��0��60   $��8q�6@   $
��Auto=�6K   $
��Auto=�6p   .��5D      �� -   5        $	 lvOpenCourses��5D   #   
 5E      �� 5F   "   .P�5G   &   �� #��Z 65    6q   �� *
CateDesc+�� 5H   &   #��Z 65    6h   �� *
CourseDesc+�� 5I   &   #��Z 65    6i   �� *	Teacher+�� 5J   &   #��Z 65    6g   �� *StartDateStr+�� 5K   &   #��Z 65    6e   �� *
EndDateStr+�� 5L   &   #��Z 65    6d   �� *
TuitionStr+5M   #   5U      5V      5X      5Z      � 5\      $��0��6   $��1��6    $��15q�60    5]      �� 5^      $��* �6$   �� 5_      $��* �6$   �� 5`      $��* �6$   �� 5a      $��* �6$   �� 5b      $��* �6$   �� 5c      $��* �6$   �� 5d      $��* �6$   �� 5e      $��* �6$    5g      �� 5h      $��* �6'   �� 5i      $��2* �6'   .+�5k      �� $��0��6   $��0��6%      column_course_id#��Z 65   $
��left=�6g   $��Center=�6�   $��80��6�   �� 5l      -   5        $��
lbCourseID��5l      $��0��6*   $��1��67   $��--��6G   $
��left=�6T   $��Center=�6o   $��80��6�   �� 5m      $��1��6   $��0��6%      column_category#��Z 65   $
��left=�6f   $��Center=�6�   $��80��6�   �� 5n      -   5        $��
lbCateDesc��5n      $��1��6*   $��1��67   $��--��6G   $
��left=�6T   $��Center=�6o   $��80��6�   �� 5o      $��2��6   $��0��6%   #��Z 65   $
��left=�6i   $��Center=�6�   $��90��6�   �� 5p      -   5        $��lbCourseDesc��5p      $��2��6,   $��1��69   $��--��6I   $
��left=�6V   $��Center=�6q   $��80��6�   �� 5q      $��3��6   $��0��6%   #��Z 65   $
��left=�6j   $��Center=�6�   $��90��6�   �� 5r      -   5        $��	lbTeacher��5r      $��3��6)   $��1��66   $��--��6F   $
��left=�6S   $��Center=�6n   $��80��6�   �� 5s      $��4��6   $��0��6%   #��Z 65   $
��left=�6h   $��Center=�6�   $��80��6�   �� 5t      -	   5         ��Name$ lbStartDate��5t      $��4��6-   $��1��6:   $	��200��6J   �� 5u      $��5��6   $��0��6%   #��Z 65   $
��left=�6f   $��Center=�6�   $��80��6�   �� 5v      -
   5        $ 	lbEndDate��5v      $��-��6+   $��5��67   $��1��6D   $	��200��6T   �� 5w      $��6��6   $��0��6%   #��Z 65   $
��left=�6e   $��Center=�6�   $��80��6�   �� 5x      -   5        $ 	lbTuition��5x      $��6��6+   $��1��68   $��--��6H   $
��left=�6U   $��Center=�6p   $��80��6�   �� 5y      $��7��6   $��0��6+   $��2��6;    �� Orientation$ 
Horizontal=�6O   $��Right=�6h   .+�5z      �� -   5         ��Name$ btAddCourse��5z       &  "MaterialDesignFloatingActionButton#��[  5{       � ToolTip$( "MaterialDesignFloatingActionButton��5|      $��
10,0,20,10q�6I   $��40��6]   $��40��6h   .��5}      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$	 Add=�62   5~      �� 5      -   5        $ btDeleteRegister��5      #��[  5�      $( "MaterialDesignFloatingActionButton��5�      $��
10,0,20,10q�6I   $��40��6]   $��40��6h   .��5�        $ Delete=�62   5�      5�      5�      5�      5�      5�      