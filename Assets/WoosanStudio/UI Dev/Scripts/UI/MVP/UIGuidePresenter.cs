using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

namespace WoosanStudio.ZombieShooter.UI
{
    /// <summary>
    /// 가이드 프리젠터
    /// *MVP 모델
    /// </summary>
    public class UIGuidePresenter : MonoBehaviour
    {
        [Header("[복제하려는 아이템]")]
        public GameObject[] PrefabItems;

        [Header("[[Auto]사용 중인 아이템]")]
        public List<GuideItem> GuideItems = new List<GuideItem>(); 

        [Header("[MVP View]")]
        public UIGuideView View;


        /// <summary>
        /// 활성 비활성
        /// </summary>
        public void SetActivate(bool value)
        {
            View.SetActivate(value);
        }

        /// <summary>
        /// 아이템 생성
        /// </summary>
        public void MakeItem(GuideItem.Type type)
        {
            GameObject clone = Instantiate(PrefabItems[(int)type]) as GameObject;
            GuideItems.Add(clone.GetComponent<GuideItem>());
            //부모 세팅
            clone.transform.SetParent(this.transform);
            //이름 생성 및 세팅
            GuideItem guideItem = clone.GetComponent<GuideItem>();
            clone.name = guideItem.Name = guideItem.ItemType.ToString() + "-" + GuideItem.Index;
            //위치 조정
            View.Anchor(clone);
        }

        /// <summary>
        /// 모든 아이템 이동
        /// </summary>
        /// <param name="items"></param>
        public void MoveAllItem()
        {
            List<string> removeItem = new List<string>();

            for (int i = 0; i < this.GuideItems.Count; i++)
            {
                switch (this.GuideItems[i].PivotNumber)
                {
                    case 0://0 -> 1번으로
                        this.GuideItems[i].PivotNumber = 1;
                        View.MoveItem(1, this.GuideItems[i].gameObject);
                        break;
                    case 1://1 -> 2번으로
                        this.GuideItems[i].PivotNumber = 2;
                        View.MoveItem(2, this.GuideItems[i].gameObject);
                        break;
                    case 2://2 -> 는 제거
                        //리스트에서 제거
                        removeItem.Add(this.GuideItems[i].Name);
                        break;
                }
            }

            //삭제할 아이템이 있으면 찾아서 삭제
            if(removeItem.Count > 0)
            {
                for (int i = 0; i < removeItem.Count; i++)
                {
                    for (int j = 0; j < this.GuideItems.Count; j++)
                    {
                        if(GuideItems[j].Name.Equals(removeItem[i]))
                        {
                            GameObject clone = GuideItems[j].gameObject;
                            GuideItems.RemoveAt(j);
                            Destroy(clone);
                        }
                    }
                }
            }
        }

        /*
        #region [-TestCode]
        void Update()
        {
            //캔버스 활성화
            if (Input.GetKeyDown(KeyCode.A))
            {
                SetActivate(true);
            }

            //아이템 생성
            if (Input.GetKeyDown(KeyCode.S))
            {
                MakeItem(GuideItem.Type.Demon);
            }

            //아이템 이동
            if (Input.GetKeyDown(KeyCode.D))
            {
                MoveAllItem();
                //MakeItem(GuideItem.Type.Demon);
            }
        }
        #endregion
        */
    }
}
