using System;
using System.IO;
using System.Collections.Generic;
using Models = Infrastructure.DataModels;


namespace Infrastructure.Services.FolderReader
{
    /// <summary>
    /// Represents a simple file reader from the specific folder.
    /// </summary>
    public class FileReader : IFileReader<Models.File>
    {
        /// <summary>
        /// Returns all files from specific folder.
        /// </summary>
        /// <param name="folderPath">Folder to read files from.</param>
        /// <returns>All files from the folder.</returns>
        public IEnumerable<Models.File> GetFiles(string folderPath)
        {
            if (string.IsNullOrEmpty(folderPath) || string.IsNullOrWhiteSpace(folderPath))
                throw new ArgumentException(nameof(folderPath));

            var files = Directory.GetFiles(folderPath);

            for (int i = 0; i < files.Length; i++)
            {
                yield return new Models.File
                {
                    Name = CutName(files[i]),
                    FileData = System.IO.File.ReadAllBytes(files[i])
                };
            }
        }

        /// <summary>
        /// Return the name of the folder or file without full path.
        /// </summary>
        /// <param name="filePath">Full path to file or folder</param>
        /// <returns>Name of file of folder</returns>
        private string CutName(string filePath)
        {
            if (string.IsNullOrEmpty(filePath) || string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException(nameof(filePath));

            return filePath.Remove(0, filePath.LastIndexOf('\\') + 1);
        }
    }
    
}