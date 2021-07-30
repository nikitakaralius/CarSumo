using System.Collections.Generic;

namespace CarSumo.DataModel.Players
{
    public class SerializablePlayerStorage
    {
        public IEnumerable<SerializablePlayer> AllPlayers { get; set; }
        
        public SerializablePlayer ActivePlayer { get; set; }
    }
}