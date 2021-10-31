using CarSumo.DataModel.GameResources;

namespace DataModel.GameData.Resources.Binding
{
    public interface IResourceStorageBinding
    {
        GameResourceStorage BindFrom(SerializableResources resources);
    }
}