   M S B A M L   `   `   `  ���� K  GTabletManagerWPF, Version=2.0.1.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:TabletManagerWPF.View.PagesTabletManagerWPF.View.Pages  W0clr-namespace:TabletManagerWPF.View.UserControls"TabletManagerWPF.View.UserControls  W0clr-namespace:TabletManagerWPF.View.UserControls"TabletManagerWPF.View.UserControls  =#clr-namespace:TabletManagerWPF.ViewTabletManagerWPF.View  G(clr-namespace:TabletManagerWPF.ViewModelTabletManagerWPF.ViewModel  -    'TabletManagerWPF.View.Pages.TabletsPage   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     ;local)clr-namespace:TabletManagerWPF.View.Pages     Econtrols0clr-namespace:TabletManagerWPF.View.UserControls     IUserControls0clr-namespace:TabletManagerWPF.View.UserControls     4View#clr-namespace:TabletManagerWPF.View     >	viewmodel(clr-namespace:TabletManagerWPF.ViewModel       N� Title$  TabletsPage�� � 	Resources  �� MergedDictionaries �� Source��  �� $ /Resources/Styles.xaml?�%    .��� 
������� � ColumnDefinitions �� $	��51* ��� $
��Auto ��� $	��49* �.+�&   System.Windows.Controls.DataGrid  -     Name$ dataGrid��$��
10,49,5,10q�   AutoGenerateColumns .     
IsReadOnly . ���� *	Tablets+ �� SelectedItem �� *SelectedTablet+	 �� Mode$	 TwoWay=�
   Columns
 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Nazwa��1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *WindowsName+  $ Odpowiedzialny�� �� *Responsible+  $ Typ blokady�� �� *
LockType+  -   $ 	dataGrid1��$��10,279,10,10q�$��2�� .  ���� *TabletEvents+
   $ Tablet�� �� *
SourceName+  $ Osoba�� �� *	CreatedBy+  $
 Data�� �� *CreationDate+  $ Event�� �� *	EventType+  $
 Opis�� �� *	Content+6   0TabletManagerWPF.View.UserControls.TabletControl  $��2��$	��215��$	��Top=�$��
10,10,10,0q�   tablet �� *SelectedTablet+�� -    ��Name$ button��   ButtonPL#��[  $��Zmień lokalizacje��$��	0,13,91,0q�$	��Top=����	#FF707070  ButtonStyle#��[ $��Right=�$��95������ *ChangeLocationCommand+�� -   $ button1��#��[  $��
Drukuj kod��$��	0,13,10,0q�$	��Top=�#��[ $��Right=�$��62������ *PrintTabletBarcodeCommand+�� -    ��Name$ checkBox��$��2��$��Tylko przekazania��$
��Left=�$��
20,255,0,0q�$	��Top=� d� 	IsChecked �� *
PassOnly+�� $��Z ostatniej :��$��2��$��
0,239,10,0q�$��35��$	��Top=�$��Right=�$	��244��.����  �� Orientation$ 
Horizontal=�$��
73,-5,0,-5q�.+�	� -    	�Name$ radioButton��$��1��$��1h��$
��Left=�$	��Top=�$��5,0,0,0q� �� *	OneHour+	� -   $ radioButton_Copy��$��1��$	��12h��$
��Left=�$	��Top=�$��10,0,0,0q� �� *TvelweHGour+	� -   $ radioButton_Copy1��$��1��$	��24h��$
��Left=�$	��Top=�$��10,0,0,0q� �� *OneDay+�� $��1��$��3��$��Stretch=�$��Center=�
�������