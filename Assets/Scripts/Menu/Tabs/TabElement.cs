using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.Tabs
{
	public class TabElement : MonoBehaviour
	{
		[Header("Required Components")]
		[SerializeField] private TMP_Text _title;
		[SerializeField] private Button _button;
		[SerializeField] private GameObject _tabFocus;
		[SerializeField] private GameObject _associatedWindow;

		[Header("Preferences")] 
		[SerializeField] private Color _selectedColor = Color.white;
		[SerializeField] private Color _defaultColor = Color.white;
		[SerializeField] private bool _selectedOnStart = false;

		private readonly Subject<TabElement> _selectedObserver = new Subject<TabElement>();
		
		private void OnValidate() => SetSelected(_selectedOnStart);

		private void Start()
		{
			_button.onClick.AddListener(() =>
			{
				SetSelected(true);
				_selectedObserver.OnNext(this);
			});
		}

		public void SetSelected(bool selected)
		{
			_title.color = selected ? _selectedColor : _defaultColor;
			_tabFocus.gameObject.SetActive(selected);
			_associatedWindow.gameObject.SetActive(selected);
		}

		public IObservable<TabElement> ObserveSelected() => _selectedObserver;
	}
}