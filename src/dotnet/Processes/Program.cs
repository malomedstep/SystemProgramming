using System;
using System.Diagnostics;
using System.IO;

namespace Processes {
    class Program {
        static void StartProcess() {
            Console.WriteLine("Enter process name: ");
            var name = Console.ReadLine();
            try {
                var proc = Process.Start(name);
                proc.EnableRaisingEvents = true;
                proc.Exited += (s, e) => {
                    Console.WriteLine($"Process exited with code {proc.ExitCode}");
                };
            } catch (Exception ex) {
                Console.WriteLine($"Failed to start a process {name}");
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }

        static void GetAllProcesses() {
            var procs = Process.GetProcesses();
            foreach (var proc in procs) {
                Console.WriteLine(proc.ProcessName);
            }
        }

        static void FindAndKillProcess() {
            Console.WriteLine("Enter process name: ");
            var name = Console.ReadLine();
            var processes = Process.GetProcessesByName(name);
            foreach (var proc in processes) {
                try {
                    proc.Kill();
                    Console.WriteLine($"Process {proc.ProcessName} with PID {proc.Id} terminated");
                } catch (Exception ex) {
                    Console.WriteLine($"Failed to terminate process {proc.ProcessName} with PID {proc.Id}");
                    Console.WriteLine($"Exception: {ex.Message}");
                }
            }
        }

        static void RestartProcess() {
            var proc = Process.Start(Environment.CurrentDirectory + "/../../../slaveapp/bin/debug/slaveapp.exe");
            proc.EnableRaisingEvents = true;

            proc.Exited += (s, e) => {
                RestartProcess();
            };
        }

        static void Main(string[] args) {
            var separator = new string('=', 20);

            Console.WriteLine("To call 'StartProcess' press Enter...");
            Console.ReadLine();
            StartProcess();

            Console.WriteLine(separator);
            Console.WriteLine("To call 'GetAllProcesses' press Enter...");
            Console.ReadLine();
            GetAllProcesses();

            Console.WriteLine(separator);
            Console.WriteLine("To call 'FindAndKillProcess' press Enter...");
            Console.ReadLine();
            FindAndKillProcess();

            Console.WriteLine(separator);
            Console.WriteLine("To call 'RestartProcess' press Enter...");
            Console.ReadLine();
            RestartProcess();

            Console.WriteLine(separator);
            Console.ReadLine();

        }
    }
}
