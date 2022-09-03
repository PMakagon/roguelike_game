using System.Collections.Generic;

namespace LiftGame.GameCore.Pause
{
    public class PauseHandler : IPauseHandler

    {
    private readonly List<IPauseable> _pauseables = new List<IPauseable>();
    public bool IsPaused { get; private set; }

    public void Register(IPauseable pauseable)
    {
        _pauseables.Add(pauseable);
    }

    public void UnRegister(IPauseable pauseable)
    {
        _pauseables.Remove(pauseable);
    }

    public void SetPaused(bool isPaused)
    {
        IsPaused = isPaused;
        foreach (var pauseable in _pauseables)
        {
            pauseable.SetPaused(isPaused);
        }
    }
    }
}