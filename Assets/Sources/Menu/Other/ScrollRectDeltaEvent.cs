using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Sources.Menu.Other
{
	public class ScrollRectDeltaEvent : MonoBehaviour
	{
		[SerializeField] private ScrollRect _scrollRect;
		[SerializeField, BoxGroup] private Vector2 _deltaToInvokeEvent;
		[SerializeField, BoxGroup] private UnityEvent<Vector2> _valueChanged;

		private Vector2 _previous = Vector2.zero;
		
		private void OnEnable()
		{
			_scrollRect.onValueChanged.AddListener(OnValueChanged);
		}

		private void OnDisable()
		{
			_scrollRect.onValueChanged.RemoveListener(OnValueChanged);
		}

		private void OnValueChanged(Vector2 delta)
		{
			if (Mathf.Abs(delta.x - _previous.x) < _deltaToInvokeEvent.x)
			{
				return;
			}
			if (Mathf.Abs(delta.y - _previous.y) < _deltaToInvokeEvent.y)
			{
				return;
			}
			_previous = delta;
			_valueChanged.Invoke(delta);
		}
	}
}