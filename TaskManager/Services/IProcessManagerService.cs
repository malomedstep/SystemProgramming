using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Services {
    public interface IProcessManagerService {
        event EventHandler<ProcessInfoEventArgs> ProcessStarted;

        bool TerminateProcess(int processId);

    }
}
