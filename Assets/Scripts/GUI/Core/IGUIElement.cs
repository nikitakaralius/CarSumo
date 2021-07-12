namespace CarSumo.GUI.Core
{
    public interface IGUIElement
    {
        void Process();
        void Stop();
    }

    public class EmptyGUIElement : IGUIElement
    {
        public void Process()
        {
            
        }

        public void Stop()
        {
        }
    }
}