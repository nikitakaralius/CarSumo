using DataManagement.Players.Models;
using UnityEngine;

namespace DataManagement.Players.Services
{
    public interface IPlayerConstruct
    {
        Sprite GetIcon(Player player);
    }
}