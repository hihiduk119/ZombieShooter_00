namespace WoosanStudio.ZombieShooter
{
    public interface IProjectileLauncher : IProjectileLauncherEvents
    {
        /// <summary>
        /// 발사 시스템 세팅
        /// </summary>
        ProjectileLauncher ProjectileLauncher { get; set; }

        /// <summary>
        /// 어떤 총인지에 대한 세팅값
        /// </summary>
        //GunSettings GunSettings { get; set; }

        /// <summary>
        /// 발사 런처의 액션을 가져옴
        /// </summary>
        /// <returns></returns>
        IProjectileLauncherEvents GetProjectileLauncherEvents();

        /// <summary>
        /// 재장전
        /// </summary>
        void ReloadAmmo();

        /// <summary>
        /// 탄 사용
        /// </summary>
        void UseAmmo();

        /// <summary>
        /// 발사 시작
        /// </summary>
        void Fire();

        /// <summary>
        /// 발사 정지
        /// </summary>
        void Stop();
        

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //IInputActions GetInput();
    }
}