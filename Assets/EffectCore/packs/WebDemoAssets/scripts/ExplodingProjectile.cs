using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using WoosanStudio.Extension;
using WoosanStudio.ZombieShooter;
using WoosanStudio.ZombieShooter.UI;
/* THIS CODE IS JUST FOR PREVIEW AND TESTING */
// Feel free to use any code and picking on it, I cannot guaratnee it will fit into your project

[RequireComponent(typeof(destroyMe))]
public class ExplodingProjectile : MonoBehaviour , IHaveHitDamage
{
    public GameObject impactPrefab;
    [Header("[피 튀김 이팩트")]
    public GameObject bloodImpactPrefab;
    public Rigidbody thisRigidbody;

    //Add Force에 사용될 파워
    public Vector3 Force;

    //플레이어가 쏜건지 몬스터가 쏜건지 알기 위한 용도.
    //ProjectileLauncher에게서 전달 받음.
    public bool playerShoted = true;

    //
    // 관통이 되게 설계할 필요가 있다.
    // 이떼 CheckCollision 에서 맞은 것 찾아서 리스트에 담아서 찾는 방법 쓰면 될듯 함.
    //

    //매 프레임 업데이트를 통한 타겟 확인 방식.
    private Vector3 previousPosition;
    //자동으로 탄이 디스폰되는 시간 설정값
    private float autoDieTime = 1f;
    //플레이어가 쏜 총알 Raycast용 마스크 
    int playerShotedLayerMask = 0;
    //몬스터가 쏜 총알 Raycastd용 마스크
    int monsterShotedLayerMask = 0;

    /// <summary>
    /// 데미지 계산 반환용 클래스
    /// </summary>
    public class CalculatorInfo
    {
        public int Damage;
        public string KeyValue;

        public CalculatorInfo(int damage,string keyValue)
        {
            Damage = damage;
            KeyValue = keyValue;
        }
    }

    //캐싱용
    RaycastHit hit;
    Vector3 direction;
    Ray ray;
    Coroutine dieTimer;
    int layerMask = 0;
    //CardManager cardList;

    //=====================>   IHaveHitDamage 시작    <=====================
    private int _damage = 40;
    public int Damage { get => _damage; set => _damage = value; }
    //=====================>   IHaveHitDamage 끝    <=====================

    void Start()
    {
        thisRigidbody = GetComponent<Rigidbody>();
        previousPosition = transform.position;

        //Barrier 를 제외한 나머지만 체크함.
        playerShotedLayerMask = ~(1 << LayerMask.NameToLayer("Barrier"));
        
        //Barrier 를 제외한 나머지만 체크함.
        monsterShotedLayerMask = ~(1 << LayerMask.NameToLayer("Monster"));

        //Destory Me 자동 추가
    }

    /// <summary>
    /// 자동으로 autoDieTime시간이 지나면 Despawn
    /// </summary>
    public void BeginDieTimer()
    {
        if (dieTimer != null) StopCoroutine(dieTimer);
        dieTimer = StartCoroutine(DieTimer());
    }

    IEnumerator DieTimer()
    {
        float deltaTime = 0;
        WaitForEndOfFrame WFE = new WaitForEndOfFrame();
        while(autoDieTime > deltaTime)
        {
            deltaTime += Time.deltaTime;

            yield return WFE;
        }

        Destroy(gameObject);
        //[Object Pool]
        //Reset();
    }    

    /// <summary>
    /// 발사시 호출
    /// </summary>
    public void Launch()
    {
        //리지드 바디에 가속도 적용
        thisRigidbody.AddForce(Force);
        //부딫히지 않았을 자동 디스폰 타이머 작동
        BeginDieTimer();
    }

    /// <summary>
    /// 픽스드 업데이트를 통해서 매 프레임마다 레이를 쏴서 물체와의 충동을 체크
    /// </summary>
    void FixedUpdate()
    {
        CheckCollision(previousPosition);
        previousPosition = transform.position;
    }

    /// <summary>
    /// 데미지 체크
    /// </summary>
    /// <param name="prevPos"></param>
    void CheckCollision(Vector3 prevPos)
    {
        direction = transform.position - prevPos;

        //Debug.Log("direction = " + direction);

        ray = new Ray(prevPos, direction);
        float dist = Vector3.Distance(transform.position, prevPos);

        //Debug.DrawRay(transform.position, prevPos, Color.blue, 0.3f);

        if (playerShoted) { layerMask = playerShotedLayerMask; }//플레이어 사격시
        else { layerMask = monsterShotedLayerMask; }//몬스터 사격시

        if (Physics.Raycast(ray, out hit, dist , layerMask))
        {
            /*
            #region - [Test]
            GameObject testDummy = GameObject.FindGameObjectWithTag("TestDummy");
            testDummy.transform.position = hit.point;
            #endregion
            */

            transform.position = hit.point;
            Quaternion rot = Quaternion.FromToRotation(Vector3.forward, hit.normal);
            Vector3 pos = hit.point;

            //부딫힌 오브젝트에 IHaveHealth를 가지고 있으면 데미지를 준다
            IHaveHealth haveHealth = hit.transform.GetComponent(typeof(IHaveHealth)) as IHaveHealth;

            if (haveHealth != null)
            {
                //몬스터 저항도 생각 해야한다.
                //몬스터 데이터 가져와서 저항에 따른 데미지 감소도 여기서 계산
                MonsterSettings monsterSettings = hit.transform.GetComponent<Monster>().MonsterSettings;

                //카드를 통해 계산된 데미지를 주어야 한다.
                //*현재 총알에 세팅된 카드 리스트 받아오기

                //계산 정보 전용 클래스
                CalculatorInfo calculatorInfo =  null;
                ///카드의 정보와 몬스터의 정보를 가지고 계산해서 실제 받을 데미지를 계산
                calculatorInfo = DamageCalculator(monsterSettings);

                //몬스터에 데미지 전달
                //keyValue 는 다음과 같다
                //{{0,"default"},{1,"critical"},{2,"status"} };
                haveHealth.DamagedEvent.Invoke(calculatorInfo.Damage, hit.point, calculatorInfo.KeyValue);

                //히트 메시지 출력
                //* 크리티컬시 주황색 및 사이즈 크게 트윈
                if (calculatorInfo.KeyValue.Equals("critical"))
                {
                    HitMessage.Instance.Show(calculatorInfo.Damage, 1, 2f);
                } else
                {
                    HitMessage.Instance.Show(calculatorInfo.Damage, 0);
                }
            }

            //부딫힌 오브젝트가 IHaveHit 가지고 있다.
            // - Pong에니메이션 실행을 위해.
            // - 맞았을때 빨강 블링크 생행
            IHaveHit haveHit = hit.transform.GetComponent<IHaveHit>();
            if (haveHit != null) { haveHit.Hit(); }

            /*
            //test code start
            #region - [Test]
            if (TestPrefabs.instance != null)
            {
                TestPrefabs.instance.MakeEnd(pos);
            }
            //test code end
            #endregion
            */

            Destroy(gameObject);
            //임팩트 생성/
            Instantiate(impactPrefab, pos, rot);

            //피튀김 이펙트 생성
            //*체력이 있다는건 몬스터를 의미
            if(haveHealth != null)
            {
                //피 임팩트가 있음
                if (bloodImpactPrefab != null)
                {
                    Vector3 rotation = rot.eulerAngles;
                    rotation.y += 15*Random.Range(-1,5);
                    rot = Quaternion.Euler(rotation);
                    Instantiate(bloodImpactPrefab, pos, rot);
                }
            }
        }
    }

    /// <summary>
    /// 데미지 계산 해줌
    /// * 카드의 정보와 몬스터의 정보를 가지고 계산해서 실제 받을 데미지를 계산
    /// * 카드에 의해 발생한 데미지 증감 모두 반영
    /// * 몬스터 저항에 의한 데미지 증감 모두 반영
    /// </summary>
    /// <param name="CardSettingsClone">카드의 정보</param>
    /// <param name="monsterSettings">몬스터의 정보</param>
    /// <returns>정수로 강제 변환하여 리턴</returns>
    CalculatorInfo DamageCalculator(MonsterSettings monsterSettings)
    {
        //몬스터 아이디가 100이상이면 네임드 몬스터
        //bool isNamedZombie = false;
        bool isCriticalDamage = false;
        float damage = 0;
        
        //카드 메니저에 데미지 계산요청
        //*건 타입 및 탄약 타입 ,네임드 ,일반 몬스터 특성에 의한 계산
        //*hasCards반복 사용이 퍼포먼스에 영향을 줄수도 있음
        damage = WoosanStudio.ZombieShooter.DamageCalculator.Instance.GetHitDamageFromPlayer(monsterSettings);

        //크리티컬 판정인지 확률 계산
        if (isCriticalDamage = WoosanStudio.ZombieShooter.DamageCalculator.Instance.IsCriticalDamage())
        {
            //크리티컬 판정시 데미지 가져오기
            damage = WoosanStudio.ZombieShooter.DamageCalculator.Instance.GetCriticalDamage(damage);
        }

        ////몬스터에 저항이 존재 한다면 계산해서 저항받은 데미지 계산
        damage = WoosanStudio.ZombieShooter.DamageCalculator.Instance.GetDamageThatReflectsMonsterResistance(damage, monsterSettings.Propertys, monsterSettings.Level);
        ////데미지가 1보다 작다면 최소 데미지 1로 마춤
        if (damage < 1f) damage = 1f;

        //keyValue 는 다음과 같다
        //{{0,"default"},{1,"critical"},{2,"status"} };
        string keyValue = null;                 
        if (isCriticalDamage) keyValue = "critical";
        else keyValue = "default";

        CalculatorInfo calculatorInfo = new CalculatorInfo(Mathf.RoundToInt(damage), keyValue);

        //최종 데미지 int로 변환해서 리턴
        return calculatorInfo;
    }
}