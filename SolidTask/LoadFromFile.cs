using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SolidTask.Interfaces;

namespace SolidTask
{
    public class LoadFromFile : ILoader<IEnumerable<string>>
    {
        private string filePath;

        public LoadFromFile(string filePath)
        {
            if (filePath is null)
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException();
            }

            this.filePath = filePath;
        }

        public IEnumerable<string> Load()
        {
            using var reader = new StreamReader(this.filePath);

            while (!reader.EndOfStream)
            {
                yield return reader.ReadLine();
            }
        }
    }
}
