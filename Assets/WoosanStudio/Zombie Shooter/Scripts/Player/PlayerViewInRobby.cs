using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 플레이어의 데이터
    /// *MVC패턴
    /// </summary>
    public class PlayerViewInRobby : MonoBehaviour
    {
        public PlayerControllerInRobby Controller;

        public void LeftClick()
        {
            Controller.Change(-1);
        }

        public void RightClick()
        {
            Controller.Change(1);
        }
    }
}
