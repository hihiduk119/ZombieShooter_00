using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoosanStudio.ZombieShooter.Stamina
{
    /// <summary>
    /// 스테미나 컨트롤
    /// </summary>
    public class StaminaManager : MonoBehaviour
    {
        [Header("[[Auto -> Awake()]스테미나 바 컨트롤]")]
        public UI.UIPlayerCanvasPresenter PlayerCanvasPresenter;

        [Header("[기본 최대 스테미나]")]
        public int MaxStamina = 100;

        [Header("[현재 스테미나]")]
        public int Stamina = 100;

        [Header("[스테미나 소모 최소 값 = MoveEffect.cs MinPower와 같음 *통합 필요? ]")]
        public float MinPower = 0.2f;

        //[Header("[작동해야 해서 필요]")]
        //private bool isActivate = false;
        //public bool IsActivate { get; set; }

        //벡터의 길이
        private float power;

        //감소 전용 스테미나 델타 타임
        private float downStaminaDeltaTime;

        //증가 전용 스테미나 델타 타임
        private float upStaminaDeltaTime;

        private void Awake()
        {
            //자동  셋업
            this.PlayerCanvasPresenter = GameObject.FindObjectOfType<UI.UIPlayerCanvasPresenter>();
        }

        private void Start()
        {
            //최대 스테미나 UI애 세팅
            PlayerCanvasPresenter.UpdateMaxStamina(this.MaxStamina);
        }

        private void Update()
        {
            //조이스틱 없을때 동작 안함
            if (WoosanStudio.Common.JoystickInput.Instance == null) return;
            //비활성시 동작 안함
            //if (!isActivate) return;

            //조이스틱 값 가져오기
            this.power = Vector2.SqrMagnitude(new Vector2(WoosanStudio.Common.JoystickInput.Instance.Horizontal, WoosanStudio.Common.JoystickInput.Instance.Vertical));

            //스테미나 증가 및 감소 컨트롤
            if(MinPower <= power)
            {
                this.downStaminaDeltaTime += Time.deltaTime;

                //스테미나 감소
                if (this.downStaminaDeltaTime >= 1f)
                {
                    DownStamina();

                    this.downStaminaDeltaTime -= 1f;
                }

                //Debug.Log("[ 벡터의 길이 = " + this.power + " 유지된 시간 = " + this.downStaminaDeltaTime + " ]");
            } else
            {
                this.upStaminaDeltaTime += Time.deltaTime;

                //스테미나 감소
                if (this.upStaminaDeltaTime >= 1f)
                {
                    UpStamina();

                    this.upStaminaDeltaTime -= 1f;
                }
            }
        }

        /// <summary>
        /// 스테미나 감소
        /// </summary>
        private void DownStamina()
        {
            //스테미나 -3감소
            this.Stamina -= 10;
            if(this.Stamina <= 0 )
            {
                this.Stamina = 0;
            }

            //스테미나 UI 업데이트
            PlayerCanvasPresenter.UpdateStamina(this.Stamina);
            //Debug.Log("스테미나 = "+this.Stamina +" 스테미나 감소 -3");
        }

        /// <summary>
        /// 스테미나 증가
        /// </summary>
        private void UpStamina()
        {
            //스테미나 +1증가
            this.Stamina += 2;

            if (this.Stamina >= this.MaxStamina)
            {
                this.Stamina = this.MaxStamina;
            }

            //스테미나 UI 업데이트
            PlayerCanvasPresenter.UpdateStamina(this.Stamina);
            //Debug.Log("스테미나 = " + this.Stamina + "스테미나 증가 +1");
        }
    }
}
