using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 시작 중간 끝상황의 이벤트가 필요할떼 사용
    /// 중간은 정확하게가 아닌 중간즈음임.
    /// </summary>
    public interface ISequential : IStart , IEnd
    {
        UnityEvent MidpointEvent { get; }
    }
}