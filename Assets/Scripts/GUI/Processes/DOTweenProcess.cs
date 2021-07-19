using System.Collections.Generic;
using DG.Tweening;

namespace CarSumo.GUI.Processees
{
    public abstract class DOTweenProcess : IGUIProcess
    {
        private IEnumerable<Tween> _tweens;
        
        public abstract void Init();
        public abstract void OnApplied();

        public void Apply()
        {
            _tweens = CreateTweenSection();
            OnApplied();
        }

        public void Stop()
        {
            foreach (Tween tween in _tweens)
            {
                tween.Kill(true);
            }
        }

        protected abstract IEnumerable<Tween> CreateTweenSection();
    }
}