using System;

namespace CarSumo.Teams
{
    public interface ITeamChangeSender
    {
        event Action ChangeSent;
    }
}