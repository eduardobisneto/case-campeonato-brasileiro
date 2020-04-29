using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.IO;

namespace CampeonatoBrasileiroAPI.Helpers
{
    public class CustomLogger : ILogger
    {
        readonly string loggerName;
        readonly CustomLoggerProviderConfiguration loggerConfiguration;

        public CustomLogger(string name, CustomLoggerProviderConfiguration config)
        {
            loggerName = name;
            loggerConfiguration = config;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            throw new NotImplementedException();
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            string mensagem = string.Format("{0} - {1}: {2} - {3}", DateTime.Now.ToString(), logLevel.ToString(), eventId.Id, formatter(state, exception));
            Log(mensagem);
        }

        public void Log(string mensagem)
        {
            string caminhoArquivo = string.Format(@"log\{0}_log.txt", DateTime.Now.ToString("ddMMyyyy"));
            using (StreamWriter streamWriter = new StreamWriter(caminhoArquivo, true))
            {
                streamWriter.WriteLine(mensagem);
                streamWriter.Close();
            }
        }
    }

    public class CustomLoggerProviderConfiguration
    {
        public LogLevel LogLevel { get; set; } = LogLevel.Information;
    }

    public class CustomLoggerProvider : ILoggerProvider
    {
        readonly CustomLoggerProviderConfiguration config;
        readonly ConcurrentDictionary<string, CustomLogger> loggers = new ConcurrentDictionary<string, CustomLogger>();

        public CustomLoggerProvider(CustomLoggerProviderConfiguration _config)
        {
            config = _config;
        }

        public ILogger CreateLogger(string category)
        {
            return loggers.GetOrAdd(category, name => new CustomLogger(name, config));
        }

        public void Dispose()
        {
        }
    }
}
