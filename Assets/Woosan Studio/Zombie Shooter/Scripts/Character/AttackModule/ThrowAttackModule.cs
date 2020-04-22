using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    public class ThrowAttackModule : ICharacterAttackModule
    {
        #region [-ICharacterAttackModule Implement]
        //공격 시작 플레그
        private bool attackStart = false;
        public bool AttackStart { get => attackStart; set => attackStart = value; }

        public void Attack(ICharacterAnimatorModule characterAnimatorModule)
        {
            //공격이 시작되면 호출
            if (attackStart)
            {
                //공격을 바로 할려고 값을 일부러 넣어줌
                DoAttack(characterAnimatorModule);

                //Debug.Log("attackDelay = " + this.attackDelay + "  this.hitDelay = " + this.hitDelay + "    this.damage = " + this.damage);
            }
        }
        #endregion

        //공격과 공격 사이의 딜레이
        private float attackDelay = 0;
        //실제 시간
        private float attackDeltaTime = 0;
        //공격이 시작됬음을 알림
        private bool launchStart = false;
        //공격이 시작과 실제 때림 발생 사이의 간격
        private float fireDelay = 0;
        //실제시간
        private float fireDeltaTime = 0;
        //공격 데미지
        private int damage;
        private IHaveHit haveHit;
        private IHaveHealth haveHealth;

        //발사체 쏘기
        private IProjectileLauncher projectileLauncher;
        private Transform myTransform;

        /// <summary>
        /// 해당 트랜스 폼에 발사체 런처를 생성.
        /// </summary>
        /// <param name="transform">런처가 생성될 트랜스폼</param>
        private void SetProjectileLauncher(Transform transform, ProjectileSettings projectileSettings)
        {
            this.myTransform = transform;
            projectileLauncher = (IProjectileLauncher)this.myTransform.gameObject.AddComponent<ProjectileLauncher>();

            projectileLauncher.ProjectileLauncher = myTransform.GetComponent<ProjectileLauncher>();
            projectileLauncher.ProjectileLauncher.projectileSetting = projectileSettings;
        }

        /// <summary>
        /// 근접 공격 세팅
        /// </summary>
        /// <param name="attackDelay">공격 후 다음 공격간의 딜레이</param>
        /// <param name="hitDelay">공격실행 후 실제 공격 까지의 딜레이</param>
        public ThrowAttackModule(MonsterSettings monsterSettings, IHaveHit haveHit, IHaveHealth haveHealth,Transform projectileLauncherTransform)
        {
            //몬스터 데이터 세팅
            this.attackDelay = monsterSettings.AttackDelay;
            this.fireDelay = monsterSettings.HitDelay;
            this.damage = monsterSettings.Damage;

            //인터페이스 세팅
            this.haveHit = haveHit;
            this.haveHealth = haveHealth;

            //[중요]처음 시작시 바로 공격을 해야하기에 attackDelay값과 동일하게 마춰줌
            attackDeltaTime = attackDelay;

            //프로젝타일 런처 생성.
            SetProjectileLauncher(projectileLauncherTransform, monsterSettings.ProjectileSettings);
        }

        /// <summary>
        /// 몬스터 공격 시작
        /// </summary>
        public void DoAttack(ICharacterAnimatorModule characterAnimatorModule)
        {
            attackDeltaTime += Time.deltaTime;
            fireDeltaTime += Time.deltaTime;
            //Debug.Log("attackDeltaTime = " + attackDeltaTime + "         attackDelay = " + attackDelay);

            if (attackDeltaTime > attackDelay)
            {
                //Debug.Log("attack s");
                characterAnimatorModule.Attack();

                attackDeltaTime = 0;
                fireDeltaTime = 0;
                //공격이 시작되면 바리케이트 맞는 연출 활성화.
                launchStart = true;
            }

            if (launchStart)
            {
                //Debug.Log("Hit !!");
                LaunchProjectile();
            }
        }

        /// <summary>
        /// 발사체 발사
        /// </summary>
        void LaunchProjectile()
        {
            //Debug.Log("hitDeltaTime = " + hitDeltaTime + "         hitDelay = " + hitDelay);
            if (fireDeltaTime > fireDelay)
            {
                //Debug.Log("hit s");
                //바리케이트에 히트 호출
                fireDeltaTime = 0;

                projectileLauncher.ProjectileLauncher.Fire();

                launchStart = false;
            }
        }
    }
}
