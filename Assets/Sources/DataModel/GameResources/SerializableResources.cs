using System.Collections.Generic;

namespace CarSumo.DataModel.GameResources
{
    public class SerializableResources
    {
        // Installed version of Json.NET can not handle dictionaries as IReadOnlyDictionary
        public Dictionary<ResourceId, int> Amounts { get; set; }
        public Dictionary<ResourceId, int?> Limits { get; set; }
    }
}