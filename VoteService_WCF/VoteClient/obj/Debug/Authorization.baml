   M S B A M L   `   `   `  ????E  AVoteClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:VoteClient
VoteClient  5   	       VoteClient.Authorization   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:VoteClient     5   	     ? Title$  Authorization??5   	   $	??204??6    ? Icon$ /Icons/key-person.png??6,   $	??300??6I    ? WindowState$ 	Maximized=?6U   ??5
      ?? 5   
    ?? EndPoint$ 0.5,1&?6    ?? 
StartPoint$ 0.5,0&?6/   .??5      ?  ? Color$ Black??6    ? Offset$ 0Q?6)   ? 5      $ White??6   5      .??5      ? $??Center=?6   $??0,2,0,-2q?6(   $	??188??6:   $	??232??6G   .+?5   
   ?? $"??Введите пароль:??6   $??Center=?6*   $??0,20,0,0q?6G   $	??Top=?6Y   $??18(?6q   $
??Bold"?6   ?? 5   
   -   5         ??Name$ 	bShowHide??5      $??177,60,20,0q?6%   $	??Top=?6:   $??16(?6R   
??????????	#FF111111$??25??6?   .??5      ?? -   5         ??Name$ btImage??5      $??\Icons\hide.png??6%   $??17??6>   6L   5      ?? 5   
   -   5        $ bConnect??5      $??Подключиться??6#   $??
20,95,20,0q?6:   $	??Top=?6N   $??16(?6f   
??????????	#FF050000$??40??6?   G? 5   
   -   5        	 G?Name$	 
pbPassword??5      $??
20,60,60,0q?6*   $	??Top=?6>   $??26??6V   
 G? 	MaxLength$
 18??6b   $??16(?6q    G? Password$  ??6    J? 	AllowDrop . 4  .System.Windows.Automation.AutomationProperties   HelpText$ password??6?   $??Center=?6?   ?? 5   
   -   5         ??Name$ tbInsertPassword??5      $
??Left=?6,   $??	20,60,0,0q?6G    ?? TextWrapping$
 Wrap=?6Z   $??TextBox??6n   $	??Top=?6}   $	??152??6?   $??26??6?   $??16(?6?   $y?Hidden=?6?   {?.  $??Center=?6?   5      5      