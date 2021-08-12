using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
	public interface IReadOnlyDragHandlerData
	{
		Transform ContentParent { get; }
		Transform DraggingParent { get; }
		LayoutGroup ContentLayoutGroup { get; }
	}
	
	[System.Serializable]
	public struct DragHandlerData : IReadOnlyDragHandlerData
	{
		[SerializeField] private Transform _contentParent;
		[SerializeField] private Transform _draggingParent;
		[SerializeField] private LayoutGroup _contentLayoutGroup;

		public Transform ContentParent => _contentParent;
		public Transform DraggingParent => _draggingParent;
		public LayoutGroup ContentLayoutGroup => _contentLayoutGroup;
	}
}