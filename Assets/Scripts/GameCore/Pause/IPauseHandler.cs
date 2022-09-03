namespace LiftGame.GameCore.Pause
{
    public interface IPauseHandler
    {
        public bool IsPaused { get; }
        public void Register(IPauseable pauseable);
        public void UnRegister(IPauseable pauseable);
        public void SetPaused(bool isPaused);
    }
}