using System;
using System.IO;
using System.Linq;

namespace HelpersProject.Helpers
{
    public class FileHelper
    {
        public string GetPathToSolutionRoot()
        {
            var directoryPath = AppDomain.CurrentDomain.BaseDirectory;
            while (!Directory.GetFiles(directoryPath, "*.sln", SearchOption.TopDirectoryOnly).Any())
                directoryPath = Path.GetFullPath(directoryPath+ "..//");

            if (!directoryPath.EndsWith(@"/"))
                directoryPath += @"/";

            return directoryPath;
        }

    }
}