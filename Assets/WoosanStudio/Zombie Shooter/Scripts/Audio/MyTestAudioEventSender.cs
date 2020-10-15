using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkTonic.MasterAudio;


namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 오디오 테스트용 이벤트 발생기
    /// </summary>
    public class MyTestAudioEventSender : MonoBehaviour
    {
        public enum TestAmmoType
        {
            Bullet,
            Laser,
            Plasma,
        }

        public TestAmmoType testAmmoType;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        #region [-TestCode]
        void Update()
        {
            //에어스트라이크 사운드 호출
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                MasterAudio.FireCustomEvent("CustomEvent_CallAirStrike", this.transform);
            }

            //권총 사운드
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                switch (testAmmoType)
                {
                    case TestAmmoType.Bullet:
                        MasterAudio.FireCustomEvent("CustomEvent_FirePistolWithBullet", this.transform);
                        break;
                    case TestAmmoType.Laser:
                        MasterAudio.FireCustomEvent("CustomEvent_FirePistolWithLaser", this.transform);
                        break;
                    case TestAmmoType.Plasma:
                        MasterAudio.FireCustomEvent("CustomEvent_FirePistolWithPlasma", this.transform);
                        break;

                }

                //권총 재장전
                //MasterAudio.FireCustomEvent("CustomEvent_ReloadPistol", this.transform);



                //Debug.Log("Click alpha 1");
            }

            //돌격소총 사운드
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                switch (testAmmoType)
                {
                    case TestAmmoType.Bullet:
                        MasterAudio.FireCustomEvent("CustomEvent_FireAssaultRifleWithBullet", this.transform);
                        break;
                    case TestAmmoType.Laser:
                        MasterAudio.FireCustomEvent("CustomEvent_FireAssaultRifleWithLaser", this.transform);
                        break;
                    case TestAmmoType.Plasma:
                        MasterAudio.FireCustomEvent("CustomEvent_FireAssaultRifleWithPlasma", this.transform);
                        break;

                }

                //돌격소총 재장전
                //MasterAudio.FireCustomEvent("CustomEvent_ReloadAssaultRifle", this.transform);
                //Debug.Log("Click alpha 2");
            }

            //샷건 사운드
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                switch (testAmmoType)
                {
                    case TestAmmoType.Bullet:
                        MasterAudio.FireCustomEvent("CustomEvent_FireShotgunWithBullet", this.transform);
                        break;
                    case TestAmmoType.Laser:
                        MasterAudio.FireCustomEvent("CustomEvent_FireShotgunWithLaser", this.transform);
                        break;
                    case TestAmmoType.Plasma:
                        MasterAudio.FireCustomEvent("CustomEvent_FireShotgunWithPlasma", this.transform);
                        break;

                }

                //샷건 재장전
                //MasterAudio.FireCustomEvent("CustomEvent_ReloadShotgun", this.transform);
                //Debug.Log("Click alpha 3");
            }

            //스나이퍼라이플 사운드
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                switch (testAmmoType)
                {
                    case TestAmmoType.Bullet:
                        MasterAudio.FireCustomEvent("CustomEvent_FireSniperRifleWithBullet", this.transform);
                        break;
                    case TestAmmoType.Laser:
                        MasterAudio.FireCustomEvent("CustomEvent_FireSniperRifleWithLaser", this.transform);
                        break;
                    case TestAmmoType.Plasma:
                        MasterAudio.FireCustomEvent("CustomEvent_FireSniperRifleWithPlasma", this.transform);
                        break;

                }

                //저격소총 재장전
                //MasterAudio.FireCustomEvent("CustomEvent_ReloadSniperRifle", this.transform);
                //Debug.Log("Click alpha 4");
            }


            //오디오 믹서 컨트롤 볼륨 다운
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                //오디오믹서 컨트롤
                MasterAudio.FireCustomEvent("CustomEvent_Snapshots_SongVolumeDown", this.transform);
            }

            //오디오 믹서 컨트롤 볼륨 업
            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                //오디오믹서 컨트롤
                MasterAudio.FireCustomEvent("CustomEvent_Snapshots_Default", this.transform);
            }
        }
        #endregion
    }
}
