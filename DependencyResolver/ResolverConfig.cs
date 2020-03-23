using System;
using Bll.Contract.Entities;
using Bll.Contract.Entities.XmlSerializationModel;
using Bll.Contract.Interfaces;
using Bll.Implementation.ServiceImplementation;
using Dal.Contract.Interafces;
using Dal.Implementation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.IO;
using NLog;
using NLog.Extensions.Logging;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.Extensions.Logging;

namespace DependencyResolver
{
    public class ResolverConfig
    {
        public static IConfigurationRoot ConfigurationRoot { get; } =
            new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        public IServiceProvider CreateServiceProvider()
        {
            string sourceFilePath = CreateValidPath("InputFile") ?? throw new ArgumentNullException("CreateValidPath(\"sourceFilePath\")");
            string targetFilePath = CreateValidPath("OutputFile") ?? throw new ArgumentNullException("CreateValidPath(\"targetFilePath\")");

            string regexPattern = @"(?<Protocol>\w+):\/\/(?<Domain>[\w@][\w.:@]+)\/?[\w\.?=%&=\-@/$,]*";

            return new ServiceCollection()
                .AddSingleton(new FileLoggerProvider().CreateLogger(null))
                .AddSingleton<ILoader<IEnumerable<string>>>(new LoadFromFile(sourceFilePath))
                .AddSingleton<IParser<Bll.Contract.Entities.Url>, UrlParser>()
                .AddSingleton<IValidator<string>>(new UrlValidator(regexPattern))
                .AddSingleton<IConverter<IEnumerable<string>, XDocument>, UrlToXDocumentConverter>()
                .AddSingleton<ISaver<XDocument>>(new XmlFileSaver(targetFilePath))
                .AddSingleton<IService, UrlToXmlService<IEnumerable<string>, XDocument>>()
                .BuildServiceProvider();
        }

        private string CreateValidPath(string path) =>
            Path.Combine(Directory.GetCurrentDirectory(), ConfigurationRoot[path]);

        private class FileLoggerProvider : ILoggerProvider
        {
            public Microsoft.Extensions.Logging.ILogger CreateLogger(string categoryName)
            {
                return new FileLogger();
            }

            public void Dispose()
            {
            }

            private class FileLogger : Microsoft.Extensions.Logging.ILogger
            {
                public IDisposable BeginScope<TState>(TState state)
                {
                    return null;
                }

                public bool IsEnabled(Microsoft.Extensions.Logging.LogLevel logLevel)
                {
                    return true;
                }

                public void Log<TState>(Microsoft.Extensions.Logging.LogLevel logLevel, EventId eventId,
                    TState state, Exception exception, Func<TState, Exception, string> formatter)
                {
                    File.AppendAllText("log.txt", formatter(state, exception));
                    Console.WriteLine(formatter(state, exception));
                }
            }
        }
    }
}
