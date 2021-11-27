using CarSumo.Input;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CarSumo.Cameras
{
	public class CameraInput : MonoBehaviour
	{
		[SerializeField, Required, SceneObjectsOnly] private SwipeInputScreen _inputScreen;

		public ISwipeScreen Events => _inputScreen;
	}
}