using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using System.Linq;

namespace WoosanStudio.ZombieShooter
{
    public class ProjectileLauncher : MonoBehaviour
    {
        public Transform spawnLocator;
        public Transform spawnLocatorMuzzleFlare;
        public Transform shellLocator;
        //총구 들림 에니메이션 [삭제 예정]
        //public Animator recoilAnimator;

        public Transform[] shotgunLocator;

        public WoosanStudio.ZombieShooter.ProjectileSettings projectileSetting;

        public bool CameraShake = true;
        public float rapidFireDelay;
        public ICameraShaker Shaker;

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

        //탄퍼짐시 해당 파워
        public float BulletSpreadPower = 0.25f;

        private void Awake()
        {
            Shaker = FindObjectOfType<CameraShaker>();
        }

        void Start()
        {
            //모델 총기위치에 맞게 총구,총구화염,탄피배출 위치 세팅
            Initilized();
        }

        /// <summary>
        /// 모델 총기위치에 맞게 총구,총구화염,탄피배출 위치 세팅
        /// </summary>
        void Initilized()
        {
            //총구 위치, 총구 화염 위치, 탄배출구 위치 세팅
            spawnLocator = transform.Find("Locator");
            spawnLocatorMuzzleFlare = transform.Find("MuzzleLocator");
            shellLocator = transform.Find("ShellLocator");

            //샷건 총구
            if(spawnLocator.childCount > 0)
                shotgunLocator = spawnLocator.GetComponentsInChildren<Transform>();

        }

        void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                firing = true;
                Fire();
            }
            if (Input.GetButtonUp("Fire1"))
            {
                firing = false;
                firingTimer = 0;
            }

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
        }

        public void Fire()
        {
            if (CameraShake)
            {
                Shaker.Shake();
            }
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
    }
}
