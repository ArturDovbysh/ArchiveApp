using System;
using System.IO;

namespace Infrastructure.Services.FolderWriter
{
    /// <summary>
    /// Represents a writer which writes data from File object to phisical file.
    /// </summary>
    public class FileWriter : IFileWriter
    {
        /// <summary>
        /// Writes data from FileData byte array to specific file.
        /// </summary>
        /// <param name="file">Specific File object to write.</param>
        /// <param name="folderPath">Full path to folder where to write new file.</param>
        public void WriteFile(DataModels.File file, string folderPath)
        {
            if (file == null || !(file is Infrastructure.DataModels.File))
                throw new ArgumentException(nameof(file));

            if (string.IsNullOrEmpty(folderPath) || string.IsNullOrWhiteSpace(folderPath))
                throw new ArgumentException(nameof(folderPath));

            string fileName = string.Concat(folderPath, "\\", file.Name);

            using (var fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                fileStream.Write(file.FileData, 0, file.FileData.Length);
            }
        }
    }
}
