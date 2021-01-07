namespace WoosanStudio.ZombieShooter
{
    public interface IWeaponStat
    {
        /// <summary>
        /// 무기 레벨
        /// </summary>
        int Level { get; }

        /// <summary>
        /// 무기 종류
        /// </summary>
        int Type { get; }

        /// <summary>
        /// 무기 데이지
        /// </summary>
        int Damage { get; }
    }
}