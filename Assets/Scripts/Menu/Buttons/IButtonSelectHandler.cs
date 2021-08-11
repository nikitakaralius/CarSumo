namespace Menu.Buttons
{
	public interface IButtonSelectHandler<in T>
	{
		void OnButtonSelected(T element);
		void OnButtonDeselected(T element);
	}
}