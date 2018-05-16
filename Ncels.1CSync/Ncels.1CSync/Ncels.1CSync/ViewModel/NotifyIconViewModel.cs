using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Ncels._1CSync.Annotations;
using Ncels._1CSync.Controller;

namespace Ncels._1CSync.ViewModel
{
    /// <summary>
    /// Provides bindable properties and commands for the NotifyIcon. In this sample, the
    /// view model is assigned to the NotifyIcon in XAML. Alternatively, the startup routing
    /// in App.xaml.cs could have created this view model, and assigned it to the NotifyIcon.
    /// </summary>
    public class NotifyIconViewModel : INotifyPropertyChanged
    {
        public Sync1CController _sync1CController = new Sync1CController();

        public NotifyIconViewModel()
        {
            _sync1CController.LogAction = Writelog;
        }

        /// <summary>
        /// Shows a window, if none is already open.
        /// </summary>
        public ICommand ShowWindowCommand
        {
            get
            {
                return new DelegateCommand
                {
                    CanExecuteFunc = () => Application.Current.MainWindow == null,
                    CommandAction = () =>
                    {
                        var window = new MainWindow()
                        {
                            DataContext = this
                        };
                        // window.TbLog.Text = LogMsg;

                        Application.Current.MainWindow = window;
                        // Application.Current.MainWindow.
                        Application.Current.MainWindow.Show();
                    }
                };
            }
        }

        /// <summary>
        /// Hides the main window. This command is only enabled if a window is open.
        /// </summary>
        public ICommand HideWindowCommand
        {
            get
            {
                return new DelegateCommand
                {
                    CommandAction = () => Application.Current.MainWindow.Close(),
                    CanExecuteFunc = () => Application.Current.MainWindow != null
                };
            }
        }

        public ICommand StartSyncCommand
        {
            get
            {
                return new DelegateCommand
                {
                    CommandAction = () => _sync1CController.StartSync(),
                    CanExecuteFunc = () => _sync1CController.IsTerminate
                };
            }
        }

        public ICommand StopSyncCommand
        {
            get
            {
                return new DelegateCommand
                {
                    CommandAction = () => _sync1CController.StopSync(),
                    CanExecuteFunc = () => !_sync1CController.IsTerminate
                };
            }
        }

        /// <summary>
        /// Shuts down the application.
        /// </summary>
        public ICommand ExitApplicationCommand
        {
            get
            {
                return new DelegateCommand {CommandAction = () => Application.Current.Shutdown()};
            }
        }
        
        private string _logmsg = string.Empty;
        public string LogMsg
        {
            get { return _logmsg; }
            set
            {
                OnPropertyChanged();
                if (_logmsg.Length > 5000)
                    _logmsg = string.Empty;
                _logmsg += value + "\n";
            }
        }
        public void Writelog(string msg)
        {
            LogMsg = msg;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


    /// <summary>
    /// Simplistic delegate command for the demo.
    /// </summary>
    public class DelegateCommand : ICommand
    {
        public Action CommandAction { get; set; }
        public Func<bool> CanExecuteFunc { get; set; }

        public void Execute(object parameter)
        {
            CommandAction();
        }

        public bool CanExecute(object parameter)
        {
            return CanExecuteFunc == null  || CanExecuteFunc();
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}