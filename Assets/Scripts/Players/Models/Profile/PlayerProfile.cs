using System.Collections.Generic;
using DataManagement.Resources;
using UnityEngine;

namespace CarSumo.Players.Models
{
    public class PlayerProfile
    {
        public string Name { get; }
        public Sprite Icon { get; }

        public IReadOnlyDictionary<ResourceId, int> Resources { get; }

        public PlayerProfile(string name, Sprite icon, IReadOnlyDictionary<ResourceId, int> resources)
        {
            Name = name;
            Icon = icon;
            Resources = resources;
        }
    }
}