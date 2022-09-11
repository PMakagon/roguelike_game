using Cysharp.Threading.Tasks;

namespace LiftGame.GameCore.ScenesLoading
{
    public interface ILoadingOperation
    {
        UniTask Load();
        UniTask Unload();
    }
}