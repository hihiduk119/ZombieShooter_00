using UnityEngine;
using System.Collections;

using WoosanStudio.Extension;
using WoosanStudio.ZombieShooter;
/* THIS CODE IS JUST FOR PREVIEW AND TESTING */
// Feel free to use any code and picking on it, I cannot guaratnee it will fit into your project

[RequireComponent(typeof(destroyMe))]
public class ExplodingProjectile : MonoBehaviour , IHaveHitDamage
{
    public GameObject impactPrefab;
    public Rigidbody thisRigidbody;

    //매 프레임 업데이트를 통한 타겟 확인 방식.
    private Vector3 previousPosition;

    //Add Force에 사용될 파워
    public Vector3 Force;

    //플레이어가 쏜건지 몬스터가 쏜건지 알기 위한 용도.
    //ProjectileLauncher에게서 전달 받음.
    public bool playerShoted = true;

    //
    // 관통이 되게 설계할 필요가 있다.
    // 이떼 CheckCollision 에서 맞은 것 찾아서 리스트에 담아서 찾는 방법 쓰면 될듯 함.
    //

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
                haveHealth.DamagedEvent.Invoke(Damage, hit.point);
                Debug.Log("공격 받음 2");
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
}