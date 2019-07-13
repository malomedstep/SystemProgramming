using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;



namespace TaskManager.ViewModel {
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel() {
            RunNewTaskCommand = new RelayCommand(OnRunNewTaskCommand);
            ExitCommand = new RelayCommand(OnExitCommand);


            WqlEventQuery query =
                new WqlEventQuery("__InstanceCreationEvent",
                new TimeSpan(0, 0, 1),
                "TargetInstance isa \"Win32_Process\"");

            ManagementEventWatcher watcher =
                new ManagementEventWatcher();
            watcher.Query = query;
            watcher.Options.Timeout = new TimeSpan(0, 0, 5);
            watcher.EventArrived += (s, e) => {
                // MessageBox.Show(((ManagementBaseObject)e.NewEvent["TargetInstance"])["ProcessId"].ToString(), "");
            };
            //watcher.Start();

            //  watcher.Stop();
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e) {
            var a = new Process();
            a.GetHashCode();

            var procs = Process.GetProcesses();
            var procsToSkip = procs.Where(p => Processes.Any(pr => pr.Process.Id == p.Id));
            var procsToAdd = procs.Where(p => !Processes.Any(pr => pr.Process.Id == p.Id));
        }

        private Timer _timer;

        private void OnExitCommand() {
            Application.Current.Shutdown();
        }

        private void OnRunNewTaskCommand() {
            var shell = new Shell32.Shell();
            shell.FileRun();
        }

        public ObservableCollection<ProcessInfo> Processes { get; } = new ObservableCollection<ProcessInfo>();

        public ICommand RunNewTaskCommand { get; }
        public ICommand ExitCommand { get; }

    }
}