namespace CarSumo.VFX.Core
{
    public class MonoEnableEmitter : MonoEmitter
    {
        public override void Emit() => gameObject.SetActive(true);

        public override void Stop() => gameObject.SetActive(false);
    }
}
