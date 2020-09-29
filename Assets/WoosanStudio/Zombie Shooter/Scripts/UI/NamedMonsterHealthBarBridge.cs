using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace WoosanStudio.ZombieShooter
{
    /// <summary>
    /// 네임드 몬스터 전용으로 생성시 체력바와 자동 연결 시키는 브릿지
    /// </summary>
    public class NamedMonsterHealthBarBridge : MonoBehaviour
    {
        //데미지 이벤트 연결용
        //*데미지 이벤트가 해당 인터페이스에서 전달됨
        private IHaveHealth haveHealth;
        //Monster.cs 가져오며 스폰시 이벤트 발생
        private ISpawnHandler spawnHandler;
        //죽음 발생 이벤트를 가지고 있음
        private DoDie doDie;

        private void Awake()
        {
            //IHaveHealth를 찾아서 이벤트 등록
            haveHealth = transform.GetComponent<IHaveHealth>();
            //생성시 UI 활성 필요
            spawnHandler = transform.GetComponent<ISpawnHandler>();
            //스폰 이벤트 연결
            spawnHandler.SpawnEvent.AddListener(OnSpawn);
            //죽음시 UI 비활성 필요
            doDie = transform.GetComponent<DoDie>();
            //죽음 이벤트 연결
            doDie.OnDieEvent.AddListener(OnDie);
            //데미지 발생 연결
            Connect();
        }

        /// <summary>
        /// 몬스터 스폰시
        /// </summary>
        void OnSpawn()
        {
            NamedMonsterHealthBarController.Instance.SetActivate(true);
        }

        /// <summary>
        /// 몬스터 죽음시
        /// </summary>
        /// <param name="pos"></param>
        void OnDie(Vector3 pos)
        {
            NamedMonsterHealthBarController.Instance.SetActivate(false);
        }

        /// <summary>
        /// 데미지 이벤트 발생 연결
        /// </summary>
        public void Connect()
        {
            haveHealth.DamagedEvent.AddListener(DamagedEventHandler);
        }

        /// <summary>
        /// 이벤트 등록 부분
        /// </summary>
        /// <param name="damage"></param>
        /// <param name="hit"></param>
        public void DamagedEventHandler(int damage, Vector3 hit, string keyValue)
        {
            //0-1 사이값으로 변경
            float amount = (float)haveHealth.Health / (float)haveHealth.MaxHealth;

            Debug.Log("========>   amount = " + amount + "  health = " + haveHealth.Health + "Max health = " + haveHealth.MaxHealth);

            //네임드 몬스터 체력바에 체력 적용
            NamedMonsterHealthBarController.Instance.SetAmount(amount);
        }
    }
}
