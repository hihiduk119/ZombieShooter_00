using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

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

        public WoosanStudio.ZombieShooter.ProjectileSettings[] bombList;

        public bool CameraShake = true;
        public float rapidFireDelay;
        public ICameraShaker Shaker;

        float firingTimer;
        public bool firing;
        public int bombType = 0;


        //public bool swarmMissileLauncher = false;
        //int projectileSimFire = 1;

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
            //if (swarmMissileLauncher)
            //{
            //    projectileSimFire = 5;
            //}
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
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Switch(-1);
            }
            if (Input.GetButtonDown("Fire2") || Input.GetKeyDown(KeyCode.E))
            {
                Switch(1);
            }

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

            if (bombList[bombType].rapidFire && firing)
            {
                if (firingTimer > bombList[bombType].rapidFireCooldown + rapidFireDelay)
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

        public void Switch(int value)
        {
            bombType += value;
            if (bombType < 0)
            {
                bombType = bombList.Length;
                bombType--;
            }
            else if (bombType >= bombList.Length)
            {
                bombType = 0;
            }
        }

        public void Fire()
        {
            if (CameraShake)
            {
                Shaker.Shake();
            }
            Instantiate(bombList[bombType].muzzleflare, spawnLocatorMuzzleFlare.position, spawnLocatorMuzzleFlare.rotation);
            //   bombList[bombType].muzzleflare.Play();

            if (bombList[bombType].hasShells)
            {
                Instantiate(bombList[bombType].shellPrefab, shellLocator.position, shellLocator.rotation);
            }
            //총구 들림
            //if (MuzzleFlip) { recoilAnimator.SetTrigger("recoil_trigger"); }

            Rigidbody rocketInstance;
            rocketInstance = Instantiate(bombList[bombType].bombPrefab, spawnLocator.position, spawnLocator.rotation) as Rigidbody;
            // Quaternion.Euler(0,90,0)
            rocketInstance.AddForce(spawnLocator.forward * Random.Range(bombList[bombType].min, bombList[bombType].max));

            if (bombList[bombType].shotgunBehavior)
            {
                for (int i = 0; i < bombList[bombType].shotgunPellets; i++)
                {
                    Rigidbody rocketInstanceShotgun;
                    rocketInstanceShotgun = Instantiate(bombList[bombType].bombPrefab, shotgunLocator[i].position, shotgunLocator[i].rotation) as Rigidbody;
                    // Quaternion.Euler(0,90,0)
                    rocketInstanceShotgun.AddForce(shotgunLocator[i].forward * Random.Range(bombList[bombType].min, bombList[bombType].max));
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
