using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Archiver
{
    public class ConfigurationSectionHandler : IConfigurationSectionHandler
    {
        public object Create(object parent, object configContext, XmlNode section)
        {
            switch (section.Name)
            {
                case "archiverConfig": return new ArchiverConfig(section);
                default: throw new ArgumentException("Invalid configuration section.");
            }
        }
    }

    public class ConfigurationService
    {
        private static ArchiverConfig _config;
        public static ArchiverConfig Configuration
        {
            get
            {
                return _config
                    ?? (_config = (ArchiverConfig)ConfigurationManager.GetSection("archiverConfig"))
                    ?? (_config = new ArchiverConfig());
            }
        }
    }

    public class ArchiverConfig
    {
        public IList<ArchiveJob> ArchiveJobs { get; set; }

        public ArchiverConfig()
        {
            ArchiveJobs = new List<ArchiveJob>();
        }

        public ArchiverConfig(XmlNode section)
            : this()
        {
            foreach (var pairNode in section.SelectNodes("archiveJob").Cast<XmlNode>())
            {
                try
                {
                    ArchiveJobs.Add(new ArchiveJob()
                    {
                        SourcePath = pairNode.Attributes["source"].Value,
                        ArchivePath = pairNode.Attributes["archive"].Value,
                        Filter = GetValueOrDefault(pairNode, "filter", null),
                    });
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public string GetValueOrDefault(XmlNode node, string attributeName, string defaultData)
        {
            try
            {
                return node.Attributes[attributeName].Value;
            }
            catch (Exception)
            {
                return defaultData;
            }
        }
    }

    public class ArchiveJob
    {
        public string SourcePath { get; set; }
        public string ArchivePath { get; set; }
        public string Filter { get; set; }
    }
}
