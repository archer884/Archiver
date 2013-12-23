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
        public IList<SourceArchivePair> SourceArchivePairs { get; set; }

        public ArchiverConfig()
        {
            SourceArchivePairs = new List<SourceArchivePair>();
        }

        public ArchiverConfig(XmlNode section)
            : this()
        {
            foreach (var pairNode in section.SelectNodes("path").Cast<XmlNode>())
            {
                SourceArchivePairs.Add(new SourceArchivePair()
                {
                    SourcePath = pairNode.Attributes["source"].Value,
                    ArchivePath = pairNode.Attributes["archive"].Value,
                });
            }
        }
    }

    public class SourceArchivePair
    {
        public string SourcePath { get; set; }
        public string ArchivePath { get; set; }
    }
}
