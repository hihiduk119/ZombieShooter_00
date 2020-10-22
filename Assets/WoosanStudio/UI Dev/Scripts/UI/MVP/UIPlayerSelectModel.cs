using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 플레이어의 데이터
    /// *MVC패턴
    /// </summary>
    public class UIPlayerSelectModel : MonoBehaviour
    {
        //*모델 타입 이넘과 모델 리스트의 순서는 동일해야 한다.
        public enum ModelType
        {
            BusinessMan = 0,
            FireFighter,
            Hobo,
            Pimp,
            Policeman,
            Prostitute,
            Punk,
            RiotCop,
            RoadWorker,
            Robber,
            Sheriff,
            StreetMan,
            Trucker,
            Woman,
        }

        [Serializable]
        public class Data {
            public UIPlayerSelectModel.ModelType Type = UIPlayerSelectModel.ModelType.BusinessMan;

            public Data(){}
        }

        public Data data = new Data();

        public void Load()
        {
            if (!PlayerPrefs.HasKey("PlayerInRobby")) { Save(); }

            data = JsonUtility.FromJson<UIPlayerSelectModel.Data>(PlayerPrefs.GetString("PlayerInRobby"));
        }

        public void Save()
        {
            PlayerPrefs.SetString("PlayerInRobby", JsonUtility.ToJson(data));
        }
    }
}
