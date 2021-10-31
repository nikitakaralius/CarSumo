namespace DataModel.GameData.Resources
{
    public interface IInitialResourceStorageProvider
    {
        GameResourceStorage GetInitialStorage();
    }
}