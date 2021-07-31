using System.Collections.Generic;

namespace CarSumo.DataModel.GameResources
{
    public class SerializableResources
    {
        public IReadOnlyDictionary<ResourceId, (int, int?)> Storage { get; set; }
    }
}