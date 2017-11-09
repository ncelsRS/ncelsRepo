using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Hardcodet.Wpf.TaskbarNotification;
using log4net.Config;
using Ncels._1CSync.ViewModel;

namespace Ncels._1CSync
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private TaskbarIcon notifyIcon;
        
        public App()
        {
            try
            {
                XmlConfigurator.Configure();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Application.Current.MainWindow = null;
            notifyIcon = (TaskbarIcon)FindResource("NotifyIcon");
            if (notifyIcon != null)
            {
                var notify = (NotifyIconViewModel)notifyIcon.DataContext;
                if (notify.StartSyncCommand.CanExecute(null))
                    notify.StartSyncCommand.Execute(null);
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            notifyIcon.Dispose(); //the icon would clean up automatically, but this is cleaner
            base.OnExit(e);
        }
    }
}
