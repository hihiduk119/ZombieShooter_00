using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 모델을 변경하기 위해
    /// </summary>
    public class Model : MonoBehaviour
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

        [Header("[모델 리스트]")]
        public List<GameObject> Models = new List<GameObject>();

        //테스트용
        int testIndex = 0;

        public void Swap( ModelType type)
        {
            Models.ForEach(value => value.SetActive(false));

            //해당 모델을 활성화.
            //*모델 타입 이넘과 모델 리스트의 순서는 동일해야 한다.
            Models[(int)type].SetActive(true);
        }

        #region [-TestCode]
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Swap((ModelType)testIndex);

                testIndex--;

                if (testIndex < 0)
                    testIndex = Models.Count - 1;
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Swap((ModelType)testIndex);
                testIndex++;

                if (testIndex >= Models.Count)
                    testIndex = 0;
            }
        }
        #endregion

    }
}
