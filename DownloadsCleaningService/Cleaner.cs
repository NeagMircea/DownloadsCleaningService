using Syroot.Windows.IO;
using System;
using System.IO;
using System.Timers;

namespace DownloadsCleaningService
{
    public class Cleaner
    {
        private readonly Timer timer;

        public Cleaner()
        {
            timer = new Timer(1000 * 60 * 60 * 24) { AutoReset = true };
            timer.Elapsed += OnTimeElapsed;
        }

        private void OnTimeElapsed(object sender, ElapsedEventArgs e)
        {
            KnownFolder downloads = new KnownFolder(KnownFolderType.Downloads);
            string[] files = Directory.GetFiles(downloads.Path);

            foreach (string file in files)
            {
                TimeSpan timeSpan = DateTime.Now - File.GetCreationTime(file);

                if (timeSpan.Days >= 7)
                {
                    File.Delete(file);
                }
            }
        }

        public void Start()
        {
            timer.Start();
        }

        public void Stop()
        {
            timer.Stop();
        }
    }
}
