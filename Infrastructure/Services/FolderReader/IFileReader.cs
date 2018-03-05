using System.Collections.Generic;
using Infrastructure.DataModels;

namespace Infrastructure.Services.FolderReader
{
    public interface IFileReader<T>
    {
        IEnumerable<T> GetFiles(string folderPath);
    }
}
