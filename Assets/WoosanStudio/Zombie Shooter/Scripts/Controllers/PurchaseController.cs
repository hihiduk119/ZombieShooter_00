using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

namespace WoosanStudio.ZombieShooter
{    
    /// <summary>
    /// 모든 구매를 처리하는 컨트롤러
    /// **세이브 저장 잘되는지 테스트 필요.
    /// </summary>
    public class PurchaseController : MonoBehaviour
    {
        //싱글톤 패턴
        static public PurchaseController Instance;
        //실제 젬을 추가 
        private GemPresenter gemPresenter;
        //실제 코인을 추가
        private CoinPresenter coinPresenter;
        //실제 에너지를 추가
        private EnergyPresenter energyPresenter;

        private void Awake()
        {
            Instance = this;
        }

        /// <summary>
        /// 코인 & 젬 구매
        /// </summary>
        public void BuySometing(UIShopSlotModel.Data data)
        {
            int gainValue = data.GainValue;

            switch (data.type)
            {
                case UIShopSlotModel.Type.Gem:
                    gemPresenter.AddGem(gainValue);
                    break;
                case UIShopSlotModel.Type.Goin:
                    coinPresenter.AddCoin(gainValue);
                    break;
            }
        }

        /// <summary>
        /// 에너지 구매
        /// </summary>
        public void BuyEnergy(UIShopChargesAllEnergyPopupModel.Data data)
        {
            //최대 에너지 많큼 추가
            energyPresenter.UpdateEnergy(energyPresenter.Model.GetData().MaxEnergy);
        }
    }
}
