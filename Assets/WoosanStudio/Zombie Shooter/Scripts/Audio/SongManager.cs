using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DarkTonic.MasterAudio;

namespace WoosanStudio.ZombieShooter.Audio
{
    public class SongManager : MonoBehaviour
    {
        public PlaylistController playlistController;

        private bool pause = false;

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

        void Update()
        {
            //일시 정지 또는 재시작
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Pause();
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                PlayRobbySong();
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                PlayLoadingSong();
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                PlayCardSelectionSong();
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
    }
}
