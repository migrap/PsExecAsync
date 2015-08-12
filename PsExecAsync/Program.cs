using System;
using System.Configuration;
using System.Linq;
using System.Reactive.Subjects;

namespace PsExecAsync {
    public class Program {
        private static void Main(string[] args) {
            Console.SetBufferSize(999, 999);

            var filename = ConfigurationManager.AppSettings.Get("psexec");
            var arguments = ConfigurationManager.AppSettings.Get("arguments");

            filename = Environment.ExpandEnvironmentVariables(filename);

            var configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var execAsyncSection = (PsExecAsyncSection)configuration.GetSection("psexecasync");
            var subject = new Subject<ComputerElement>();

            subject.Subscribe(Done);

            foreach(var computer in execAsyncSection.Computers.Where(x => x.ExitCode > 0)) {
                new PsExec().RunAsync(filename, arguments, computer).ContinueWith(x => subject.OnNext(x.Result));
            }

            Console.WriteLine("Press ENTER to exit");
            Console.ReadLine();
            execAsyncSection.SectionInformation.ForceSave = true;
            configuration.SaveAs("PsExecAsync.exe.config", ConfigurationSaveMode.Minimal, true);
        }

        private static void Done(ComputerElement element) {
            Console.WriteLine("Complete: {0} {1}", (object)element.EndPoint, (object)new {
                ExitCode = element.ExitCode,
                StartTime = element.StartTime,
                TotalProcessorTime = element.TotalProcessorTime
            });
        }
    }
}