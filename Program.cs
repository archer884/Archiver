using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archiver
{
    class Program
    {
        static ArchiverConfig Config = ConfigurationService.Configuration;
        static void Main(string[] args)
        {
            EnsureDirectories();

            foreach (var pair in Config.ArchiveJobs)
            {
                ArchiveFiles(pair);
            }
        }

        public static void ArchiveFiles(ArchiveJob job)
        {
            foreach (var file in Directory.EnumerateFiles(job.SourcePath, job.Filter)
                .Select(file => new FileInfo(file))
                .Where(file => file.CreationTime <= DateTime.Now.Date.AddDays(-7)))
            {
                var targetDirectory = file.CreationTime.ToString("yyyyMM");
                CreateIfAbsent(Path.Combine(job.ArchivePath, targetDirectory));

                var targetPath = Path.Combine(
                    job.ArchivePath,
                    targetDirectory,
                    file.Name);

                try
                {
                    File.Move(file.FullName, targetPath);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to move file: \nSource: {0} \nArchive: {1} \nFile: {2}",
                        job.SourcePath,
                        job.ArchivePath,
                        file);
                    Console.WriteLine(e.Message);
                }
            }
        }

        public static void EnsureDirectories()
        {
            foreach (var pair in Config.ArchiveJobs)
            {
                if (!Directory.Exists(pair.SourcePath))
                {
                    throw new ArgumentException(string.Format("Invalid directory: {0}", pair.SourcePath));
                }

                if (!Directory.Exists(pair.ArchivePath))
                {
                    throw new ArgumentException(string.Format("Invalid directory: {0}", pair.ArchivePath));
                }

                CreateIfAbsent(Path.Combine(pair.ArchivePath, DateTime.Now.ToString("yyyyMM")));
            }
        }

        public static void CreateIfAbsent(string path)
        {
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        }
    }
}
