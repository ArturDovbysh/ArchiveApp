using System;
using System.Collections.Generic;

namespace Infrastructure.DataModels
{

    /// <summary>
    /// Represents an object where stores data about folder and its data.
    /// </summary>
    [Serializable]
    public class Folder
    {
        /// <summary>
        /// Name of the folder.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Subfolders within the folder.
        /// </summary>
        public List<Folder> SubFolders { get; set; }

        /// <summary>
        /// Files within the folder.
        /// </summary>
        public List<File> Files { get; set; }

        /// <summary>
        /// Initializes a new instance of Folder with values by default. 
        /// </summary>
        public Folder()
        {
            SubFolders = new List<Folder>();
            Files = new List<File>();
        }

        public override string ToString()
        {
            return Name;
        }

    }
}
