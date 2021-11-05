using GameModes;
using UnityEngine;

namespace Game.Mediation
{
	public interface IMediator
	{
		void Boot(GameMode mode);
	}
	
	public class GameMediator : MonoBehaviour, IMediator
	{
		
	}
}