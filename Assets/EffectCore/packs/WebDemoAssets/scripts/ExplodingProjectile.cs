using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using WoosanStudio.Extension;
using WoosanStudio.ZombieShooter;
/* THIS CODE IS JUST FOR PREVIEW AND TESTING */
// Feel free to use any code and picking on it, I cannot guaratnee it will fit into your project

[RequireComponent(typeof(destroyMe))]
public class ExplodingProjectile : MonoBehaviour , IHaveHitDamage
{
    public GameObject impactPrefab;
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

    //캐싱용
    RaycastHit hit;
    Vector3 direction;
    Ray ray;
    Coroutine dieTimer;
    int layerMask = 0;
    CardManager cardList;

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
            #region - [Test]
            GameObject testDummy = GameObject.FindGameObjectWithTag("TestDummy");
            testDummy.transform.position = hit.point;
            #endregion

            transform.position = hit.point;
            Quaternion rot = Quaternion.FromToRotation(Vector3.forward, hit.normal);
            Vector3 pos = hit.point;

            //부딫힌 오브젝트에 IHaveHealth를 가지고 있으면 데미지를 준다
            IHaveHealth haveHealth = hit.transform.GetComponent(typeof(IHaveHealth)) as IHaveHealth;
            if (haveHealth != null) {

                //몬스터 저항도 생각 해야한다.
                //몬스터 데이터 가져와서 저항에 따른 데미지 감소도 여기서 계산
                MonsterSettings monsterSettings = hit.transform.GetComponent<Monster>().monsterSettings;

                //카드를 통해 계산된 데미지를 주어야 한다.
                //*현재 총알에 세팅된 카드 리스트 받아오기

                ///카드의 정보와 몬스터의 정보를 가지고 계산해서 실제 받을 데미지를 계산
                Damage = DamageCalculator(CardManager.Instance.HasCards, monsterSettings);

                //keyValue 는 다음과 같다
                //{{0,"default"},{1,"critical"},{2,"status"} };
                haveHealth.DamagedEvent.Invoke(Damage, hit.point,"default");
            }

            //부딫힌 오브젝트가 IHaveHit 가지고 있다.
            // - Pong에니메이션 실행을 위해.
            // - 맞았을때 빨강 블링크 생행
            IHaveHit haveHit = hit.transform.GetComponent<IHaveHit>();
            if (haveHit != null) { haveHit.Hit(); }

            //test code start
            #region - [Test]
            if (TestPrefabs.instance != null)
            {
                TestPrefabs.instance.MakeEnd(pos);
            }
            //test code end
            #endregion

            Destroy(gameObject);
            //임팩트 생성/
            Instantiate(impactPrefab, pos, rot);
        }
    }

    /// <summary>
    /// 데미지 계산 해줌
    /// * 카드의 정보와 몬스터의 정보를 가지고 계산해서 실제 받을 데미지를 계산
    /// </summary>
    /// <param name="CardSettingsClone">카드의 정보</param>
    /// <param name="monsterSettings">몬스터의 정보</param>
    /// <returns>정수로 강제 변환하여 리턴</returns>
    int DamageCalculator(List<CardSetting> CardSettingsClone, MonsterSettings monsterSettings)
    {
        //Debug.Log("맞은 몬스터 ID = " + monsterSettings.MonsterId.ToString());
        //CardSettingsClone.ForEach(value => Debug.Log("카드 종류 = " + value.Type.ToString()));

        //무기 계산은 다음과 같이 계산하다
        //모든 프로퍼티 저장
        //무기 와 탄약에 기반한 기본 데미지 계산 = N

        //무기와 탄약 데미지의 합.
        //float damage = ;
        //카드는 중첩 카운트로 계산
        //카드 중첩 카운트 만큼 프로퍼티 생성
        

        return 5;
    }

    

    int CriticalDamage()
    {
        return -1;
    }
}