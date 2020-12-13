using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;
using UnityEngine.UI;
using System.Text;

namespace WoosanStudio.ZombieShooter.UI
{
    /// <summary>
    /// 히트 메시지 표현
    /// </summary>
    public class HitMessage : MonoBehaviour
    {
        //싱글톤 패턴
        static public HitMessage Instance;
        //히트 글자
        private Text text;
        //글자 변경을 위한 스트링 빌더
        private StringBuilder stringBuilder = new StringBuilder();
        //히트 표시 앞글자
        private string[] prefix = { "hit " ,"crit "};

        private void Awake()
        {
            //시작시 transform에서 가져옴
            text = this.GetComponent<Text>();
            //싱글톤 패턴
            Instance = this;
        }

        /// <summary>
        /// 표시 하기
        /// </summary>
        /// <param name="damage">데미지 값</param>
        /// <param name="color">글자 색 = 기본 흰색</param>
        public void Show(int damage,int type,float endValue = 1.5f)
        {
            //활성화
            text.enabled = true;
            //초기화 => 연속 트윈 호출시 초기화 안됌.
            text.transform.DOKill();
            text.DOKill();
            //스케일 초기화
            text.transform.localScale = Vector3.one;

            //표시할 글자 조합 1
            stringBuilder.Clear();

            //타입에 따라 색 및 글자 변경
            switch (type)
            {
                case 0: //"Hit" 메시지
                    //텍스트 색 초기화
                    text.color = Color.white;
                    //표시할 글자 조합 2
                    stringBuilder.Append(prefix[0]);
                    break;
                case 1: //"Crit" 메시지
                    //텍스트 색 초기화
                    text.color = new Color32(255, 209, 0, 255);
                    //표시할 글자 조합 2
                    stringBuilder.Append(prefix[1]);
                    break;
            }

            //표시할 글자 조합 3
            stringBuilder.Append(damage);
            //글자 세팅
            text.text = stringBuilder.ToString();

            //요요로 스케일 트윈
            text.transform.DOScale(endValue, 0.075f).SetEase(Ease.InOutSine).SetLoops(2, LoopType.Yoyo).OnComplete(() => {
                //글자 페이드 아웃
                text.DOFade(0f, 0.5f).OnComplete(() => {
                    //비활성화
                    text.enabled = false;
                });
            });
        }

        //#region [-TestCode]
        //void Update()
        //{
        //    if (Input.GetKeyDown(KeyCode.Alpha1))
        //    {
        //        Show(Random.Range(1,20),Color.white);
        //    }
        //}
        //#endregion
    }
}
