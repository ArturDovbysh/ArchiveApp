using Infrastructure.DataModels;

namespace Infrastructure.Services.FolderReader
{
    public interface IFolderReader
    {
        Folder GetFolders(string folderPath);
    }
}
