using System.IO;
using Infrastructure.DataModels;

namespace Infrastructure.Services.FolderWriter
{
    /// <summary>
    /// Represents a writer which creates specific folder and writes to it all data within the folder.
    /// </summary>
    public class FolderWriter : IFolderWriter
    {
        /// <summary>
        /// Writes all data within the specific folder and creates it.
        /// </summary>
        /// <param name="folder">Folder to write.</param>
        /// <param name="folderPath">Full path where to write the folder.</param>
        /// <returns></returns>
        public bool WriteFolder(Folder folder, string folderPath)
        {
            if (folder == null || !(folder is Folder))
                return false;

            if (string.IsNullOrEmpty(folderPath) || string.IsNullOrWhiteSpace(folderPath))
                return false;

            string folderName = string.Concat(folderPath, "\\", folder.Name);

            Directory.CreateDirectory(folderName);

            FileWriter fileWriter = new FileWriter();

            foreach (var file in folder.Files)
            {
                fileWriter.WriteFile(file, folderName);
            }

            foreach (var fold in folder.SubFolders)
            {
                WriteFolder(fold, folderName);
            }

            return true;
        }
    }
}
