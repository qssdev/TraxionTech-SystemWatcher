using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SystemWatcher
{
    partial class FileWatcher : ServiceBase
    {
        FileSystemWatcher watcher;
        public FileWatcher()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            watcher = new FileSystemWatcher();
            watcher.Path = @"C:\Folder1";
            watcher.Created += new FileSystemEventHandler(OnCreated);
            watcher.EnableRaisingEvents = true;

            LogAllEvents("System watcher service started.", EventLogEntryType.Information);
        }

        protected override void OnStop()
        {
            watcher.EnableRaisingEvents=false;
            LogAllEvents($"System watcher stop.", EventLogEntryType.Information);
        }

        private void OnCreated(object sender, FileSystemEventArgs e) 
        {
            string fileName = e.Name;
            string sourcePath = @"C:\Folder1";
            string targetPath = @"C:\Folder2";

            try
            {
                string sourceFile = Path.Combine(sourcePath, fileName);
                string destFile = Path.Combine(targetPath, fileName);

                File.Move(sourceFile, destFile);

                LogAllEvents($"System watcher move the {fileName} from {sourcePath} To {targetPath}.", EventLogEntryType.Information);
            }
            catch (Exception ex)
            {
                LogAllEvents($"System watcher encouter an error while moving file: {ex.Message}", EventLogEntryType.Error);
            }
        }

        private void LogAllEvents(string message, EventLogEntryType eventType)
        {
            try
            {
                // Get the path to the log file
                string logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log.txt");

                // Write the message to the log file
                using (StreamWriter writer = new StreamWriter(logPath, true))
                {
                    writer.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} - {message}");
                }
                RemoveOldLogFile();

            }
            catch (Exception ex)
            {
                EventLog.WriteEntry(AppDomain.CurrentDomain.FriendlyName, $"System watcher encounter an error while logging data. please see exception message : {ex.Message}", eventType);
            }
        }

        private void RemoveOldLogFile()
        {
            // Delete old log files if there are more than 10
            DirectoryInfo dirInfo = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            FileInfo[] logFiles = dirInfo.GetFiles("*.txt");
            if (logFiles.Length > 10)
            {
                Array.Sort(logFiles, (x, y) => y.CreationTime.CompareTo(x.CreationTime));
                for (int i = 10; i < logFiles.Length; i++)
                {
                    logFiles[i].Delete();
                }
            }
        }
    }
}
