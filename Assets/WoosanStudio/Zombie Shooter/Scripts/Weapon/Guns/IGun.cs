﻿namespace WoosanStudio.ZombieShooter
{
    public interface IGun : IReloadEvent, IGunSettings
    {
        bool UseLaserPoint { get; set; }
        //IGunStat GetGunStat();
        ProjectileLauncher ProjectileLauncher { get; set; }
        IProjectileLauncher IProjectileLauncher { get; }

        void SetInputEventHandler(IStart start, IEnd end);

        void Initialize();
    }
}