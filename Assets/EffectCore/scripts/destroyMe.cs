using UnityEngine;
using System.Collections;

//Lean pool 사용
using Lean.Pool;

public class destroyMe : MonoBehaviour{

    float timer;
    public float deathtimer = 1;

    //[Object Pool]
    private ParticleSystem _particleSystem;

    //[Object Pool]
    private void Awake()
    {
        _particleSystem = this.GetComponent<ParticleSystem>();
    }

    //[Object Pool]
    //private void OnDisable()
    //{
    //    Reset();
    //}

    /// <summary>
    /// 파티클 재사용하기 위한 리//[Object Pool]
    /// </summary>
    #region -오브젝트풀 사용부분
    //private void Reset()
    //{
    //    timer = 0;

    //    transform.position = Vector3.zero;
    //    transform.rotation = Quaternion.identity;

    //    if (_particleSystem != null)
    //    {
    //        _particleSystem.Clear();
    //        _particleSystem.Simulate(0.0f, true, true);
    //        _particleSystem.Play();
    //    }

    //    gameObject.SetActive(false);

    //    Lean.Pool.LeanPool.Despawn(gameObject);
    //}
    #endregion

    void Update ()
    {
        timer += Time.deltaTime;

        if (timer >= deathtimer)
        {

            Destroy(this.gameObject);
            //[Object Pool]
            //Reset();
        }
    }
}
