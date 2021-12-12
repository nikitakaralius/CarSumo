using System.Collections.Generic;

namespace CarSumo.DataModel.GameResources
{
    public class SerializableResources
    {
        // Installed version of Json.NET can not handle dictionaries as IReadOnlyDictionary
        // public Dictionary<ResourceId, int> Amounts { get; set; }
        // public Dictionary<ResourceId, int?> Limits { get; set; }
        public IEnumerable<ResourceAmount> Amounts { get; set; }
        public IEnumerable<ResourceLimit> Limits { get; set; }
    }
    
    public class ResourceAmount
    {
        public readonly ResourceId Id;
        public readonly int Value;

        public ResourceAmount(ResourceId id, int value)
        {
            Id = id;
            Value = value;
        }
    }

    public class ResourceLimit
    {
        public readonly ResourceId Id;
        public readonly int? Value;

        public ResourceLimit(ResourceId id, int? value)
        {
            Id = id;
            Value = value;
        }
    }
}