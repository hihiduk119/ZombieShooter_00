using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{
    public class AmmoBar : MonoBehaviour, IHaveAmmo
    {
        //현재 탄약
        [SerializeField]
        private int ammo = 10;
        public int Ammo { get => ammo; set => ammo = value; }

        //최대 탄약
        [SerializeField]
        private int maxAmmo = 10;
        public int MaxAmmo { get => maxAmmo; set => maxAmmo = value; }

        //탄약 사용 이벤트
        //*_iGun.ProjectileLauncher.TriggerEvent와 연결용
        private UnityEvent fireEvent;
        public UnityEvent FireEvent {
            get { return fireEvent; }
            set {
                fireEvent = value;

                //Debug.Log(">>>>>>>>>> 탄약 감소 이벤트 변경 프로젝타일 이벤트로 변경!! = ");
                //탄약 사용시 탄약 1 씩 감소
                fireEvent.AddListener(DecreaseAmmo);

                //Debug.Log(">>>>>>>>>> 이벤트 갯수 = " + fireEvent.GetPersistentEventCount());
            }
        }

        //탄약 0 이벤트
        private UnityEvent zeroEvent = new UnityEvent();
        public UnityEvent ZeroEvent { get => zeroEvent; set => throw new System.NotImplementedException(); }

        void Awake()
        {
            //초기화 Ih
            Initialize();
        }

        /// <summary>
        /// 초기화
        /// </summary>
        public void Initialize()
        {
            //최대 체력에 현재 체력 마춤
            ammo = maxAmmo;
        }

        /// <summary>
        /// 새로운 체력을 세팅합니다
        /// </summary>
        /// <param name="health"></param>
        public void SetMaxAmmo(int maxAmmo)
        {
            //최대 탄약 세로 세팅
            this.maxAmmo = maxAmmo;
            //현재 탄약 초기화
            Initialize();
        }

        /// <summary>
        /// 탄약 감소됨
        /// </summary>
        void DecreaseAmmo()
        {
            ammo -= 1;

            Debug.Log("남은 탄약 = " + ammo);

            //탄약이 0 이하면 호출
            if (ammo <= 0)
            {
                zeroEvent.Invoke();

                Debug.Log("탄약 0임!!");

                ammo = 0;
            }
        }
    }
}
