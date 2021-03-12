using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using DarkTonic.MasterAudio;

namespace WoosanStudio.ZombieShooter.Audio
{
    /// <summary>
    /// 사운드 송 플레이 메니저
    /// </summary>
    public class SongManager : MonoBehaviour
    {
        static public SongManager Instance;

        public PlaylistController playlistController;

        private bool pause = false;

        private void Awake()
        {
            Instance = this;

            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        /// <summary>
        /// 씬로드 호출
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="mode"></param>
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            //Debug.Log(scene.name);
            switch(scene.name)
            {
                case "1.ZombieShooter-Robby":
                    //로비 송 플레이
                    this.PlayRobbySong();
                    break;
                default: break;
            }

            if(scene.Equals("0.ZombieShooter-Title") )
            {
                Debug.Log("[타이틀 씬 로드]");
                //사운드 죽임
                this.Pause();
            }
        }

        /// <summary>
        /// 노래 일시 정지
        /// </summary>
        /// <param name="pause"></param>
        public void Pause()
        {
            pause = !pause;

            if (pause)
            {
                playlistController.PausePlaylist();
            } else
            {
                playlistController.RestartPlaylist();
            }
        }

        /// <summary>
        /// 로비 송 플레이
        /// </summary>
        public void PlayRobbySong()
        {
            playlistController.StartPlaylist("Robby playlist");
        }

        /// <summary>
        /// 로딩 송 플레이
        /// </summary>
        public void PlayLoadingSong()
        {
            playlistController.StartPlaylist("Loading playlist");
        }

        /// <summary>
        /// 전투 송 플레이
        /// </summary>
        /// <param name="index"></param>
        public void PlayBattleSong(int index)
        {
            switch(index)
            {
                case 0:
                    playlistController.StartPlaylist("Battle playlist", "Battle/1.Unbreakable_loop"); break;
                case 1:
                    playlistController.StartPlaylist("Battle playlist", "Battle/2.Hard Rock_loop"); break;
                case 2:
                    playlistController.StartPlaylist("Battle playlist", "Battle/3.Can't Stop Me_loop"); break;
                case 3:
                    playlistController.StartPlaylist("Battle playlist", "Battle/4.Let's Rock_loop"); break;
                case 4:
                    playlistController.StartPlaylist("Battle playlist", "Battle/5.Be Faster_loop"); break;
                case 5:
                    playlistController.StartPlaylist("Battle playlist", "Battle/6.Fatality Racer_loop"); break;
                case 6:
                    playlistController.StartPlaylist("Battle playlist", "Battle/7.Rage Machine_loop"); break;
                case 7:
                    playlistController.StartPlaylist("Battle playlist", "Battle/8.No Way Back_loop"); break;
                case 8:
                    playlistController.StartPlaylist("Battle playlist", "Battle/9.Time For Action_loop"); break;
                case 9:
                    playlistController.StartPlaylist("Battle playlist", "Battle/10.Heart of Warrior_loop"); break;
            }

            //Battle/
        }

        /// <summary>
        /// 카드 선택송 플레이
        /// </summary>
        public void PlayCardSelectionSong()
        {
            playlistController.StartPlaylist("Card Selection playlist");
        }

        /*
        void Update()
        {
            //일시 정지 또는 재시작
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                MasterAudio.FireCustomEvent("CustomEvent_ReloadPistol", this.transform);
                //Pause();
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                MasterAudio.FireCustomEvent("CustomEvent_ReloadShotgun", this.transform);
                //PlayRobbySong();
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                MasterAudio.FireCustomEvent("CustomEvent_ReloadAssaultRifle", this.transform);
                //PlayLoadingSong();
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                MasterAudio.FireCustomEvent("CustomEvent_ReloadSniperRifle", this.transform);
                //PlayCardSelectionSong();
            }

            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                PlayBattleSong(0);
            }

            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                PlayBattleSong(1);
            }

            if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                PlayBattleSong(2);
            }
        }
        */
    }
}
