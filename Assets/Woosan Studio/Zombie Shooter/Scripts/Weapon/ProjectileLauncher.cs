using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    public class ProjectileLauncher : MonoBehaviour , IProjectileLauncher
    {
        public Transform spawnLocator;
        public Transform spawnLocatorMuzzleFlare;
        public Transform shellLocator;
        //총구 들림 에니메이션 [삭제 예정]
        //public Animator recoilAnimator;

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
        //탄퍼짐 활성화
        public bool MinorRotate;
        public bool MajorRotate = false;
        int seq = 0;

        private Coroutine _updateFrame;

        //탄퍼짐시 해당 파워
        public float BulletSpreadPower = 0.25f;

        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> IProjectileLauncherActions Implementation <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

        private UnityEvent _triggerEvent = new UnityEvent();
        public UnityEvent TriggerEvent { get { return _triggerEvent; } set { _triggerEvent = value; } }
        

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
            //SetCameraShaker();
            SetModelLocator();
        }

        /// <summary>
        /// 모델 총기위치에 맞게 총구,총구화염,탄피배출 위치 세팅
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

            Fire();
            firingTimer = 0;            

            while (true)
            {
                if (projectileSetting.rapidFire && firing)
                {
                    if (firingTimer > projectileSetting.rapidFireCooldown + rapidFireDelay)
                    {
                        Fire();
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

        public void StartFiring()
        {
            firing = true;
            //Fire();

            if (_updateFrame != null) StopCoroutine(_updateFrame);
            _updateFrame = StartCoroutine(UpdateFrame());
        }

        public void StopFiring()
        {
            firing = false;
            firingTimer = 0;

            if (_updateFrame != null) StopCoroutine(_updateFrame);
        }

        public void Fire()
        {
            TriggerEvent.Invoke();

            Instantiate(projectileSetting.muzzleflare, spawnLocatorMuzzleFlare.position, spawnLocatorMuzzleFlare.rotation);
            //   bombList[bombType].muzzleflare.Play();

            if (projectileSetting.hasShells)
            {
                Instantiate(projectileSetting.shellPrefab, shellLocator.position, shellLocator.rotation);
            }
            //총구 들림
            //if (MuzzleFlip) { recoilAnimator.SetTrigger("recoil_trigger"); }

            Rigidbody rocketInstance;
            rocketInstance = Instantiate(projectileSetting.bombPrefab, spawnLocator.position, spawnLocator.rotation) as Rigidbody;
            // Quaternion.Euler(0,90,0)
            rocketInstance.AddForce(spawnLocator.forward * Random.Range(projectileSetting.min, projectileSetting.max));

            if (projectileSetting.shotgunBehavior)
            {
                for (int i = 0; i < projectileSetting.shotgunPellets; i++)
                {
                    Rigidbody rocketInstanceShotgun;
                    rocketInstanceShotgun = Instantiate(projectileSetting.bombPrefab, shotgunLocator[i].position, shotgunLocator[i].rotation) as Rigidbody;
                    // Quaternion.Euler(0,90,0)
                    rocketInstanceShotgun.AddForce(shotgunLocator[i].forward * Random.Range(projectileSetting.min, projectileSetting.max));
                }
            }

            if (Torque)
            {
                rocketInstance.AddTorque(spawnLocator.up * Random.Range(Tor_min, Tor_max));
            }
            //탄퍼짐 작음
            if (MinorRotate)
            {
                RandomizeRotation();
            }
            //탄퍼짐 큼
            if (MajorRotate)
            {
                Major_RandomizeRotation();
            }
        }


        //탄퍼짐 메소드
        void RandomizeRotation()
        {
            if (seq == 0)
            {
                seq++;
                transform.Rotate(0, 1 * BulletSpreadPower, 0);
            }
            else if (seq == 1)
            {
                seq++;
                transform.Rotate(1 * BulletSpreadPower, 1 * BulletSpreadPower, 0);
            }
            else if (seq == 2)
            {
                seq++;
                transform.Rotate(1 * BulletSpreadPower, -3 * BulletSpreadPower, 0);
            }
            else if (seq == 3)
            {
                seq++;
                transform.Rotate(-2 * BulletSpreadPower, 1 * BulletSpreadPower, 0);
            }
            else if (seq == 4)
            {
                seq++;
                transform.Rotate(1 * BulletSpreadPower, 1 * BulletSpreadPower, 1 * BulletSpreadPower);
            }
            else if (seq == 5)
            {
                seq = 0;
                transform.Rotate(-1 * BulletSpreadPower, -1 * BulletSpreadPower, -1 * BulletSpreadPower);
            }
        }

        //탄퍼짐 메소드 2
        void Major_RandomizeRotation()
        {
            if (seq == 0)
            {
                seq++;
                transform.Rotate(0, 5 * BulletSpreadPower, 0);
            }
            else if (seq == 1)
            {
                seq++;
                transform.Rotate(5 * BulletSpreadPower, 5 * BulletSpreadPower, 0);
            }
            else if (seq == 2)
            {
                seq++;
                transform.Rotate(5 * BulletSpreadPower, -15 * BulletSpreadPower, 0);
            }
            else if (seq == 3)
            {
                seq++;
                transform.Rotate(-10 * BulletSpreadPower, 5 * BulletSpreadPower, 0);
            }
            else if (seq == 4)
            {
                seq++;
                transform.Rotate(5 * BulletSpreadPower, 5 * BulletSpreadPower, 5 * BulletSpreadPower);
            }
            else if (seq == 5)  
            {
                seq = 0;
                transform.Rotate(-5 * BulletSpreadPower, -5* BulletSpreadPower, -5 * BulletSpreadPower);
            }
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
