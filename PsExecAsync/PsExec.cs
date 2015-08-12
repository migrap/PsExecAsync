using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PsExecAsync {
    public class PsExec {
        public Task<ComputerElement> RunAsync(string filename, string arguments, ComputerElement computer) {
            var taskCompletionSource = new TaskCompletionSource<ComputerElement>();
            var processStartInfo = new ProcessStartInfo(filename) {
                UseShellExecute = false,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
                FileName = filename,
                Arguments = string.Format("\\\\{0} {1}", computer.EndPoint, arguments)
            };

            Console.WriteLine("Starting: {0} {1}", computer.EndPoint, arguments);

            var process = new Process() {
                StartInfo = processStartInfo,
                EnableRaisingEvents = true
            };

            process.Exited += (sender, args) => {
                var standardOutput = process.StandardOutput;
                var standardError = process.StandardError;
                computer.Output = standardOutput.ReadToEnd().Replace("\r\n", ";");
                computer.Error = standardError.ReadToEnd().Replace("\r\n", ";");
                computer.ExitCode = process.ExitCode;
                computer.StartTime = process.StartTime;
                computer.TotalProcessorTime = process.TotalProcessorTime;
                taskCompletionSource.SetResult(computer);
                process.Dispose();
            };

            process.Start();

            return taskCompletionSource.Task;
        }
    }
}