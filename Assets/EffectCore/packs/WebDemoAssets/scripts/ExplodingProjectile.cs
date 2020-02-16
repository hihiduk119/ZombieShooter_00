using UnityEngine;
using System.Collections;

/* THIS CODE IS JUST FOR PREVIEW AND TESTING */
// Feel free to use any code and picking on it, I cannot guaratnee it will fit into your project
public class ExplodingProjectile : MonoBehaviour
{
    public GameObject impactPrefab;
    public Rigidbody thisRigidbody;

    //매 프레임 업데이트를 통한 타겟 확인 방식.
    private Vector3 previousPosition;

    //Add Force에 사용될 파워
    public Vector3 Force;

    /// <summary>
    /// 관통이 되게 설계할 필요가 있다.
    /// 이떼 CheckCollision 에서 맞은 것 찾아서 리스트에 담아서 찾는 방법 쓰면 될듯 함.
    /// </summary>
    ///

    //캐싱용
    RaycastHit hit;
    Vector3 direction;
    Ray ray;

    void Start()
    {
        thisRigidbody = GetComponent<Rigidbody>();
        previousPosition = transform.position;
    }

    //[Object Pool] 사용시 초기화용 , 근데 사용을 안함???
    private void Reset()
    {
        thisRigidbody.velocity = Vector3.zero;
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        gameObject.SetActive(false);
        Lean.Pool.LeanPool.Despawn(gameObject);
    }


    /// <summary>
    /// 발사시 호p
    /// </summary>
    public void Launch()
    {
        thisRigidbody.AddForce(Force);
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
        ray = new Ray(prevPos, direction);
        float dist = Vector3.Distance(transform.position, prevPos);
        if (Physics.Raycast(ray, out hit, dist))
        {
            //충돌 예외처리
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Barrier")) return;

            transform.position = hit.point;
            Quaternion rot = Quaternion.FromToRotation(Vector3.forward, hit.normal);
            Vector3 pos = hit.point;

            //Instantiate(impactPrefab, pos, rot);
            //Destroy(gameObject);
            //[Object Pool]
            Lean.Pool.LeanPool.Spawn(impactPrefab, pos, rot);
            //사용을 안하는 이유가 궁금
            Reset();
        }
    }
}