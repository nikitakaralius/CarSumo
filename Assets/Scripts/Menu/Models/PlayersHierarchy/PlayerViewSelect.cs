namespace CarSumo.Menu.Models
{
    public class PlayerViewSelect : IPlayerViewSelect
    {
        private readonly IPlayerSelect _wrappedSelect;
        private PlayerViewItem _selected = null;
        
        public PlayerViewSelect(IPlayerSelect wrappedSelect)
        {
            _wrappedSelect = wrappedSelect;
        }

        public void MakePlayerSelected(PlayerViewItem newSelected)
        {
            _wrappedSelect.MakePlayerSelected(newSelected.Profile);

            if (_selected != null)
            {
                _selected.Highlight.MakeRegular();
            }
            
            newSelected.Highlight.MakeSelected();
            _selected = newSelected;
        }
    }
}