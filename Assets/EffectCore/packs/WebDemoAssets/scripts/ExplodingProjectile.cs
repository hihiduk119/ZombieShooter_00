using UnityEngine;
using System.Collections;

/* THIS CODE IS JUST FOR PREVIEW AND TESTING */
// Feel free to use any code and picking on it, I cannot guaratnee it will fit into your project
public class ExplodingProjectile : MonoBehaviour
{
    public GameObject impactPrefab;
    //null임 사용 안함.
    public GameObject explosionPrefab;
    public float thrust;

    public Rigidbody thisRigidbody;

    public GameObject particleKillGroup;
    private Collider thisCollider;

    public bool LookRotation = true;
    //유도 미사일 모드임
    public bool Missile = false;
    public Transform missileTarget;
    public float projectileSpeed;
    public float projectileSpeedMultiplier;

    public bool ignorePrevRotation = false;

    public bool explodeOnTimer = false;
    public float explosionTimer;
    float timer;

    private Vector3 previousPosition;

    //오브젝트 풀
    public  WoosanStudio.ZombieShooter.IObjectPool ImpactPool;

    //[쓸모 없는거 정리하자 존나 했갈린다.]
    //
    void Start()
    {
        thisRigidbody = GetComponent<Rigidbody>();
        if (Missile)
        {
            missileTarget = GameObject.FindWithTag("Target").transform;
        }
        thisCollider = GetComponent<Collider>();
        previousPosition = transform.position;
    }

    //[Object Pool] 사용시 초기화용
    private void Reset()
    {
        //LookRotation = true;
        //Missile = false;

        //ignorePrevRotation = false;

        //explodeOnTimer = false;
        ////explosionTimer = 0f;
        //timer = 0f;

        thisRigidbody.velocity = Vector3.zero;

        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;

        gameObject.SetActive(false);

        Lean.Pool.LeanPool.Despawn(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        /*     if(Input.GetButtonUp("Fire2"))
             {
                 Explode();
             }*/
        timer += Time.deltaTime;
        if (timer >= explosionTimer && explodeOnTimer == true)
        {
            Explode();
        }

    }

    void FixedUpdate()
    {
        if (Missile)
        {
            projectileSpeed += projectileSpeed * projectileSpeedMultiplier;
            //   transform.position = Vector3.MoveTowards(transform.position, missileTarget.transform.position, 0);

            transform.LookAt(missileTarget);

            thisRigidbody.AddForce(transform.forward * projectileSpeed);
        }

        //if (LookRotation && timer >= 0.05f)
        //{
        //    transform.rotation = Quaternion.LookRotation(thisRigidbody.velocity);
        //}

        CheckCollision(previousPosition);

        previousPosition = transform.position;
    }

    void CheckCollision(Vector3 prevPos)
    {
        RaycastHit hit;
        Vector3 direction = transform.position - prevPos;
        Ray ray = new Ray(prevPos, direction);
        float dist = Vector3.Distance(transform.position, prevPos);
        if (Physics.Raycast(ray, out hit, dist))
        {

            //Debug.Log("충돌 속도 = " + thisRigidbody.velocity);
            transform.position = hit.point;
            Quaternion rot = Quaternion.FromToRotation(Vector3.forward, hit.normal);
            Vector3 pos = hit.point;
            //Instantiate(impactPrefab, pos, rot);
            Lean.Pool.LeanPool.Spawn(impactPrefab, pos, rot);

            //explodeOnTimer 는 시한 폭탁이고 Missile은 유도이기 때문에 둘다 사용 안함.
            if (!explodeOnTimer && Missile == false)
            {
                //Destroy(gameObject);
                //[Object Pool]
                Reset();
            }
            else if (Missile == true)
            {
                thisCollider.enabled = false;
                particleKillGroup.SetActive(false);
                thisRigidbody.velocity = Vector3.zero;
                //Destroy(gameObject, 5);
                //[Object Pool]
                Reset();
            }

        }
    }

    /// <summary>
    /// 사용 안하는데 만약 사용한다면 문제 있는거임
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "FX")
        {
            ContactPoint contact = collision.contacts[0];
            Quaternion rot = Quaternion.FromToRotation(Vector3.forward, contact.normal);
            if (ignorePrevRotation)
            {
                rot = Quaternion.Euler(0, 0, 0);
            }
            Vector3 pos = contact.point;
            //[Object Pool]
            Instantiate(impactPrefab, pos, rot);
            if (!explodeOnTimer && Missile == false)
            {
                Destroy(gameObject);
            }
            else if (Missile == true)
            {

                thisCollider.enabled = false;
                particleKillGroup.SetActive(false);
                thisRigidbody.velocity = Vector3.zero;

                Destroy(gameObject, 5);
            }
        }
    }

    /// <summary>
    /// explosionPrefab 이 null이라 사용할 일이 없음
    /// </summary>
    void Explode()
    {
        //[Object Pool]
        //Instantiate(explosionPrefab, gameObject.transform.position, Quaternion.Euler(0, 0, 0));
        //Destroy(gameObject);

        Lean.Pool.LeanPool.Spawn(explosionPrefab, gameObject.transform.position, Quaternion.Euler(0, 0, 0));
        Reset();
    }
}