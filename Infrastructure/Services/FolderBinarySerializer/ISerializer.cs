namespace Infrastructure.Services.FolderBinarySerializer
{
    public interface ISerializer
    {
        bool Serialize(object obj, string filePath);
        object Deserialize(string filePath);
    }
}
