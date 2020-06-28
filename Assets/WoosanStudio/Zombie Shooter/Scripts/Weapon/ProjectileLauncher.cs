﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    public class ProjectileLauncher : MonoBehaviour , IProjectileLauncher
    {
        //
        // *모델의 이름으로 찾기 때문에 해당
        // 총구, 총구 화염 , 탄피배출 transform 을 미리 만들어 놔야 한다
        //
        //총구 위치
        public Transform spawnLocator;
        //총구 머즐 위치
        public Transform spawnLocatorMuzzleFlare;
        //탄피 배출 위치
        public Transform shellLocator;
        //총구 들림 에니메이션 [삭제 예정]
        //public Animator recoilAnimator;
        //샷건 총구 위치
        public Transform[] shotgunLocator;

        //발사체 세팅값
        public ProjectileSettings projectileSetting;

        public float rapidFireDelay;
        //public bool CameraShake = true;
        //public ICameraShaker Shaker;

        float firingTimer;
        public bool firing;
        public int bombType = 0;

        public bool Torque = false;
        public float Tor_min, Tor_max;

        //총구 들림 활성화
        //public bool MuzzleFlip = false;

        private Coroutine _updateFrame;

        //탄퍼짐시 해당 파워
        public float BulletSpreadPower = 0.25f;

        //
        Vector3 forceValue;

        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> IProjectileLauncherActions Implementation <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

        private UnityEvent _triggerEvent = new UnityEvent();
        public UnityEvent TriggerEvent { get { return _triggerEvent; } set { _triggerEvent = value; } }

        //[Object Pool]
        //오브젝트 풀 관련 캐슁
        IObjectPool _shellPool;

        //오브젝트 풀에서 오브젝트 생성 기본 값
        private int poolMax = 20;


        void Start()
        {
            //초기화 루틴
            Initilized();
        }

        /// <summary>
        /// 초기화 루틴
        /// </summary>
        void Initilized()
        {
            SetModelLocator();

        }

        /// <summary>
        /// 모델 총기위치에 맞게 총구,총구화염,탄피배출 위치 세팅
        /// *모델의 이름으로 찾기 때문에 해당 이름에 마추어 총구, 총구 화염 , 탄피배출 transform 을 만들어 놓아야 한다
        /// </summary>
        void SetModelLocator()
        {
            //총구 위치
            spawnLocator = transform.Find("Locator");
            //총구 화염 위치
            spawnLocatorMuzzleFlare = transform.Find("MuzzleLocator");
            //탄배출구 위치
            shellLocator = transform.Find("ShellLocator");
            //샷건 총구
            if (spawnLocator.childCount > 0)
                shotgunLocator = spawnLocator.GetComponentsInChildren<Transform>();
        }

        

        /// <summary>
        /// 유저 Key Input을 제어하는 부분의 액션 이벤트를 등록
        /// </summary>
        public void SetInputEventHandler(IInputEvents inputEvents)
        {
            inputEvents.StartEvent.AddListener(StartFiring);
            inputEvents.EndEvent.AddListener(StopFiring);
        }

        /// <summary>
        /// 카메라 연출용
        /// </summary>
        /*void SetCameraShaker()
        {
            Shaker = FindObjectOfType<CameraShaker>();
        }*/

        IEnumerator UpdateFrame()
        {
            WaitForEndOfFrame WFEF = new WaitForEndOfFrame();

            Shoot();
            firingTimer = 0;            

            while (true)
            {
                if (projectileSetting.rapidFire && firing)
                {
                    if (firingTimer > projectileSetting.rapidFireCooldown + rapidFireDelay)
                    {
                        Shoot();
                        firingTimer = 0;
                    }
                }

                if (firing)
                {
                    firingTimer += Time.deltaTime;
                }

                yield return WFEF;
            }
        }

        public void Fire() { StartFiring(); }
        public void Stop() { StopFiring(); }

        /// <summary>
        /// 사격 시작
        /// </summary>
        public void StartFiring()
        {
            firing = true;
            //Fire();

            if (_updateFrame != null) StopCoroutine(_updateFrame);
            _updateFrame = StartCoroutine(UpdateFrame());
        }

        /// <summary>
        /// 사격 정지
        /// </summary>
        public void StopFiring()
        {
            firing = false;
            firingTimer = 0;

            if (_updateFrame != null) StopCoroutine(_updateFrame);
        }

        #region [-TestCode]
        void Update()
        {
            //scene view에서 방향 지시
            Debug.DrawRay(spawnLocator.position, spawnLocator.forward * 1000, Color.red);

            //사격 시작 및 정지 
            //if(Input.GetKeyDown(KeyCode.K))
            //{
            //    StartFiring();
            //}

            //if (Input.GetKeyDown(KeyCode.L))
            //{
            //    StopFiring();
            //}
        }
        #endregion

        public void Shoot()
        {
            TriggerEvent.Invoke();

            //머즐 플레이어 사용시
            if (projectileSetting.hasMuzzleFlare)
            {
                Instantiate(projectileSetting.muzzleflare, spawnLocatorMuzzleFlare.position, spawnLocatorMuzzleFlare.rotation);
            }
            //bombList[bombType].muzzleflare.Play(); //????
            
            //탄피 사용시
            if (projectileSetting.hasShells)
            {
                //Debug.Log("spawn shell");
                Instantiate(projectileSetting.shellPrefab, shellLocator.position, shellLocator.rotation);
                
            }
            //총구 들림
            //if (MuzzleFlip) { recoilAnimator.SetTrigger("recoil_trigger"); }

            if (projectileSetting.shotgunBehavior)
            {
                for (int i = 0; i < projectileSetting.shotgunPellets; i++)
                {
                    Rigidbody rocketInstanceShotgun;
                    rocketInstanceShotgun = Instantiate(projectileSetting.bombPrefab, shotgunLocator[i].position, shotgunLocator[i].rotation) as Rigidbody;

                    this.forceValue = shotgunLocator[i].forward * Random.Range(projectileSetting.min, projectileSetting.max) * (1f / Time.fixedDeltaTime) * 0.02f;

                    rocketInstanceShotgun.AddForce(this.forceValue);
                    //Player 사격인지 몬스터 사격인지 세팅
                    rocketInstanceShotgun.GetComponent<ExplodingProjectile>().playerShoted = projectileSetting.playerShoted;

                    //?????
                    //rocketInstanceShotgun.velocity = Vector3.zero;//가속도 초기화
                    //rocketInstanceShotgun.AddForce(shotgunLocator[i].forward * Random.Range(projectileSetting.min, projectileSetting.max));

                }
            } else
            {
                Rigidbody rocketInstance;
                rocketInstance = Instantiate(projectileSetting.bombPrefab, spawnLocator.position, spawnLocator.rotation) as Rigidbody;

                this.forceValue = spawnLocator.forward * Random.Range(projectileSetting.min, projectileSetting.max) * (1f / Time.fixedDeltaTime) * 0.02f;

                rocketInstance.AddForce(this.forceValue);

                //Player 사격인지 몬스터 사격인지 세팅
                rocketInstance.GetComponent<ExplodingProjectile>().playerShoted = projectileSetting.playerShoted;

                #region - [Test]
                //test code start
                //TestPrefabs.instance.MakeStart(spawnLocator.position);
                //test code end
                #endregion

                //???
                //rocketInstance.velocity = Vector3.zero;//가속도 초기화
                //rocketInstance.AddForce(spawnLocator.forward * Random.Range(projectileSetting.min, projectileSetting.max));
            }

            //if (Torque)
            //{
            //    rocketInstance.AddTorque(spawnLocator.up * Random.Range(Tor_min, Tor_max));
            //}
        }

        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> IProjectileLauncher Implementation <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        //public GunSettings GunSettings { get; set; }
        ProjectileLauncher IProjectileLauncher.ProjectileLauncher { get { return this; } set { value = this; } }

        public IProjectileLauncherEvents GetProjectileLauncherEvents()
        {
            return (IProjectileLauncherEvents)this;
        }

        public void ReloadAmmo()
        {
            Debug.Log("ProjectileLauncher.ReloadAmmo()");
        }

        public void UseAmmo()
        {
            Debug.Log("ProjectileLauncher.UseAmmo()");
        }
    }
}