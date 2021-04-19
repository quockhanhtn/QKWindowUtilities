using QKWU.App.Views;
using System.Windows;

namespace QKWU.App
{
   /// <summary>
   /// Interaction logic for App.xaml
   /// </summary>
   public partial class App : Application
   {
      private System.Windows.Forms.NotifyIcon notifyIcon;
      private bool isExitedApp;

      protected override void OnStartup(StartupEventArgs e)
      {
         base.OnStartup(e);
         MainWindow = new MainWindow();
         MainWindow.Closing += MainWindow_Closing;

         notifyIcon = new System.Windows.Forms.NotifyIcon
         {
            Icon = QKWU.App.Properties.Resources.qk,
            Visible = true
         };
         notifyIcon.DoubleClick += (s, args) => ShowMainWindow();

         CreateContextMenu();
      }

      private void CreateContextMenu()
      {
         notifyIcon.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
         notifyIcon.ContextMenuStrip.Items.Add("MainWindow...").Click += (s, e) => ShowMainWindow();
         notifyIcon.ContextMenuStrip.Items.Add("Exit").Click += (s, e) => ExitApplication();
      }

      private void ExitApplication()
      {
         isExitedApp = true;
         MainWindow.Close();
         notifyIcon.Dispose();
         notifyIcon = null;
      }

      private void ShowMainWindow()
      {
         if (MainWindow.IsVisible)
         {
            if (MainWindow.WindowState == WindowState.Minimized)
            {
               MainWindow.WindowState = WindowState.Normal;
            }
            MainWindow.Activate();
         }
         else
         {
            MainWindow.Show();
         }
      }

      private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
      {
         if (!isExitedApp)
         {
            e.Cancel = true;
            MainWindow.Hide(); // A hidden window can be shown again, a closed one not
         }
      }
   }
}