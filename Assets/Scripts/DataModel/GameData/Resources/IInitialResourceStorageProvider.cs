using CarSumo.DataModel.GameResources;

namespace DataModel.GameData.Resources
{
    public interface IInitialResourceStorageProvider
    {
        GameResourceStorage GetInitialStorage();
    }
}