   M S B A M L   `   `   `  ����F  BYZYAdminGUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:YZYAdminGUIYZYAdminGUI  5   	   ?$clr-namespace:YZYAdminGUI.PropertiesYZYAdminGUI.Properties  5   	   E ADragablz, Version=0.0.3.223, Culture=neutral, PublicKeyToken=null5(clr-namespace:Dragablz;assembly=DragablzDragablz 5   	       YZYAdminGUI.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:YZYAdminGUI     5   	   2p$clr-namespace:YZYAdminGUI.Properties     5   	   =dragablz(clr-namespace:Dragablz;assembly=Dragablz     5   	   b ^MaterialDesignThemes.Wpf, Version=4.0.0.2422, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5
   	   -   5          � WindowStyle$
  None=�5
   R   &    YZYAdminGUI.Properties.Resources   windows_title � Title# Z 5   	   $	��770��66   $	��950��6C    � 
ResizeMode$ 	CanResize=�6O    � WindowStartupLocation$ CenterScreen=�6f   .��5      ]� .+�5   
   � $	��Top=�6   $��66��6$   ���#ffbf80.+�5      �� #��Z 6   
�������$��Center=�6W   $��Center=�6t   $��22(�6�   $��Champagne & Limousines)�6�   $
��Bold"�6�   �� 5       �� Orientation$ 
Horizontal=�6   $��40��62   $��Right=�6>   $��10 10 70 10q�6Z   .+�5      �� $��Center=�5      $��En��5      
�������6   d� 5      -   5         d�Name$ LanguageToggle��5      $��8 0 16 0q�5      5   )   �� 5       ��Language$ zh-Hans�5   $   $��Center=�5      
�������中文5      5      �� 56      -   5        $��Right=�56      $��Center=�61   ��6L   Z� ��6b   Z� .��57      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ Power=�6*   58      59      � 5:   
   $	��Top=�6   .+�5;        Dragablz.TabablzControl  	   InterTabController	 5<      !  Dragablz.InterTabController  5=      .@�5?      �� -   5        
 ��Name$
 
tabWelcome��5?   B      welcome#��Z 6   .��5@         YZYAdminGUI.UCWelcomePage  -   5          Name$ 	ucWelcome��5@   S   $��10q�6*   $��Center=�66   6S   5A      �� 5B      -   5        $
 	tabCourse��5B   M      content_course_tab#��Z 6   .��5D      #   YZYAdminGUI.UCSearchingCourse  -   5          Name$ ucCourse��5D   W   $��10q�6.   $��Center=�6:   6W   5E      �� 5F      -   5        $
 
tabAccount��5F   N      content_account_tab#��Z 6   .��5G      $   YZYAdminGUI.UCSearchingAccount  -	   5          Name$ 	ucAccount��5G   X   $��10q�6/   $��Center=�6;   6X   5H      �� 5I      -
   5        $
 
tabTeacher��5I   N      content_teacher_tab#��Z 6   .��5J         YZYAdminGUI.UCTeacher  -   5          Name$ 	ucTeacher��5J   O   $��10q�6&   $��Center=�62   6O   5K      �� 5L      -   5        $
 
tabProfile��5L   j      content_profile_tab#��Z 6   $��right=�6N   .��5M       	   YZYAdminGUI.UCAdminProfile	  -   5         	 Name$ 	ucProfile��5M   T   $��10q�6+   $��Center=�67   6T   5N      5O      5P      5Q      5R      