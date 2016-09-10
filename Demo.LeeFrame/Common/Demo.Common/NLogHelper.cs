using System;
using System.Text;
using NLog;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;

namespace Demo.Common
{
    public sealed class NLogHelper
    {
        static NLogHelper()
        {
            //can edit
            NLogConfig.Instance.ConfigBySize();
        }

        public static void Debug(string message)
        {
            var logger = LogManager.GetCurrentClassLogger();
            RecordDebug(logger, message);
        }

        public static void Debug(Type type, string message)
        {
            var logger = LogManager.GetLogger(type.FullName);
            RecordDebug(logger, message);
        }

        private static void RecordDebug(Logger logger,string message)
        {
            logger.Debug(message);
        }
        private static void RecordWarn(Logger logger, string message)
        {
            logger.Warn(message);
        }
        private static void RecordError(Logger logger, string message)
        {
            logger.Error(message);
        }
        private static void RecordFatal(Logger logger, string message)
        {
            logger.Fatal(message);
        }

        public static void Warn(string message, Type type = null)
        {
            var logger = (type == null) ? LogManager.GetCurrentClassLogger() : LogManager.GetLogger(type.FullName);

            RecordWarn(logger, message);
        }

        public static void Error(string message, Type type = null)
        {
            var logger = (type == null) ? LogManager.GetCurrentClassLogger() : LogManager.GetLogger(type.FullName);
            
            RecordError(logger, message);
        }

        public static void Fatal(string message, Type type = null)
        {
            var logger = (type == null) ? LogManager.GetCurrentClassLogger() : LogManager.GetLogger(type.FullName);

            RecordFatal(logger, message);
        }

    }

    public sealed class NLogConfig
    {
        private static readonly NLogConfig _instance = new NLogConfig();
        private NLogConfig()
        {

        }
        static NLogConfig()
        {

        }

        public static NLogConfig Instance
        {
            get { return _instance; }
        }

        public void ConfigByDateTime()
        {
            var config = new LoggingConfiguration();
            var fileTarget = new FileTarget()
            {
                Name = "TimeBasedArchive",
                Layout = "${longdate} ${level} ${logger} [ ${message} ]",
                FileName = "${basedir}/logs/${level}.txt",
                ArchiveFileName = "${basedir}/archives/${level}.{###}.txt",
                ArchiveEvery = FileArchivePeriod.Day,
                ArchiveNumbering = ArchiveNumberingMode.Rolling,
                MaxArchiveFiles = 100,
                ConcurrentWrites = true,
                KeepFileOpen = false,
                Encoding = Encoding.UTF8
            };
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, fileTarget));
            LogManager.Configuration = config;
        }

        public void ConfigBySize(long maxByteLen = 512000)
        {
            var config = new LoggingConfiguration();
            var fileTarget = new FileTarget()
            {
                Name = "SizeBasedArchive",
                Layout = "${longdate} ${level} ${logger} [ ${message} ]",
                FileName = "${basedir}/logs/${level}.txt",
                ArchiveFileName = "${basedir}/archives/${level}.{###}.txt",
                ArchiveAboveSize = maxByteLen,
                ArchiveNumbering = ArchiveNumberingMode.Sequence,
                MaxArchiveFiles = 100,
                ConcurrentWrites = true,
                KeepFileOpen = false,
                Encoding = Encoding.UTF8
            };
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, fileTarget));
            LogManager.Configuration = config;
        }

        public void ConfigByCSV(long maxByteLen = 512000)
        {
            var config = new LoggingConfiguration();
            var fileTarget = new FileTarget()
            {
                Name = "SizeBasedArchive",
                Layout =
                    new CsvLayout()
                    {
                        CustomColumnDelimiter = ",",
                        Header = "time,message,logger,level",
                        //QuoteChar=",",
                        Quoting = CsvQuotingMode.Auto,
                        Delimiter = CsvColumnDelimiterMode.Custom,
                        WithHeader = true,
                        Layout = "${longdate},${message},${logger},${level}"
                    },
                FileName = "${basedir}/logs/file.csv",
                ArchiveFileName = "${basedir}/archives/log.{###}.txt",
                ArchiveAboveSize = maxByteLen,
                ArchiveNumbering = ArchiveNumberingMode.Sequence,
                MaxArchiveFiles = 100,
                ConcurrentWrites = true,
                KeepFileOpen = false,
                Encoding = Encoding.UTF8
            };
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, fileTarget));
            LogManager.Configuration = config;
        }

    }

}
