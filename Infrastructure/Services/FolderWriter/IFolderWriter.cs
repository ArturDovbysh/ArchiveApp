using Infrastructure.DataModels;

namespace Infrastructure.Services.FolderWriter
{
    public interface IFolderWriter
    {
        bool WriteFolder(Folder folder, string folderPath);
    }
}
