using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

namespace WoosanStudio.ZombieShooter.Weapon
{
    /// <summary>
    /// 총기 반동 애니메이션
    /// </summary>
    public class GunAnimation : MonoBehaviour
    {

        private Quaternion InitRotation;
        private Vector3 InitPosition;

        //반동 회복 시간
        private float recoverTime = 0.1f;
        
        //수직 반동
        private float verticalRecoil = 10f;

        //수직 반동
        private Vector2 horizontalRecoil = new Vector2(-5,10);//-10 ~ 10 -> 램덤에 넣을때는 -10,20넣어야horizontalRecoil

        //총기 밀림 반동
        private float backRecoil = 0.15f;

        private IGun iGun;

        private void Awake()
        {
            InitRotation = this.transform.localRotation;
            InitPosition = this.transform.localPosition;
        }

        private void Start()
        {
            //IGun인터페이스 찾아서 총 발사시 액션 등록
            //모든 총은 IGun인터페이스 가지고 있음-> 부모가 총
            iGun = transform.parent.GetComponent<IGun>();
            //iGun이 로비 상태에서는 없음
            if(iGun != null) { iGun.ProjectileLauncher.TriggerEvent.AddListener(Action); }
        }

        public void Action()
        {
            this.transform.DOKill();
            //총기 수직,수평 반동 연출
            //기본 x = 90 이라서 그냥 넣음
            this.transform.localRotation = Quaternion.Euler(new Vector3(-verticalRecoil, 90 + Random.Range((int)horizontalRecoil.x, (int)horizontalRecoil.y), 0));
            this.transform.DOLocalRotate(InitRotation.eulerAngles, recoverTime).SetEase(Ease.OutSine);

            //총기 뒤로 밀림 반동 연출
            Vector3 tempPosition = InitPosition;
            tempPosition.x -= backRecoil;
            this.transform.localPosition = tempPosition;
            this.transform.DOLocalMove(InitPosition, recoverTime).SetEase(Ease.OutSine);

        }

        //private void OnDestroy()
        //{
        //    iGun.ProjectileLauncher.TriggerEvent.RemoveListener(Action);
        //}

        /*
        #region [-TestCode]
        void Update()
        {
            //총 반동 애니메이션
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Action();
                Debug.Log("A down");
            }
        }
        #endregion
        */
    }
}
