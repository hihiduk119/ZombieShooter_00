namespace WoosanStudio.ZombieShooter
{
    public interface IProjectileLauncher
    {
        ProjectileLauncher ProjectileLauncher { get; set; }

        /// <summary>
        /// 재장전
        /// </summary>
        void ReloadAmmo();

        /// <summary>
        /// 탄 사용
        /// </summary>
        void UseAmmo();

        /// <summary>
        /// 총의 세팅 값
        /// </summary>
        GunSettings GunSettings { get; set; }
    }
}