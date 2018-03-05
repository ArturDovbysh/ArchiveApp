using Infrastructure.DataModels;

namespace Infrastructure.Services.FolderWriter
{
    public interface IFileWriter
    {
        void WriteFile(File file, string folderPath);
    }
}
