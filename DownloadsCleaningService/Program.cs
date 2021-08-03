using System;
using Topshelf;

namespace DownloadsCleaningService
{
    class Program
    {
        static void Main(string[] args)
        {
            var exitCode = HostFactory.Run(x =>
            {
                x.Service<Cleaner>(s =>
                {
                    s.ConstructUsing(cleaner => new Cleaner());
                    s.WhenStarted(cleaner => cleaner.Start());
                    s.WhenStopped(cleaner => cleaner.Stop());
                });

                x.RunAsLocalSystem();
                x.StartAutomaticallyDelayed();
                x.SetServiceName("DownloadsCleanerService");
                x.SetDisplayName("Downloads Cleaner Service");
                x.SetDescription("Checks and cleans Downloads folder of files older than a week once a day.");
            });

            int exitCodeValue = (int)Convert.ChangeType(exitCode, exitCode.GetTypeCode());
            Environment.ExitCode = exitCodeValue;
        }
    }
}
