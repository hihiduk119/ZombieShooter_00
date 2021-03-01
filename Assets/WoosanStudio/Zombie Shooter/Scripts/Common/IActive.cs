namespace WoosanStudio.ZombieShooter.Common
{
    /// <summary>
    /// 활성 비활성이 필요할때 사용
    /// </summary>
    public interface IActive
    {
        bool Activate { get; set; }
    }
}
