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

        public WoosanStudio.ZombieShooter.ProjectileSettings bombList;

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
            //Movement
            if (Input.GetButton("Horizontal"))
            {
                if (Input.GetAxis("Horizontal") < 0)
                {
                    gameObject.transform.Rotate(Vector3.up, -25 * Time.deltaTime);
                }
                else
                {
                    gameObject.transform.Rotate(Vector3.up, 25 * Time.deltaTime);
                }
            }

            //BULLETS
            //if (Input.GetKeyDown(KeyCode.Q))
            //{
            //    Switch(-1);
            //}
            //if (Input.GetButtonDown("Fire2") || Input.GetKeyDown(KeyCode.E))
            //{
            //    Switch(1);
            //}

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

            if (bombList.rapidFire && firing)
            {
                if (firingTimer > bombList.rapidFireCooldown + rapidFireDelay)
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

        //public void Switch(int value)
        //{
        //    bombType += value;
        //    if (bombType < 0)
        //    {
        //        bombType = bombList.Length;
        //        bombType--;
        //    }
        //    else if (bombType >= bombList.Length)
        //    {
        //        bombType = 0;
        //    }
        //}

        public void Fire()
        {
            if (CameraShake)
            {
                Shaker.Shake();
            }
            Instantiate(bombList.muzzleflare, spawnLocatorMuzzleFlare.position, spawnLocatorMuzzleFlare.rotation);
            //   bombList[bombType].muzzleflare.Play();

            if (bombList.hasShells)
            {
                Instantiate(bombList.shellPrefab, shellLocator.position, shellLocator.rotation);
            }
            //총구 들림
            //if (MuzzleFlip) { recoilAnimator.SetTrigger("recoil_trigger"); }

            Rigidbody rocketInstance;
            rocketInstance = Instantiate(bombList.bombPrefab, spawnLocator.position, spawnLocator.rotation) as Rigidbody;
            // Quaternion.Euler(0,90,0)
            rocketInstance.AddForce(spawnLocator.forward * Random.Range(bombList.min, bombList.max));

            if (bombList.shotgunBehavior)
            {
                for (int i = 0; i < bombList.shotgunPellets; i++)
                {
                    Rigidbody rocketInstanceShotgun;
                    rocketInstanceShotgun = Instantiate(bombList.bombPrefab, shotgunLocator[i].position, shotgunLocator[i].rotation) as Rigidbody;
                    // Quaternion.Euler(0,90,0)
                    rocketInstanceShotgun.AddForce(shotgunLocator[i].forward * Random.Range(bombList.min, bombList.max));
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
