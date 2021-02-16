namespace WoosanStudio.ZombieShooter
{
    public interface IHaveHit
    {
        //기본 히트
        void Hit();
        //글로벌 히트
        void HitByGlobalDamage();
    }
}