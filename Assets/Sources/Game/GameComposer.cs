using AI;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game
{
	public class GameComposer : MonoBehaviour
	{
		[SerializeField, Required, AssetsOnly] private AIPlayer _ai;
	}
}