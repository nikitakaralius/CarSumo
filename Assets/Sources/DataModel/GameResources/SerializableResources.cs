using System.Collections.Generic;

namespace CarSumo.DataModel.GameResources
{
    public class SerializableResources
    {
        public IReadOnlyDictionary<ResourceId, int> Amounts { get; set; }
        public IReadOnlyDictionary<ResourceId, int?> Limits { get; set; }
    }
}