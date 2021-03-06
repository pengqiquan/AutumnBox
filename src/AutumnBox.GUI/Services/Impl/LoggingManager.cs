﻿using AutumnBox.GUI.Util;
using AutumnBox.Leafx.Container.Support;
using AutumnBox.Logging.Management;
using System;
using System.IO;

namespace AutumnBox.GUI.Services.Impl
{
    [Component(Type = typeof(ILoggingManager))]
    sealed class LoggingManager : ILoggingManager
    {
        public ILogsCollection Logs => Logging.Management.LoggingManager.Logs;
        public DirectoryInfo LogsDirectory { get; }
        public LoggingManager(IStorageManager storageManager)
        {
            LogsDirectory  = storageManager.LogsDirectory;
        }
        public void Initialize()
        {
            var logger = new TraceLogger() + new AsyncFileLogger(OpenLogFileStream());
            Logging.Management.LoggingManager.CoreLogger = logger;
        }
        private FileStream OpenLogFileStream()
        {
            //Open file
            var logFileName = $"{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.log";
            var logFile = new FileInfo(Path.Combine(LogsDirectory.FullName, logFileName));

            return logFile.Open(FileMode.OpenOrCreate, FileAccess.Write);
        }
    }
}
