using System.Diagnostics;
using GalaSoft.MvvmLight;

namespace TaskManager.ViewModel {
    public class ProcessInfo : ObservableObject {
        public Process Process { get; set; }
        public int MyProperty { get; set; }
        public PerformanceCounter CpuCounter { get; set; }
        public PerformanceCounter RamCounter { get; set; }
    }
}