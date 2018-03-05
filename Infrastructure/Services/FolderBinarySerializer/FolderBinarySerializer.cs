using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Infrastructure.DataModels;

namespace Infrastructure.Services.FolderBinarySerializer
{
    /// <summary>
    /// Represents a folder object to binary format and vice versa serializer.
    /// </summary>
    public class FolderBinarySerializer : ISerializer
    {
        /// <summary>
        /// Serializes the folder object to specific binary file. 
        /// </summary>
        /// <param name="obj">Object to serialize.</param>
        /// <param name="filePath">Full file path to serializer in.</param>
        /// <returns>True if serialization done correct, otherwise - false.</returns>
        public bool Serialize(object obj, string filePath)
        {
            if (obj == null || !(obj is Folder))
                return false;

            if (string.IsNullOrEmpty(filePath) || string.IsNullOrWhiteSpace(filePath))
                return false;

            BinaryFormatter binFormatter = new BinaryFormatter();

            using (var fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                binFormatter.Serialize(fileStream, (obj as Folder));
            }

            return true;
        }

        /// <summary>
        /// Deserializes specific binary file to the folder object.
        /// </summary>
        /// <param name="filePath">Full file path to deserialize from.</param>
        /// <returns>Folder object.</returns>
        public object Deserialize(string filePath)
        {
            if (string.IsNullOrEmpty(filePath) || string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException(nameof(filePath));

            BinaryFormatter binFormatter = new BinaryFormatter();

            object result = null;

            using (var fileStream = new FileStream(filePath, FileMode.Open))
            {
                result = binFormatter.Deserialize(fileStream);

                if (!(result is Folder))
                    return false;
            }
            return result;
        }
    }
}
