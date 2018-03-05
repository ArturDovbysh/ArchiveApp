using System;
using System.Linq;
using System.IO;
using Infrastructure.DataModels;


namespace Infrastructure.Services.FolderReader
{
    /// <summary>
    /// Represents a simple folder reader from the specific path.
    /// </summary>
    public class FolderReader : IFolderReader
    {
        private IFileReader<DataModels.File> fileReader;

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

        /// <summary>
        /// Recursive function to get all folder/subfolders tree.
        /// </summary>
        /// <param name="path">Full path to specific folder</param>
        /// <returns>Folder object with all subfolder within the folder.</returns>
        public Folder GetFolders(string path)
        {
            if (string.IsNullOrEmpty(path) || string.IsNullOrWhiteSpace(path))
                throw new ArgumentException(nameof(path));

            Folder folder = new Folder();
            folder.Name = CutName(path);
            folder.Files = fileReader.GetFiles(path).ToList<DataModels.File>();

            var subDirs = Directory.GetDirectories(path);

            if (subDirs.Length == 0)
                return folder;

            foreach (var dir in subDirs)
            {
                var fold = GetFolders(dir);
                folder.SubFolders.Add(fold);
            }

            return folder;
        }

        /// <summary>
        /// Initializes a new instance of FolderReader with values by default. 
        /// </summary>
        public FolderReader()
        {
            fileReader = new FileReader();
        }      
    }
}
