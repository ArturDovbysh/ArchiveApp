using System;

namespace Infrastructure.DataModels
{
    /// <summary>
    /// Represents an object where stores data about phisical file.
    /// </summary>
    [Serializable]
    public class File
    {
        /// <summary>
        /// Name of the file.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Data stored in the file.
        /// </summary>
        public byte[] FileData { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
