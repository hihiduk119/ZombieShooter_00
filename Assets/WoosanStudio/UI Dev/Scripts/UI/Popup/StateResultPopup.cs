using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 해당 스테이지 결과 보고.
    /// </summary>
    public class StateResultPopup : MonoBehaviour
    {
        public Text Title;
        public Text Score;
        public Text Coin;
        public Text Killed;

        public List<GameObject> unlockItems = new List<GameObject>();
        //다음 스테이지 진행 버튼
        public GameObject Next;
        //서브 패넡 사용
        public GameObject SubPanel;             
        

        #region [-TestCode]
        public List<Sprite> testSprites = new List<Sprite>();
        #endregion

        /// <summary>
        /// 스테이지 결과 세팅
        /// 
        /// </summary>
        /// <param name="isSuccess"></param>
        /// <param name="score"></param>
        /// <param name="coin"></param>
        /// <param name="killed"></param>
        /// <param name="sprites"></param>
        public void SetResult(bool isSuccess,string title,int score,int coin,int killed,bool useSub,List<Sprite> sprites)
        {
            if(isSuccess)
            {
                Title.text = title + " COMPLETE";
                //다음 버튼 활성화
                Next.SetActive(true);
            } else
            {
                Title.text = title + " FAIL";
                //다음 버튼 비활성화
                Next.SetActive(false);
            }

            //서브 패넡 사용 여부 결정
            SubPanel.SetActive(useSub);

            Score.text = score.ToString();
            Coin.text = coin.ToString();
            Killed.text = killed.ToString();

            //일단 전체 비활성화
            unlockItems.ForEach(value => value.SetActive(false));

            Transform imageTf;

            for (int index = 0; index < sprites.Count ;index++)
            {
                unlockItems[index].SetActive(true);
                imageTf = unlockItems[index].transform.Find("Icon");
                imageTf.GetComponent<Image>().sprite = sprites[index];
                imageTf.GetComponent<RectTransform>().sizeDelta = new Vector2(sprites[index].texture.width, sprites[index].texture.height);
            }
        }

        //private void Update()
        //{
        //    if(Input.GetKeyDown(KeyCode.M))
        //    {
        //        SetResult(true,"1-1",2000,100,5, false,testSprites);
        //    }
        //}
    }
}
