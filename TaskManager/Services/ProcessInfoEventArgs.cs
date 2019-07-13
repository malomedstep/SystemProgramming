using System;
using System.Diagnostics;

namespace TaskManager.Services {
    public class ProcessInfoEventArgs : EventArgs {
        public ProcessInfoEventArgs(Process process) {
            Process = process;
        }

        public Process Process { get; set; }
    }
}