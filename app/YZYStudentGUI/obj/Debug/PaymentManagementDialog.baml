   M S B A M L   `   `   `  ����H  DYZYStudentGUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:YZYStudentGUIYZYStudentGUI  5   	   C&clr-namespace:YZYStudentGUI.PropertiesYZYStudentGUI.Properties  5   	   +    %YZYStudentGUI.PaymentManagementDialog   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:YZYStudentGUI     5   	   4p&clr-namespace:YZYStudentGUI.Properties     5   	   b ^MaterialDesignThemes.Wpf, Version=4.0.0.2422, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5	   	   -   5          � Title$  PaymentManagementDialog��5
   	   $	��550��6)   $	��950��66    � WindowStartupLocation$ CenterOwner=�6Y   .��5      ]� .+�5   
   �  � ColumnDefinitions 5      �� 5      $
��1.3* �6#   �� 5      $��1* �6#    � RowDefinitions 5      �� 5      $��* �6    �� 5      $��* �6    �� 5      $��5* �6    .+�5      �� $��0��6   $��0��6'   $��2��67    �� Orientation$ 
Horizontal=�6K   $��20q�6d   $��Center=�6p   .+�5      �� (   "YZYStudentGUI.Properties.Resources   column_pay_account#��Z 6   $
��left=�6M   $��Center=�6h   $��80��6�   �� 5      -   5         ��Name$ tbPayAccount��5      $	��200��61   $��30��6=   �� 5         column_amount#��Z 6   $
��left=�6H   $��Center=�6c   $��80��6~   �� 5      -   5        $ tbAmount��5      $	��200��6,   $��30��68   �� 5      -   5           MaterialDesignRaisedButton#��[  5      +  %MaterialDesignThemes.Wpf.ButtonAssist   CornerRadius$ 10��5      	 � ToolTip$3	 -MaterialDesignRaisedButton with Round Corners��5       .��5!      �� 
   content_pay#��Z
 6    5"      5#      #  MaterialDesignThemes.Wpf.Card  5$      $��1��5%      $��0��6    $��2��60   ���#e6faff���	#FF2196F3$��	20,5,20,5q�5'         UniformCornerRadius$ 10Q�5(      .��5)      �� $ 
Horizontal=�6   .+�5*      ��    content_tuition_total#��Z 6   $
��left=�6T   $��Center=�6o   $��80��6�   ���	#FF2196F3$
��Bold"�6�   �� 5+      -   5         ��Name$ lbTuitionTotal��5+      $��--��65   $
��left=�6B   $��Center=�6]   $��80��6x   ���	#FF2196F3$
��Bold"�6�   �� 5,         content_paid_tuition#��Z 6   $
��left=�6S   $��Center=�6n   $��80��6�   ���	#FF2196F3$
��Bold"�6�   �� 5-      -   5        $ lbPaidTuition��5-      $��--��64   $
��left=�6A   $��Center=�6\   $��80��6w   ���	#FF2196F3$
��Bold"�6�   �� 5.         content_balance#��Z 6   $
��left=�6N   $��Center=�6i   $��80��6�   ���	#FF2196F3$
��Bold"�6�   �� 5/      -   5        $ 	lbBalance��5/      $��--��60   $
��left=�6=   $��Center=�6X   $��80��6s   ���	#FF2196F3$
��Bold"�6�   50      51      (  "MaterialDesignThemes.Wpf.ColorZone  52      $��2��6'   $��0��64   $��2��6D      CornerRadius$ 10��53      +  %MaterialDesignThemes.Wpf.ShadowAssist   ShadowDepth$ Depth3=�6#   ���	#FF2196F3$��1q�6l   $��20q�6�   $��Stretch=�6�   $��Stretch=�6�   .��54      �� $��5q�6   $
��Auto=�6*   $
��Auto=�6O   .��55      �� -   5        $��
lvPayments��55       �� View 56      �� 57      .P�59   "   �� #��Z 61    �� DisplayMemberBinding 6d   �� *
PayAccount+�� 5:   "   #��Z 61    6_   �� *	AmountStr+�� 5;   "      column_pay_date#��Z 61    6a   �� *
PayDateStr+5=      5E      5F      5G      5H      5I      5J      